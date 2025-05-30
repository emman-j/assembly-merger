using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ILMergeGUI.Library.Service;

namespace ILMergeGUI.Library
{
    public class Client : INotifyBase
    {
        private BindingList<KeyValuePair<string, string>> _bindingList = new BindingList<KeyValuePair<string, string>>(new Dictionary<string, string>().ToList());

        internal readonly TextBoxWriter ConsoleListener;
        internal ILMergeService ILMerge;
        internal Dictionary<CheckBox, TextBox> selectedFileControl;

        public BindingList<KeyValuePair<string, string>> bindingList
        {
            get => _bindingList;
            set
            {
                _bindingList = value;
                NotifyPropertyChanged();
            }
        }

        public Client(Dictionary<CheckBox, TextBox> selectedFilesControl) 
        {
            ConsoleListener = new TextBoxWriter();
            ILMerge = new ILMergeService();

            selectedFileControl = selectedFilesControl;
            ILMerge.OutputPath = CreateOutputDir(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output"));
            SetCheckBoxBindings();
        }

        private void SetCheckBoxBindings()
        {
            void checkBox_CheckedChanged(object sender, EventArgs e)
            {
                CheckBox checkbox = sender as CheckBox;
                TextBox textbox = selectedFileControl[checkbox];

                textbox.Text = checkbox.Checked ? SelectFile() : textbox.Text;

                if (!checkbox.Checked)
                {
                    RemoveItem(textbox.Text);
                    textbox.Text = string.Empty;
                }
                textbox.Enabled = checkbox.Checked = !string.IsNullOrEmpty(textbox.Text);
            }

            foreach (var kvp in selectedFileControl)
            {
                CheckBox checkbox = kvp.Key;
                checkbox.CheckedChanged += checkBox_CheckedChanged;
            }
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

        public string CreateOutputDir(string defaultPath)
        {
            if (!Directory.Exists(defaultPath))
            {
                Directory.CreateDirectory(defaultPath);
            }
            return defaultPath;
        }
        public void OpenOutputFolder()
        {
            if (Directory.Exists(ILMerge.OutputPath))
                Process.Start("explorer.exe", ILMerge.OutputPath);
            else
                Console.WriteLine("The specified folder does not exist." + Environment.NewLine);
        }
        public void Merge()
        {
            if (bindingList.Count == 0)
            {
                MessageBox.Show("Please select files to merge!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ILMerge.ILMergePath))
            {
                MessageBox.Show("Please select ILMerge.exe!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (File.Exists(ILMerge.OutputFilePath))
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
            string output = ILMerge.MergeFiles(bindingList);

            Console.WriteLine(output + Environment.NewLine);

            if (output.Contains("An exception occurred") && output.Contains("/zeroPeKind"))
            {
                Console.WriteLine("An exception occurred during merging." + Environment.NewLine);
                Console.WriteLine("One of your assemblies is not marked as containing only managed code,\n" +
                    "meaning it likely includes unmanaged code (e.g., native code or interop with unmanaged libraries).." + Environment.NewLine);
                Console.WriteLine("Select the ignore Portable Executable to ignore the PE (portable executable) kind\n" +
                    "and allow it to merge assemblies that contain unmanaged code.\nBUT READ THE DOCUMENTATION FIRST!" + Environment.NewLine);

                return;
            }
            else if (output.Contains("An exception occurred"))
            {
                Console.WriteLine("An exception occurred during merging." + Environment.NewLine);
                return;
            }
            else
            {
                Console.WriteLine("No exception found in the output." + Environment.NewLine);
                ClearControls();
                return;
            }
        }

        public string SelectFile()
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
        public string[] SelectFilesMultiple()
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
        public void SelectOutPath()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = ILMerge.OutputPath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    ILMerge.OutputPath = folderDialog.SelectedPath;

                    Console.WriteLine($"Output: {ILMerge.OutputPath}" + Environment.NewLine);
                }
            }
        }
        public void SearchILMergeExe()
        {
            string initialExePath = ILMerge.SearchILMergeExe(Path.GetDirectoryName(Application.ExecutablePath), "ILMerge.exe");

            if (File.Exists(initialExePath))
            {
                ILMerge.ILMergePath = initialExePath;
                Console.WriteLine($"Exe found: {ILMerge.ILMergePath}" + Environment.NewLine);
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
                ILMerge.ILMergePath = openFileDialog.FileName;
                Console.WriteLine($"Exe found: {ILMerge.ILMergePath}" + Environment.NewLine);
            }
        }

        public bool ItemExists(string key)
        {
            return bindingList.Any(kvp => kvp.Key == key);
        }
        public void AddItem(string key, string value)
        {
            if (!ItemExists(key))
                bindingList.Add(new KeyValuePair<string, string>(key, value));
        }
        public void RemoveItem(string key)
        {
            var item = bindingList.FirstOrDefault(kvp => kvp.Key == key);
            if (!item.Equals(default(KeyValuePair<string, string>)))
                bindingList.Remove(item);
        }
    }
}
