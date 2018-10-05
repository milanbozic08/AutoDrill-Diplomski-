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
    
    public partial class FormPosition : Form
    {

        public FormPosition()
        {
            InitializeComponent();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            int speed = Convert.ToInt32(textBox1.Text);

            if(speed>99 && speed<401)
            Form1.sserialPort.Write("NS1"+speed.ToString()+"//");  
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.sserialPort.Write("SM1//");
            Refresh();
            Form1.Uart_wait(3000);
            Refresh();
            Console.WriteLine("Motor 1: " + Form1.Resive);
            NCdrillForm.TrenutnaDuzinaSajleA += (Convert.ToInt32(Form1.Resive) * 31.415 / 4096);
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            int speed = Convert.ToInt32(textBox1.Text);

            if (speed > 99 && speed < 401)
                Form1.sserialPort.Write("BS1" + speed.ToString() + "//");
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.sserialPort.Write("SM1//");
            Refresh();
            Form1.Uart_wait(3000);
            Refresh();
            Console.WriteLine("Motor 1: " + Form1.Resive);
            NCdrillForm.TrenutnaDuzinaSajleA -= (Convert.ToInt32(Form1.Resive) * 31.415 / 4096);

        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            int speed = Convert.ToInt32(textBox1.Text);

            if (speed > 99 && speed < 401)
                Form1.sserialPort.Write("BS2" + speed.ToString() + "//");
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.sserialPort.Write("SM2//");
            Refresh();
            Form1.Uart_wait(3000);
            Refresh();
            Console.WriteLine("Motor 2: " + Form1.Resive);
            NCdrillForm.TrenutnaDuzinaSajleB -= (Convert.ToInt32(Form1.Resive) * 31.415 / 4096);
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            int speed = Convert.ToInt32(textBox1.Text);

            if (speed > 99 && speed < 401)
                Form1.sserialPort.Write("NS2" + speed.ToString() + "//");
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.sserialPort.Write("SM2//");
            Refresh();
            Form1.Uart_wait(3000);
            Refresh();
            Console.WriteLine("Motor 2: "+Form1.Resive);
            NCdrillForm.TrenutnaDuzinaSajleB += (Convert.ToInt32(Form1.Resive) * 31.415 / 4096.0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.sserialPort.Write("STR//");
            }
            catch { }
            NCdrillForm.TrenutnaDuzinaSajleA = 48;
            NCdrillForm.TrenutnaDuzinaSajleB = 26;
            NCdrillForm.FizickaDuzinaSajleAOduzimanje = 0;
            NCdrillForm.FizickaDuzinaSajleBOduzimanje = 0;

        }

        private void FormPosition_MouseDown(object sender, MouseEventArgs e)
        {
            NCdrillForm.PocetneKordinate(NCdrillForm.TrenutnaDuzinaSajleA, NCdrillForm.TrenutnaDuzinaSajleB);
            KoordX.Text = NCdrillForm.PocetnaKoordinataX.ToString();
            KoordY.Text = NCdrillForm.PocetnaKoordinataY.ToString();
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            int value = 90 - vScrollBar1.Value;
            Form1.sserialPort.Write("ZO" + value.ToString().PadLeft(2, '0') + "//");
            Console.WriteLine("ZO" + value.ToString().PadLeft(2, '0'));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NCdrillForm.SiftPositionX = NCdrillForm.PocetnaKoordinataX/1000;
            NCdrillForm.SiftPositionY = NCdrillForm.PocetnaKoordinataY/1000;

            NCdrillForm.KomandeLista.Clear();
            NCdrillForm.KomandeBool = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AutoClosingMessageBox.Show("Wait please...", "Message", 500);

            Kordinate TMPP = new Kordinate(0, 0);

            NCdrillForm.OdrediBrojeveKoraka(TMPP);

            try
            {
                Form1.sserialPort.Write("MA" + NCdrillForm.MotorABrojKoraka.ToString().PadLeft(5, '0') + textBox1.Text.PadLeft(3,'0') + NCdrillForm.SmerMotoraA + NCdrillForm.MotorBBrojKoraka.ToString().PadLeft(5, '0') + textBox1.Text.PadLeft(3, '0') + NCdrillForm.SmerMotoraB + "//");
                NCdrillForm.FizickaDuzinaSajleA =NCdrillForm.TrenutnaDuzinaSajleA;
                NCdrillForm.FizickaDuzinaSajleB = NCdrillForm.TrenutnaDuzinaSajleB;
                Form1.Pozicija = 1;
            }
            catch
            {
               AutoClosingMessageBox.Show("Error", "Error", 1000);
            }
        }
    }
}
