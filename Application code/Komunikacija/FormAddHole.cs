using System;
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
    public partial class FormAddHole : Form
    {
        public FormAddHole()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void FormAddHole_Load(object sender, EventArgs e)
        {
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NCdrillForm.NoviPrecnikRupe = Double.Parse(textBox1.Text.Replace(',','.'));
                NCdrillForm.NoviXRupe = (int)((Double.Parse(textBox2.Text.Replace(',', '.'))) * 1000);
                NCdrillForm.NoviYRupe = (int)((Double.Parse(textBox3.Text.Replace(',', '.'))) * 1000);
                NCdrillForm.ImaNovaRupa = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Incorrect data", "Error");
            }
            this.Close();
        }
    }
}
