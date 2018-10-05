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


namespace Komunikacija
{
    public partial class PortSettingsForm : Form
    {
        string[] PortsName = SerialPort.GetPortNames();
        public PortSettingsForm()
        {
            InitializeComponent();
        }

        private void PortSettingsForm_Load(object sender, EventArgs e)
        {
            listActivePort.AutoSize = true;

            foreach (String Port in PortsName)
            { listActivePort.Items.Add(Port); }

            if (listActivePort.Items.Count < 1)
            {
                listActivePort.Visible = false;
                labelCurrently.Text = "Active ports : NAN !";
                labelCurrently.ForeColor = System.Drawing.Color.Red;
            }

            if (Form1.CurrPort == null)
            {
                labelConnected.Text = "Device not connected";
                labelConnected.ForeColor = System.Drawing.Color.Red;
                
            }
            else
            {
                labelConnected.Text = "Device connected to port " + Form1.CurrPort;
                labelConnected.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.FindPort = false;
            Cursor.Current = Cursors.WaitCursor;

            if (Form1.sserialPort.IsOpen)
                Form1.sserialPort.Close();

            PortsName = SerialPort.GetPortNames();

            Refresh();

            foreach (string Port in PortsName)
            {
                try
                {
                    if (!Form1.FindPort)
                    {
                        Form1.sserialPort.PortName = Port;

                        if(!Form1.sserialPort.IsOpen)
                         Form1.sserialPort.Open(); 

                        if (Form1.sserialPort.IsOpen)
                        {

                            Form1.sserialPort.Write("AA//");
                             Refresh();
                             Uart_wait(4000);
                            if (Form1.Resive == "AA")
                            {
                                labelConnected.Text = "Device connected to port " + Port;
                                labelConnected.ForeColor = System.Drawing.Color.Green;
                                Form1.FindPort = true;
                                Form1.Status = "Connected";
                                Form1.CurrPort = Port;
                                break;
                            }
                            else
                            {
                                labelConnected.Text = "Device not connected";
                                labelConnected.ForeColor = System.Drawing.Color.Red;

                                if(Form1.sserialPort.IsOpen)
                                Form1.sserialPort.Close();

                                Form1.Status = "Not connected";
                                Form1.CurrPort = null;
                            } 
                        }
                    }
                }
                catch
                {
                    labelConnected.Text = "Device not connected";
                    labelConnected.ForeColor = System.Drawing.Color.Red;
                    Form1.CurrPort = null;
                    Form1.Status = "Not connected";
                }
            }
            Cursor.Current = Cursors.Default;
        }

        public void Uart_wait(int maxTime)
        {
            int count = 0;
            Form1.TikTac = true;
            Form1.DataSerial = false;
            while (!Form1.DataSerial && Form1.TikTac)
            {
                count++;
                if (count > maxTime) Form1.TikTac = false;
                else Form1.TikTac = true;
                System.Threading.Thread.Sleep(1);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.Status == "Connected")
            {
                Form1.sserialPort.Write("TEST//");
                Uart_wait(3000);
                if (Form1.Resive == "TEST")
                    MessageBox.Show("The connected is OK !", "Status connected");
                else
                    MessageBox.Show("The connected is BROKEN !","Status connected");
            }
            else
                MessageBox.Show("The connected is BROKEN !", "Status unconnected");
        }
    }
}

