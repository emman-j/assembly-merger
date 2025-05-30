using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILMergeGUI.Library.Service
{
    public class TextBoxWriter : TextWriter
    {
        public TextBox textBox { get; set; }
        public override Encoding Encoding => Encoding.UTF8;

        public TextBoxWriter(TextBox textBox)
        {
            this.textBox = textBox;
        }
        public TextBoxWriter()
        {
        }

        public void RedirectConsoleToTextBox(TextBox textBox)
        {
            this.textBox = textBox;
            TextWriter textBoxWriter = new TextBoxWriter(textBox);
            Console.SetOut(textBoxWriter);
        }
        public void RedirectDebugToTextBox(TextBox textBox)
        {
            TextBoxWriter writer = this;
            this.textBox = textBox;
            Debug.Listeners.Clear();
            Debug.Listeners.Add(new TextWriterTraceListener(writer));
            Debug.AutoFlush = true;
        }
        public void UnlinkConsoleFromTextBox()
        {
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
        public void UnlinkDebugFromTextBox()
        {
            Debug.Listeners.Clear();
        }
        public override void WriteLine(string value)
        {
            if (textBox.IsDisposed) return;

            if (textBox.InvokeRequired)
            {
                textBox?.Invoke(new Action(() => textBox?.AppendText(Environment.NewLine + value)));
            }
            else
            {
                textBox?.AppendText(Environment.NewLine + value);
            }
        }
        public override void Write(string value)
        {
            if (textBox.IsDisposed) return;

            if (textBox.InvokeRequired)
            {
                textBox?.Invoke(new Action(() => textBox?.AppendText(value)));
            }
            else
            {
                textBox?.AppendText(value);
            }
        }
    }
}
