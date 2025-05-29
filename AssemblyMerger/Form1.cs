using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormControl = System.Windows.Forms;

namespace ILMergeGUI
{
    public partial class Form1 : Form
    {
        string ILMergePath;
        string outputPath;
        Dictionary<CheckBox, TextBox> selectedFileControl;
        BindingList<KeyValuePair<string, string>> bindingList;

        public Form1()
        {
            InitializeComponent();

            var dictionary = new Dictionary<string, string>();
            bindingList = new BindingList<KeyValuePair<string, string>>(dictionary.ToList());

            SelectedFilesListbox.DataSource = bindingList;
            SelectedFilesListbox.DisplayMember = "Key";

            selectedFileControl = new Dictionary<CheckBox, TextBox>
            {
                {checkBox1, textBox1 },
                {checkBox2, textBox3 },
                {checkBox3, textBox4 },
                {checkBox4, textBox5 },
                {checkBox5, textBox6 },
                {checkBox6, textBox7 },
                {checkBox7, textBox8 },
                {checkBox8, textBox9 },
                {checkBox9, textBox10 },
                {checkBox10, textBox11 }
            };

            CreateOutputDir();
            outputFilePath.Text = $"{outputPath}\\{outputFilename.Text}.dll";
        }
        private void Merge_Click(object sender, EventArgs e)
        {
            if (bindingList.Count == 0)
            {
                MessageBox.Show("Please select files to merge!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ILMergePath))
            {
                MessageBox.Show("Please select ILMerge.exe!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (File.Exists(outputPath))
            {
                DialogResult dialogresult = MessageBox.Show("Change the output file name if you do not wish to overwrite the existing file.\n" +
                    "Do you wish to overwrite the existing file?",
                    "File already exists!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogresult == DialogResult.No)
                {
                    return;
                }
            }

            //string output = RunBatchScript();
            string output = MergeFiles();
            consoleTB.AppendText(output + Environment.NewLine);

            if (output.Contains("An exception occurred") && output.Contains("/zeroPeKind"))
            {
                consoleTB.AppendText("An exception occurred during merging." + Environment.NewLine);
                consoleTB.AppendText("One of your assemblies is not marked as containing only managed code,\n" +
                    "meaning it likely includes unmanaged code (e.g., native code or interop with unmanaged libraries).." + Environment.NewLine);
                consoleTB.AppendText("Select the ignore Portable Executable to ignore the PE (portable executable) kind\n" +
                    "and allow it to merge assemblies that contain unmanaged code.\nBUT READ THE DOCUMENTATION FIRST!" + Environment.NewLine);

                return;
            }
            else if (output.Contains("An exception occurred"))
            {
                consoleTB.AppendText("An exception occurred during merging." + Environment.NewLine);
                return;
            }
            else
            {
                consoleTB.AppendText("No exception found in the output." + Environment.NewLine);
                ClearControls();
                return;
            }
        }
        private void SelectOutPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = outputPath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    outputPath = folderDialog.SelectedPath;

                    outputFilePath.Text = $"{outputPath}\\{outputFilename.Text}.dll";

                    consoleTB.AppendText($"Output: {outputPath}" + Environment.NewLine);
                }
            }
        }
        private void selectILMergerExe_Click(object sender, EventArgs e)
        {
            string initialExePath = SearchILMergeExe(Path.GetDirectoryName(Application.ExecutablePath), "ILMerge.exe");

            if (File.Exists(initialExePath))
            {
                ILMergerPathTB.Text = ILMergePath = initialExePath;
                consoleTB.AppendText($"Exe found: {ILMergePath}" + Environment.NewLine);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Exe Files|*.exe|All Files|*.*",
                Title = "Select ILMerge.Exe",
                InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath)
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ILMergePath = openFileDialog.FileName;
                consoleTB.AppendText($"Exe found: {ILMergePath}" + Environment.NewLine);
            }
        }
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            TextBox textbox = selectedFileControl[checkbox];
            //int indexByKey = selectedFileControl.Keys.ToList().IndexOf(checkbox);

            textbox.Text = checkbox.Checked? SelectFile() : textbox.Text;

            if (!checkbox.Checked)
            {
                RemoveItem(textbox.Text);
                textbox.Text = string.Empty;
            }
            textbox.Enabled = checkbox.Checked = !string.IsNullOrEmpty(textbox.Text);
        }
        private void outputFilename_TextChanged(object sender, EventArgs e)
        {
            outputFilePath.Text = $"{outputPath}\\{outputFilename.Text}.dll";
        }
        private void OpenOutputFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(outputPath))
            {
                Process.Start("explorer.exe", outputPath);
            }
            else
            {
                consoleTB.AppendText("The specified folder does not exist." + Environment.NewLine);
            }
        }
        private void CreateOutputDir()
        {
            string directoryPath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\output";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            outputPath = directoryPath;
        }
        private string SearchILMergeExe(string directoryPath, string fileNameToSearch)
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
                consoleTB.AppendText($"Error: {ex.Message}" + Environment.NewLine); 
            }

            return ExePath;
        }
        private string MergeFiles()
        {
            try
            {
                string batchscript = BatchScript();

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
                        return Environment.NewLine + $"Output: "+ Environment.NewLine + $"{output}" + Environment.NewLine;
                    }
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        return Environment.NewLine + $"Errors: "+ Environment.NewLine + $"{error}" + Environment.NewLine;
                    }
                    return Environment.NewLine + $"Output:\n{output}\n\nErrors:\n{error}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string RunBatchScript()
        {
            try
            {
                string batchFilePath = Path.Combine(Path.GetTempPath(), "tempILMergeBatch.bat");
                string batchscript = BatchScript();
                string output = string.Empty;
                string error = string.Empty;

                // Write the batch script content
                string batchContent = $@"
                @echo off
                echo Running ILMerge...
                {batchscript}
                echo ILMerge completed!
                pause";

                File.WriteAllText(batchFilePath, batchContent);

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
                            writer.WriteLine(batchFilePath);
                        }
                    }

                    output = process.StandardOutput.ReadToEnd();
                    error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                }

                if (File.Exists(batchFilePath))
                {
                    File.Delete(batchFilePath);
                }

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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string SelectFile()
        {
            string selectedfile = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "DLL Files|*.dll|All Files|*.*",
                Title = "Select Assemblies to Merge"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = openFileDialog.FileName;
                string filename = Path.GetFileName(filepath);

                if (ItemExists(filename))
                { return string.Empty; }

                AddItem(filename, filepath);
                selectedfile = filename;
            }
            return selectedfile;
        }
        private string[] SelectFilesMultiple()
        {
            string[] selectedfiles = null;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "DLL Files|*.dll|All Files|*.*",
                Title = "Select Assemblies to Merge"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedfiles = openFileDialog.FileNames;
                foreach (var file in selectedfiles)
                {
                    AddItem(Path.GetFileName(file), file);
                }
            }
            return selectedfiles;
        }
        private bool ItemExists(string key)
        {
            return bindingList.Any(kvp => kvp.Key == key);
        }
        private void AddItem(string key, string value)
        {
            if (!ItemExists(key))
                bindingList.Add(new KeyValuePair<string, string>(key, value));
        }
        private void RemoveItem(string key)
        {
            var item = bindingList.FirstOrDefault(kvp => kvp.Key == key);
            if (!item.Equals(default(KeyValuePair<string, string>)))
                bindingList.Remove(item);
        }
        private string BatchScript()
        { 
            string script = string.Empty;
            string dllFiles = string.Empty;
            foreach (var kvp in bindingList)
            { 
                dllFiles += $"{kvp.Value} ";
            }

            if (zeroPEKind.Checked)
            {
                script = $"{ILMergePath} /out:{outputFilePath.Text} /zeroPeKind {dllFiles}";
            }
            else
            {
                script = $"{ILMergePath} /out:{outputFilePath.Text} {dllFiles}";
            }

            return script;
        }
        private void ClearControls()
        {
            foreach (var kvp in selectedFileControl)
            {
                CheckBox checkbox = kvp.Key;
                TextBox textBox = kvp.Value;

                textBox.Text = string.Empty;
                textBox.Enabled = checkbox.Checked = false;
            }
            bindingList.Clear();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }
    }
}
