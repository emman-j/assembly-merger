namespace ILMergeGUI
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SelectFiles = new System.Windows.Forms.Button();
            this.SelectedFilesListbox = new System.Windows.Forms.ListBox();
            this.Merge = new System.Windows.Forms.Button();
            this.outputFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectOutPath = new System.Windows.Forms.Button();
            this.outputFilename = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.selectILMergerExe = new System.Windows.Forms.Button();
            this.ILMergerPathTB = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.SelectFiles);
            this.groupBox1.Controls.Add(this.SelectedFilesListbox);
            this.groupBox1.Location = new System.Drawing.Point(13, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(376, 164);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Files to Merge";
            // 
            // SelectFiles
            // 
            this.SelectFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFiles.Location = new System.Drawing.Point(269, 132);
            this.SelectFiles.Name = "SelectFiles";
            this.SelectFiles.Size = new System.Drawing.Size(100, 23);
            this.SelectFiles.TabIndex = 1;
            this.SelectFiles.Text = "Select files";
            this.SelectFiles.UseVisualStyleBackColor = true;
            // 
            // SelectedFilesListbox
            // 
            this.SelectedFilesListbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedFilesListbox.FormattingEnabled = true;
            this.SelectedFilesListbox.Location = new System.Drawing.Point(7, 26);
            this.SelectedFilesListbox.Name = "SelectedFilesListbox";
            this.SelectedFilesListbox.Size = new System.Drawing.Size(362, 95);
            this.SelectedFilesListbox.TabIndex = 0;
            // 
            // Merge
            // 
            this.Merge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Merge.Location = new System.Drawing.Point(308, 355);
            this.Merge.Name = "Merge";
            this.Merge.Size = new System.Drawing.Size(75, 27);
            this.Merge.TabIndex = 10;
            this.Merge.Text = "Merge";
            this.Merge.UseVisualStyleBackColor = true;
            // 
            // outputFilePath
            // 
            this.outputFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFilePath.Location = new System.Drawing.Point(7, 50);
            this.outputFilePath.Name = "outputFilePath";
            this.outputFilePath.Size = new System.Drawing.Size(363, 20);
            this.outputFilePath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Output Filename";
            // 
            // SelectOutPath
            // 
            this.SelectOutPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectOutPath.Location = new System.Drawing.Point(232, 77);
            this.SelectOutPath.Name = "SelectOutPath";
            this.SelectOutPath.Size = new System.Drawing.Size(138, 23);
            this.SelectOutPath.TabIndex = 2;
            this.SelectOutPath.Text = "Select Output Path";
            this.SelectOutPath.UseVisualStyleBackColor = true;
            // 
            // outputFilename
            // 
            this.outputFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFilename.Location = new System.Drawing.Point(114, 24);
            this.outputFilename.Name = "outputFilename";
            this.outputFilename.Size = new System.Drawing.Size(255, 20);
            this.outputFilename.TabIndex = 4;
            this.outputFilename.Text = "CombinedLibrary";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.selectILMergerExe);
            this.groupBox3.Controls.Add(this.ILMergerPathTB);
            this.groupBox3.Location = new System.Drawing.Point(12, 298);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(377, 51);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ILMerge Executable";
            // 
            // selectILMergerExe
            // 
            this.selectILMergerExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectILMergerExe.Location = new System.Drawing.Point(296, 20);
            this.selectILMergerExe.Name = "selectILMergerExe";
            this.selectILMergerExe.Size = new System.Drawing.Size(75, 23);
            this.selectILMergerExe.TabIndex = 7;
            this.selectILMergerExe.Text = "Select";
            this.selectILMergerExe.UseVisualStyleBackColor = true;
            // 
            // ILMergerPathTB
            // 
            this.ILMergerPathTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ILMergerPathTB.Location = new System.Drawing.Point(8, 20);
            this.ILMergerPathTB.Name = "ILMergerPathTB";
            this.ILMergerPathTB.Size = new System.Drawing.Size(282, 20);
            this.ILMergerPathTB.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.outputFilePath);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.SelectOutPath);
            this.groupBox2.Controls.Add(this.outputFilename);
            this.groupBox2.Location = new System.Drawing.Point(13, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(376, 107);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output Configuration";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox2.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox2.Location = new System.Drawing.Point(13, 387);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(376, 127);
            this.textBox2.TabIndex = 13;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 531);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Merge);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox2);
            this.MinimumSize = new System.Drawing.Size(416, 570);
            this.Name = "Form2";
            this.Text = "Form2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SelectFiles;
        private System.Windows.Forms.ListBox SelectedFilesListbox;
        private System.Windows.Forms.Button Merge;
        private System.Windows.Forms.TextBox outputFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SelectOutPath;
        private System.Windows.Forms.TextBox outputFilename;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button selectILMergerExe;
        private System.Windows.Forms.TextBox ILMergerPathTB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox2;
    }
}