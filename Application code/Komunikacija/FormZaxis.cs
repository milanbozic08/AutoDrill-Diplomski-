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
    public partial class FormZaxis : Form
    {
        public FormZaxis()
        {
            InitializeComponent();
        }

        private void vScrollBarZaxis_ValueChanged(object sender, EventArgs e)
        {
            int value = 90 - vScrollBarZaxis.Value;
             Form1.sserialPort.Write("ZO"+ value.ToString().PadLeft(2, '0')+"00//");
            Console.WriteLine("ZO" + value.ToString().PadLeft(2,'0'));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            { hScrollBar1.Enabled = true; }
            else
            {
                hScrollBar1.Enabled = false;
                Form1.sserialPort.Write("GLM100//");
            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("GLM" + hScrollBar1.Value.ToString().PadLeft(3, '0') + "//");
            Form1.sserialPort.Write("GLM"+hScrollBar1.Value.ToString().PadLeft(3,'0')+"//");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.SPUP = 90 - vScrollBarZaxis.Value;
        }

        private void SPDOWNbutton_Click(object sender, EventArgs e)
        {
            Form1.SPDOWN = 90 - vScrollBarZaxis.Value;
        }
    }
}
