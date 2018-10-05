using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Timers;

namespace Komunikacija
{
    public partial class Form1 : Form
    {
        public static bool FindPort = false;
        public static string CurrPort = null;
        public static string Status = "Not connected";
        public static SerialPort sserialPort = new SerialPort();
        public static bool TikTac = true;
        public static bool DataSerial = false;
        public static string Resive;
        int H, W;
        public static int Pozicija=0;
        public static int SPUP = 55,SPDOWN = 35;

        public Form1()
        {   
            InitializeComponent();
            sserialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        private void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            DataSerial = true;
            Resive = sserialPort.ReadExisting();
            Console.WriteLine("__"+Resive +"__");

            if(Resive == "READY")
            {
                if(Pozicija==1)
                {
                    NCdrillForm.PocetneKordinate(NCdrillForm.FizickaDuzinaSajleA, NCdrillForm.FizickaDuzinaSajleB);                  
                }

                if(NCdrillForm.KomandeBool)
                switch (Pozicija)
                {
                    case 0:
                        Form1.sserialPort.Write(NCdrillForm.KomandeLista[0].GetKomanda());
                        NCdrillForm.FizickaDuzinaSajleA = NCdrillForm.KomandeLista[0].GetDuzinaA();
                        NCdrillForm.FizickaDuzinaSajleB = NCdrillForm.KomandeLista[0].GetDuzinaB();

                        NCdrillForm.IzbuseneRupe.Add(NCdrillForm.KomandeLista[0]);

                        NCdrillForm.KomandeLista.RemoveAt(0);
                        Pozicija = 1;
                        break;
                    case 1:
                            int Value = Convert.ToInt32(NCdrillForm.brzinamotora.Split('%')[0]) + 100;
                           // if (Value > 99 && Value < 201)
                             //   Form1.sserialPort.Write("GLM" + Value.ToString().PadLeft(3, '0') + "//");
                             // Slepi obe komande u jednu ....
                            Form1.sserialPort.Write("ZO"+SPDOWN.ToString().PadLeft(2,'0')+"//"); // Spust burgiju
                            Console.WriteLine("SpustiBurgiju");
                        Pozicija = 2;
                        break;

                    case 2:
                        Form1.sserialPort.Write("ZO"+ SPUP.ToString().PadLeft(2, '0')+"//"); // Podigni burgiju
                        Pozicija = 0;

                        if (NCdrillForm.KomandeLista.Count == 0)
                        {
                            NCdrillForm.KomandeBool = false;
                            Form1.sserialPort.Write("GLM101//");
                        }
                        break;
                }

            }
        }
        
        private void Form1_Load_1(object sender, EventArgs e)
        {
            string[] PortsName = SerialPort.GetPortNames();

            sserialPort.BaudRate = 38400;
            sserialPort.Parity = Parity.None;
            sserialPort.StopBits = StopBits.One;
            sserialPort.Handshake = Handshake.None;
            sserialPort.ReadTimeout = 200;

            label1.Text = Status;

            foreach (string Port in PortsName)
            {
                try
                {
                    if (!FindPort)
                    {
                        sserialPort.PortName = Port;

                        if(!sserialPort.IsOpen)
                             sserialPort.Open(); 

                        if (sserialPort.IsOpen)
                        {
                            sserialPort.Write("AA//");
                            Refresh();
                            Uart_wait(4000);
                            if (Resive.Trim() == "AA")
                            {
                                Status = "Connected";
                                label1.Text = Status;
                                label1.ForeColor = System.Drawing.Color.Green;
                                FindPort = true;
                                Form1.CurrPort = Port;
                                break;
                            }
                            else
                            {
                                label1.ForeColor = System.Drawing.Color.Red;

                                if (sserialPort.IsOpen)
                                    sserialPort.Close();

                                Form1.CurrPort = null;
                            }
                        }
                    }
                }
                catch  {     }
            }    
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            label1.Text = Status;

            if(Status=="Connected")
                label1.ForeColor = System.Drawing.Color.Green;
            else
                label1.ForeColor = System.Drawing.Color.Red;
        }

        public static void Uart_wait(int maxTime)
        {
            int count = 0;
            TikTac = true;
            while (!DataSerial && TikTac)
            {
                count++;
                if (count > maxTime) TikTac = false;
                else TikTac = true;
                System.Threading.Thread.Sleep(1);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PictureBox pictureBox = new PictureBox();
                Bitmap image = new Bitmap(openFileDialog.FileName);
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Image = (Image)image;
                Controls.Add(pictureBox);
                H = image.Height;
                W = image.Width;
            }
            this.ClientSize = new Size(W, H);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PortSettingsForm Portsetting = new PortSettingsForm();
            Portsetting.ShowDialog();
            Portsetting.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sserialPort.Close();
            NCdrillForm PhotoEditor = new NCdrillForm();

            PhotoEditor.ShowDialog();
            PhotoEditor.Dispose();
        }

    }

}




