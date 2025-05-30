using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ILMergeGUI.Library.Service
{
    public class ILMergeService: INotifyBase
    {
        private string _ILMergePath;
        private string _outputPath;
        private bool _zeroPeKind = true;
        private string _outputFilename = "MergedAssembly";

        public bool ZeroPEKind
        { 
            get => _zeroPeKind;
            set
            {
                _zeroPeKind = value;
                NotifyPropertyChanged();
            }
        }
        public string ILMergePath
        { 
            get => _ILMergePath;
            set
            {
                _ILMergePath = value;
                NotifyPropertyChanged();
            }
        }
        public string OutputPath
        {
            get => _outputPath;
            set
            {
                if (_outputPath != value)
                {
                    _outputPath = value;
                    NotifyPropertyChanged(nameof(OutputPath));
                    NotifyPropertyChanged(nameof(OutputFilePath)); // Trigger update
                }
            }
        }
        public string OutputFilename
        {
            get => _outputFilename;
            set
            {
                if (_outputFilename != value)
                {
                    _outputFilename = value;
                    NotifyPropertyChanged(nameof(OutputFilename));
                    NotifyPropertyChanged(nameof(OutputFilePath)); // Trigger update
                }
            }
        }
        public string OutputFilePath => Path.Combine(OutputPath ?? "", $"{OutputFilename}.dll");

        private string BatchScript(BindingList<KeyValuePair<string, string>> bindingList, string outputFilePath)
        {
            string script = string.Empty;
            string dllFiles = string.Empty;
            foreach (var kvp in bindingList)
            {
                dllFiles += $"{kvp.Value} ";
            }

            if (ZeroPEKind)
            {
                script = $"{ILMergePath} /out:{outputFilePath} /zeroPeKind {dllFiles}";
            }
            else
            {
                script = $"{ILMergePath} /out:{outputFilePath} {dllFiles}";
            }

            return script;
        }
        public string SearchILMergeExe(string directoryPath, string fileNameToSearch)
        {
            string ExePath = string.Empty;

            try
            {
                string[] files = Directory.GetFiles(directoryPath, fileNameToSearch, SearchOption.AllDirectories);

                if (files.Length == 0)
                {
                    return string.Empty;
                }

                foreach (var file in files)
                {
                    ExePath = file;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}" + Environment.NewLine);
            }

            return ExePath;
        }
        public string MergeFiles(BindingList<KeyValuePair<string, string>> bindingList)
        {
            try
            {
                string batchscript = BatchScript(bindingList, OutputFilePath);

                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe", // Run the command prompt
                    RedirectStandardInput = true, // Redirect input so we can pass commands
                    RedirectStandardOutput = true, // Redirect output to capture the results
                    RedirectStandardError = true, // Redirect errors
                    UseShellExecute = false, // Required for redirection
                    CreateNoWindow = true // Do not show the console window
                };

                using (Process process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    using (var writer = process.StandardInput)
                    {
                        if (writer.BaseStream.CanWrite)
                        {
                            writer.WriteLine(batchscript);
                        }
                    }

                    // Capture the output
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrWhiteSpace(output) && !string.IsNullOrWhiteSpace(error))
                    {
                        return Environment.NewLine + $"Output:\n{output}\n\nErrors:\n{error}" + Environment.NewLine;
                    }
                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        return Environment.NewLine + $"Output: " + Environment.NewLine + $"{output}" + Environment.NewLine;
                    }
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        return Environment.NewLine + $"Errors: " + Environment.NewLine + $"{error}" + Environment.NewLine;
                    }
                    return Environment.NewLine + $"Output:\n{output}\n\nErrors:\n{error}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
