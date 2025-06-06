﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ILMergeGUI.Library;

namespace ILMergeGUI
{
    public partial class Form1 : Form
    {
        private Client client;

        public Form1()
        {
            InitializeComponent();

            Dictionary<CheckBox, TextBox> selectedFileControl = new Dictionary<CheckBox, TextBox>
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

            client = new Client(selectedFileControl);
            client.ConsoleListener.RedirectConsoleToTextBox(consoleTB);

            SetDataBindings();
        }

        private void SetDataBindings()
        {
            SelectedFilesListbox.DataSource = client.bindingList;
            SelectedFilesListbox.DisplayMember = "Key";
            ILMergerPathTB.DataBindings.Add("Text", client.ILMerge, nameof(client.ILMerge.ILMergePath), true, DataSourceUpdateMode.OnPropertyChanged);
            outputFilePath.DataBindings.Add("Text", client.ILMerge, nameof(client.ILMerge.OutputPath), true, DataSourceUpdateMode.OnPropertyChanged);
            outputFilename.DataBindings.Add("Text", client.ILMerge, nameof(client.ILMerge.OutputFilename), true, DataSourceUpdateMode.OnPropertyChanged);
            zeroPEKind.DataBindings.Add("Checked", client.ILMerge, nameof(client.ILMerge.ZeroPEKind), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void Merge_Click(object sender, EventArgs e)
        {
            client.Merge();
        }
        private void SelectOutPath_Click(object sender, EventArgs e)
        {
            client.SelectOutPath();
        }
        private void selectILMergerExe_Click(object sender, EventArgs e)
        {
            client.SearchILMergeExe();
        }
        private void OpenOutputFolder_Click(object sender, EventArgs e)
        {
            client.OpenOutputFolder();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }

        private void ClearConsoleBtn_Click(object sender, EventArgs e)
        {
            consoleTB.Clear();
        }

        private void AboutLabel_Click(object sender, EventArgs e)
        {
            client.AboutApp();
        }
    }
}
