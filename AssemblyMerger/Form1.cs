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
        private Client client;

        string ILMergePath;
        string outputPath;
        //Dictionary<CheckBox, TextBox> selectedFileControl;
        BindingList<KeyValuePair<string, string>> bindingList;

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

        }

                if (dialogresult == DialogResult.No)
                {
                    return;
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
    }
}
