using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Komunikacija
{
    public partial class EditorForm : Form
    {
        
        public EditorForm()
        {
            InitializeComponent();
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = File.ReadAllText(NCdrillForm.path);
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = 0;
            }
            catch
            {
                textBox1.Text = "Please open the file !";
                textBox1.ForeColor = System.Drawing.Color.Red;
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = 0;
            }
        }
    }
}
