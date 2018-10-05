using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Math;




namespace Komunikacija
{
    public partial class NCdrillForm : Form
    {
        public static Kordinate MomentCordDrill = new Kordinate(0, 0);

        private Graphics g1;
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        double RulerRes = 2;
        double Scale = 1;

        public static double NosacA = 135.5;
        public static double GredaA = 176.8;
        public static double GredaB = 204;
        public static double VisakA1 = 47.5;
        public static double VisakA2 = 46.5;
        public static double NormalnoA1 = 28.9;
        public static double NormalnoA2 = 16.5;
        public static double VisakB = 79;
        public static double NormalnoB = 14.3;

        public static bool speedSend = false;

        public static double IzracunatUgaoAlfa = 0;
        public static double IzracunatUgaoTeta = 0;

        public static double SiftPositionX = 0, SiftPositionY = 0;  // Ovo treba sabirati sa Tackama da se dobije tacna pozicija

        public static double TrenutnaDuzinaSajleA=0; // mora se pre pocetka ruka dovesti u pocetni polozaj !!
        public static double TrenutnaDuzinaSajleB=0;

        public static double FizickaDuzinaSajleA = 0;
        public static double FizickaDuzinaSajleB = 0;

        public static double FizickaDuzinaSajleAOduzimanje = 0;
        public static double FizickaDuzinaSajleBOduzimanje = 0;

        public static int MotorABrojKoraka = 0;
        public static int MotorBBrojKoraka = 0;

        public static char SmerMotoraA = 'X';
        public static char SmerMotoraB = 'X';

        public static bool TackaValid = false;

        public static double NoviPrecnikRupe;
        public static int NoviXRupe, NoviYRupe;
        public static bool ImaNovaRupa = false;

        public static string brzinamotora = "100%";

       // public static int DrilingTime = 1500;

        Kordinate TrenutneKordinate = new Kordinate(0,0);

        double Precnik;     // Ovde se cuva Precnik kada se selektuje Node
        bool SelektovanNodeD = false;

        List<rupa> Rupe = new List<rupa>();

        public static List<KomandeClass> IzbuseneRupe = new List<KomandeClass>();

        //public static List<string> Komande = new List<string>();
        public static List<KomandeClass> KomandeLista = new List<KomandeClass>();
        public static bool KomandeBool = false;

        public static String path="";
        bool RullerState = false;
        bool AxesState = true;
        bool HolesState = true;
        bool DrvoState = false;

        public static double AlfaPocetno = 0, TetaPocetno=0;

        public static double PocetnaKoordinataX = 0, PocetnaKoordinataY=0;

        public NCdrillForm()
        {
            InitializeComponent();

            this.pictureBox1.MouseWheel += pictureBox1_MouseWheel;


        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            
            toolStripTextBoxZoom.Text = (Convert.ToDouble(toolStripTextBoxZoom.Text) + (e.Delta)/1200.0).ToString();
            ProveriIscrtavanja();
        }

        private void viewNCDrilltxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorForm Forma = new EditorForm();
            Forma.ShowDialog();
        }

        private void openNCDRILLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    path = openFileDialog.FileName;

                }
                catch { }
                CitanjeFajla();

                ProveriIscrtavanja();             
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            path = "";
            Rupe.Clear();
            IzbuseneRupe.Clear();
            KomandeBool = false;
            KomandeLista.Clear();

            ProveriIscrtavanja();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearImage()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            g1 = Graphics.FromImage(this.pictureBox1.Image);
        }

        private void NCdrillForm_Load(object sender, EventArgs e)
        {
            PocetneKordinate(38,24);

            try
            {
                Form1.sserialPort.Open();
                toolStripStatusLabelStatus.Text = "Connected";
                button1.Enabled = true;

            }
            catch
            {
                MessageBox.Show("Device not connected !", "Warning");
            }

            ProveriIscrtavanja();
        }

        public int KordinateX( int x)
        {
            return (x+25);
        }

        public int KordinateY (int y)
        {
            return (490-y);
        }

        public int REKordinateX(int x)
        {
            return (x - 25);
        }

        public int REKordinateY(int y)
        {
            return (490 - y);
        }

        public void Ose()
        {
            Pen p = new Pen(Color.Black, 1);

            g1.DrawLine(p, KordinateX(0), KordinateY(-25), KordinateX(0), KordinateY(520));
            g1.DrawLine(p, KordinateX(-25), KordinateY(0), KordinateX(700), KordinateY(0));
        }

        public void Ruler()
        {

            Pen p = new Pen(Color.Black, 1);
            Font drawFont = new Font("Segoe UI", 7);

            // po y osi
            for (int y = 0; y < 21*20+5; y = y+ (int)(RulerRes))
            {
                string tmp = ( y / Convert.ToDouble(toolStripTextBoxZoom.Text) / (RulerRes )).ToString();
                if (tmp.Length > 5)
                    tmp =  (y / Convert.ToDouble(toolStripTextBoxZoom.Text) / RulerRes).ToString().Substring(0, 4);
                
               // g1.DrawLine(p, KordinateX(0), KordinateY(y), KordinateX(-10), KordinateY(y));

                    if ((y % 20) == 0)
                    {
                        g1.DrawLine(p, KordinateX(0), KordinateY(y), KordinateX(-10), KordinateY(y));
                        g1.DrawString(tmp, drawFont, myBrush, KordinateX(-25), KordinateY(y + 5));
                    }
                
                     else
                         g1.DrawLine(p, KordinateX(0), KordinateY(y), KordinateX(-5), KordinateY(y));
            }

            // po x osi
            for (int x = 0; x < 27 * 20 + 5; x = x + (int)(RulerRes))
            {
                string tmp = (x / Convert.ToDouble(toolStripTextBoxZoom.Text) / (RulerRes)).ToString();
                if (tmp.Length > 5)
                    tmp = (x / Convert.ToDouble(toolStripTextBoxZoom.Text) / RulerRes).ToString().Substring(0, 4);


                if ((x % 20) == 0)
                {
                    g1.DrawLine(p, KordinateX(x), KordinateY(0), KordinateX(x), KordinateY(-10));
                    g1.DrawString(tmp, drawFont, myBrush, KordinateX(x-5), KordinateY(-10));
                }

                else
                    g1.DrawLine(p, KordinateX(x), KordinateY(0), KordinateX(x), KordinateY(-5));
            }
        }

        private void mmToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (mmToolStripMenuItem.Checked)
            {
                cmToolStripMenuItem.Checked = false;
                RulerRes = 2;

                ProveriIscrtavanja();
            }
        }

        private bool CitanjeFajla ()
        {
            string line;
            System.IO.StreamReader file;

            if (path != "")
                file = new System.IO.StreamReader(path);
            else
            {
                MessageBox.Show("Insert NC File", "Error");
                return false;
            }

            line = file.ReadLine();


            // Start File
            if(line!="M48")
            {
                MessageBox.Show("Incorrect File", "Error");
                return false;
            }

            line = file.ReadLine();
            line = file.ReadLine();

            // Resolution
           if(line[line.Length-3]=='4' && line[line.Length - 1] == '2')
                Scale=10;
           else if (line[line.Length - 3] == '4' && line[line.Length - 1] == '3')
                Scale = 1;
           else if (line[line.Length - 3] == '4' && line[line.Length - 1] == '4')
                Scale = 0.1;
            else
            {
                MessageBox.Show("Resolution Error", "Error");
                return false;
            }

            // Sve tačke
            line = file.ReadLine();
            line = file.ReadLine();

            string[] spliter;
            double prec;

            while ((line = file.ReadLine())!="%")
            {
                 spliter = line.Split('C');
                // spliter[1] = spliter[1].Replace('.',',');

                 prec = Double.Parse(spliter[1]);
                 spliter = spliter[0].Split('F');
                 rupa R1 = new rupa(prec,Convert.ToInt32(spliter[0].Substring(1)));
                 Rupe.Add(R1);
            }

            line = file.ReadLine();
        Restart:
            if (line[0] == 'T')
                foreach (rupa value in Rupe)

                {
                    if (value.GetRupaIme() == Convert.ToInt32(line.Substring(1)))
                    {
                        line = file.ReadLine();
                        while (line[0] != 'T' && line[0] != 'M')
                        {
                            spliter = line.Split('Y');
                            int KordinataY = Convert.ToInt32(spliter[1]);
                            spliter = spliter[0].Split('X');
                            value.AddKordinata((int)(Convert.ToInt32(spliter[1]) * Scale), (int)(KordinataY * Scale));


                            line = file.ReadLine();
                        }
                        goto Restart;
                    }
                }
            else
                if (line == "M30")
                AutoClosingMessageBox.Show("File is Read :)", "Message", 500);
            else
                MessageBox.Show("File Error !", "Error");


            return true;
        }

        private void cmToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (cmToolStripMenuItem.Checked)
            {
                mmToolStripMenuItem.Checked = false;
                RulerRes = 20;

                ProveriIscrtavanja();
            }
        }

        private void rulerToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rulerToolStripMenuItem.Checked)
            {
                RullerState = true;

                ProveriIscrtavanja();
            }
            else
            {
                RullerState = false;

                ProveriIscrtavanja();
            }
          

        }

        void IscrtajTacke ()
        {
            Pen p = new Pen(Color.Black, 1);

            foreach (rupa Value in Rupe)
                foreach (Kordinate Val in Value.Kordinata)
                {
                    g1.DrawEllipse(p, KordinateX((int)(Val.GetX() / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text)-Value.GetRupaPrecnik()*4/2)), KordinateY((int)(Val.GetY() / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) + Value.GetRupaPrecnik() * 4 / 2)), (float)Value.GetRupaPrecnik() * 4, (float)Value.GetRupaPrecnik() * 4);
                }
            try
            {
                PocetneKordinate(FizickaDuzinaSajleA, FizickaDuzinaSajleB);
                p = new Pen(Color.Green, 3);
                g1.DrawLine(p, KordinateX((int)(PocetnaKoordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) )), KordinateY((int)(PocetnaKoordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) - 4)), KordinateX((int)(PocetnaKoordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) )), KordinateY((int)(PocetnaKoordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) + 4)));
                g1.DrawLine(p, KordinateX((int)(PocetnaKoordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) - 4)), KordinateY((int)(PocetnaKoordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) )), KordinateX((int)(PocetnaKoordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) + 4)), KordinateY((int)(PocetnaKoordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) )));
            }
            catch { }
        }

        public void TreeViewRefresh()
        {
            treeView1.Visible = true;
            label1.Visible = true;
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            foreach (rupa value in Rupe)
            {
                treeView1.Nodes.Add(("DIAMETER : "+value.GetRupaPrecnik().ToString()+"mm"));

                foreach (Kordinate val in value.Kordinata)
                {
                    treeView1.Nodes[Rupe.IndexOf(value)].Nodes.Add(
                    new TreeNode("X:" + Convert.ToString(val.GetX()) + " Y:" + Convert.ToString(val.GetY())));
                }
            }
            treeView1.EndUpdate();
        }

        private void viewHolesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (viewHolesToolStripMenuItem.Checked)
            {
                HolesState = true;

                ProveriIscrtavanja();              
            }
            else
            {
                HolesState = false;

                ProveriIscrtavanja();
            }
        }

        private void shiftIn00ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int minx, miny;

                minx = Rupe[0].Kordinata[0].GetX();
                miny = Rupe[0].Kordinata[0].GetY();

                foreach (rupa Value in Rupe)
                    foreach (Kordinate Val in Value.Kordinata)
                    {
                        if (Val.GetX() < minx) minx = Val.GetX();
                        if (Val.GetY() < miny) miny = Val.GetY();
                    }
                foreach (rupa Value in Rupe)
                    foreach (Kordinate Val in Value.Kordinata)
                    {
                        Val.SetX(Val.GetX() - minx);
                        Val.SetY(Val.GetY() - miny);
                    }
                AutoClosingMessageBox.Show("Succsesful", "Message", 500);

                ProveriIscrtavanja();
            }
            catch
            {
                MessageBox.Show("Fail Shift !", "Error");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            if (e.Node.Text[0] == 'D') // diameter
            {
                SelektovanNodeD = true;

                string[] spliter;
                spliter = e.Node.Text.Split(':');
                spliter = spliter[1].Split('m');
                Precnik = Convert.ToDouble( spliter[0].Trim());

                Pen p = new Pen(Color.Red, 2);
                ProveriIscrtavanja(true);

                foreach (rupa Value in Rupe)
                {
                    if(Value.GetRupaPrecnik()==Precnik)
                    {
                        foreach(Kordinate Val in Value.Kordinata)
                        {
                            g1.DrawEllipse(p, KordinateX((int)(Val.GetX() / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) - Precnik * 4 / 2)), KordinateY((int)(Val.GetY() / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) + Precnik * 4 / 2)), (float)Precnik * 4, (float)Precnik * 4);
                        }
                    }
                }
            }

            if (e.Node.Text[0] == 'X')      // X cordinata
            {
                SelektovanNodeD = false;
               
                string selectedNodeText = e.Node.Text;
                string[] Spliter = selectedNodeText.Split('Y');
                int kordinataY = Convert.ToInt32(Spliter[1].Substring(1)); // Y kordinata selektovana
                Spliter = Spliter[0].Split('X');
                int kordinataX = Convert.ToInt32(Spliter[1].Substring(1)); // X kordinata selektovana
                double PrecnikRupe = 0;

                string[] spliter;
                string Precnik;

                spliter = e.Node.Parent.Text.Split(':');
                spliter = spliter[1].Split('m');
                Precnik = spliter[0].Trim();
                Precnik.Replace('.', ',');
                PrecnikRupe = Convert.ToDouble(Precnik); // Precnik rupe selektovane kordinate


                TrenutneKordinate.SetX(kordinataX);
                TrenutneKordinate.SetY(kordinataY);

                pictureBox1.Image = new Bitmap(pictureBox1.Height, pictureBox1.Width);
                g1 = Graphics.FromImage(this.pictureBox1.Image);

                ProveriIscrtavanja(true);

                Pen p = new Pen(Color.Red, 2);

                g1.DrawEllipse(p, KordinateX((int)(kordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) - PrecnikRupe * 4 / 2)), KordinateY((int)(kordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) + PrecnikRupe * 4 / 2)), (float)PrecnikRupe * 4, (float)PrecnikRupe * 4);

            }
        }

        // Dodavanje instrukcija u paajuci meni
        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        MouseEventArgs me = (MouseEventArgs)e;
                        System.Drawing.Point coordinates = me.Location;

                        ContextMenuStrip rightClickMenuStrip = new ContextMenuStrip();
                        rightClickMenuStrip.ItemClicked += menuStrip_ItemClicked;

                        if (!SelektovanNodeD)
                        {
                            rightClickMenuStrip.Items.Add("Delate Hole");
                            rightClickMenuStrip.Items.Add("Go To This Hole");
                        }
                        if(SelektovanNodeD)
                            rightClickMenuStrip.Items.Add("Drill This Holes");



                        rightClickMenuStrip.Show(this, coordinates.X+treeView1.Location.X, coordinates.Y + treeView1.Location.Y);//places the menu at the pointer position
                    }
                    break;
            }
        }

        // Desni klik na madajuci meni u prikazu rupa
        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Delate Hole")
            {
                foreach (rupa Value in Rupe)
                    for (int x = 0; x < Value.Kordinata.Count; x++)
                    {
                        if (TrenutneKordinate.GetX() == Value.Kordinata.ElementAt(x).GetX() && TrenutneKordinate.GetY() == Value.Kordinata.ElementAt(x).GetY())
                        {
                            Value.Kordinata.RemoveAt(x);

                            ProveriIscrtavanja();
                        }
                    }
            }
            if (e.ClickedItem.Text == "Drill This Hole")
            {
                AutoClosingMessageBox.Show("Wait please...", "Message",500);

                OdrediBrojeveKoraka(TrenutneKordinate);

                Console.WriteLine("Poslato uartom: " + "MA" + MotorABrojKoraka.ToString().PadLeft(5, '0') + "100" + SmerMotoraA + MotorBBrojKoraka.ToString().PadLeft(5, '0') + "100" + SmerMotoraB + "//");
                try
                {
                    Form1.sserialPort.Write("MA" + MotorABrojKoraka.ToString().PadLeft(5,'0') + toolStripTextBoxSpeed + SmerMotoraA + MotorBBrojKoraka.ToString().PadLeft(5, '0') + toolStripTextBoxSpeed + SmerMotoraB + "//");
                    FizickaDuzinaSajleA = TrenutnaDuzinaSajleA;
                    FizickaDuzinaSajleB = TrenutnaDuzinaSajleB;
                    Form1.Pozicija = 1;
                    KomandeBool = true;

                    KomandeClass NovaKomanda = new KomandeClass("", TrenutnaDuzinaSajleA, TrenutnaDuzinaSajleB); 

                    IzbuseneRupe.Add(NovaKomanda);
                    try
                    {
                        KomandeLista.Clear();
                    }
                    catch { }
                }
                catch
                {
                    toolStripStatusLabelStatus.Text = "Connection Error";
                }


            }

            if (e.ClickedItem.Text == "Go To This Hole")
            {
                AutoClosingMessageBox.Show("Wait please...", "Message", 500);

                OdrediBrojeveKoraka(TrenutneKordinate);

                Console.WriteLine("Poslato uartom: " + "MA" + MotorABrojKoraka.ToString().PadLeft(5, '0') + "100" + SmerMotoraA + MotorBBrojKoraka.ToString().PadLeft(5, '0') + "100" + SmerMotoraB + "//");
                try
                {
                    Form1.sserialPort.Write("MA" + MotorABrojKoraka.ToString().PadLeft(5, '0') + toolStripTextBoxSpeed + SmerMotoraA + MotorBBrojKoraka.ToString().PadLeft(5, '0') + toolStripTextBoxSpeed + SmerMotoraB + "//");
                    FizickaDuzinaSajleA = TrenutnaDuzinaSajleA;
                    FizickaDuzinaSajleB = TrenutnaDuzinaSajleB;
                    Form1.Pozicija = 1;
                }
                catch
                {
                    toolStripStatusLabelStatus.Text = "Connection Error";
                }


            }

            if (e.ClickedItem.Text == "Drill This Holes")
            {
                if (!NCdrillForm.speedSend)
                {
                    Form1.sserialPort.Write("GLM200//");
                    Thread.Sleep(300);
                }
                KomandeBool =true;
                foreach (rupa Value in Rupe)
                {
                    if (Value.GetRupaPrecnik() == Precnik)
                    {
                        foreach (Kordinate Val in Value.Kordinata)
                        {
                            OdrediBrojeveKoraka(Val);
                            string Temp = "MA" + MotorABrojKoraka.ToString().PadLeft(5, '0') + toolStripTextBoxSpeed.ToString().PadLeft(3,'0') + SmerMotoraA + MotorBBrojKoraka.ToString().PadLeft(5, '0') + toolStripTextBoxSpeed.ToString().PadLeft(3, '0') + SmerMotoraB + "//";
                            KomandeClass TMP = new KomandeClass(Temp, TrenutnaDuzinaSajleA, TrenutnaDuzinaSajleB);
                            KomandeLista.Add(TMP);
                        }
                    }
                }

                Form1.sserialPort.Write(KomandeLista[0].GetKomanda());
                NCdrillForm.FizickaDuzinaSajleA = NCdrillForm.KomandeLista[0].GetDuzinaA();
                NCdrillForm.FizickaDuzinaSajleB = NCdrillForm.KomandeLista[0].GetDuzinaB();
                ProveriIscrtavanja();

                IzbuseneRupe.Add(KomandeLista[0]);

                KomandeLista.RemoveAt(0);
                Form1.Pozicija = 1;
            }
        }
        
        public static void OdrediBrojeveKoraka(Kordinate TrenutneKordinate)
        {
            if (Odredi_uglove(TrenutneKordinate.GetX() / 10000.0, TrenutneKordinate.GetY() / 10000.0))
            {
                MotorABrojKoraka = BrojKorakaMotorA(DuzinaSajleA(IzracunatUgaoAlfa));
                MotorBBrojKoraka = BrojKorakaMotorB(DuzinaSajleB(IzracunatUgaoTeta));

                Console.WriteLine("Broj Koraka Motora A:  " + MotorABrojKoraka.ToString());
                Console.WriteLine("Broj Koraka Motora B:  " + MotorBBrojKoraka.ToString());
                TackaValid = true;
            }
            else
            {
                MessageBox.Show("These coordinates are not possible !!!", "Error");
                TackaValid = false;
            }
        }

        //Desni Klik na sliku
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            double PrecnikRupe = 0;
            int SelektovanaKordinataX = 0;
            int SelektovanaKordinataY = 0;

            MouseEventArgs me = (MouseEventArgs)e;
            System.Drawing.Point coordinates = me.Location;

            SelektovanaKordinataX = (int)(REKordinateX(me.X) * 500 / Convert.ToDouble(toolStripTextBoxZoom.Text));
            SelektovanaKordinataY = (int)(REKordinateY(me.Y) * 500 / Convert.ToDouble(toolStripTextBoxZoom.Text));

            foreach (rupa Value in Rupe)
                foreach (Kordinate Val in Value.Kordinata)
                {
                    if ((Val.GetX() + 1000) > SelektovanaKordinataX && (Val.GetX() - 1000) < SelektovanaKordinataX && (Val.GetY() + 1000) > SelektovanaKordinataY && (Val.GetY() - 1000) < SelektovanaKordinataY)
                    {
                        TrenutneKordinate.SetX(Val.GetX());
                        TrenutneKordinate.SetY(Val.GetY());
                        PrecnikRupe = Value.GetRupaPrecnik();
                    }
                }

            pictureBox1.Image = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            g1 = Graphics.FromImage(this.pictureBox1.Image);

            ProveriIscrtavanja(true);

            Pen p = new Pen(Color.Red, 2);

            g1.DrawEllipse(p, KordinateX((int)(TrenutneKordinate.GetX() / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) - PrecnikRupe * 4 / 2)), KordinateY((int)(TrenutneKordinate.GetY() / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) + PrecnikRupe * 4 / 2)), (float)PrecnikRupe * 4, (float)PrecnikRupe * 4);



            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        if (PrecnikRupe > 0)
                        {
                            ContextMenuStrip rightClickMenuStrip = new ContextMenuStrip();
                            rightClickMenuStrip.ItemClicked += menuStrip_ItemClicked;

                            rightClickMenuStrip.Items.Add("Delate Hole");
                            rightClickMenuStrip.Items.Add("Go To This Hole");
                            rightClickMenuStrip.Items.Add("Drill This Hole");

                            rightClickMenuStrip.Show(this, coordinates.X , coordinates.Y + rightClickMenuStrip.Height );

                        }
                    }
                    break;

                case MouseButtons.Left:
                    {
                        if (PrecnikRupe > 0)
                        {
                            labelDiameter.Text = "Diameter: " + PrecnikRupe.ToString() + " mm";
                            labelDiameter.Visible = true;
                            labelX.Text = "X Coordinate: " + TrenutneKordinate.GetX() + " um";
                            labelX.Visible = true;
                            labelY.Text = "Y Coordinate: " + TrenutneKordinate.GetY() + " um";
                            labelY.Visible = true;
                        }
                    }
                    break;
            }

        }

        private void axesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ClearImage();

            if(axesToolStripMenuItem.Checked)
            {
                AxesState = true;

                ProveriIscrtavanja();
            }
            else
            {
                AxesState = false;

                ProveriIscrtavanja();
            }
        }

        public void ProveriIscrtavanja(bool NeDirajDrvo=false)  // ako je "true" nece se drvo refresovati !
        {
            ClearImage();

            if (AxesState)
                Ose();

            if (RullerState)
                Ruler();
            if (NeDirajDrvo == false)
            {
                if (DrvoState)
                {
                    TreeViewRefresh();
                    treeView1.Visible = true;
                }
                else treeView1.Visible = false;
            }

            if (HolesState)
                IscrtajTacke();

            CrtajIzbuseneRupe();
            
        }

        void CrtajIzbuseneRupe()
        {
            Pen p1 = new Pen(Color.Red, 1);
            foreach (KomandeClass Value in IzbuseneRupe)
            {
                try
                {
                    PocetneKordinate(Value.GetDuzinaA(), Value.GetDuzinaB());

                    g1.DrawLine(p1, KordinateX((int)(PocetnaKoordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text))), KordinateY((int)(PocetnaKoordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) - 4)), KordinateX((int)(PocetnaKoordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text))), KordinateY((int)(PocetnaKoordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) + 4)));
                    g1.DrawLine(p1, KordinateX((int)(PocetnaKoordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) - 4)), KordinateY((int)(PocetnaKoordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text))), KordinateX((int)(PocetnaKoordinataX / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text) + 4)), KordinateY((int)(PocetnaKoordinataY / 500 * Convert.ToDouble(toolStripTextBoxZoom.Text))));
                }
                catch { }
            }
        }

        private void toolStripMenuItemTreeView_CheckedChanged(object sender, EventArgs e)
        {
            if(toolStripMenuItemTreeView.Checked)
            {
                DrvoState = true;

                ProveriIscrtavanja();
            }
            else
            {
                DrvoState = false;

                ProveriIscrtavanja();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ProveriIscrtavanja();
        }

        private void NCdrillForm_Activated(object sender, EventArgs e)
        {
            bool PostojiRupa = false;

            if (ImaNovaRupa)
            {
                ImaNovaRupa = false;

                foreach (rupa Val in Rupe)
                {
                    if (Val.GetRupaPrecnik() == NoviPrecnikRupe)
                    {
                        PostojiRupa = true;
                        Val.AddKordinata(NoviXRupe, NoviYRupe);
                    }
                }
                if(!PostojiRupa)
                {
                    rupa RU = new rupa(NoviPrecnikRupe, Rupe.Count + 1);
                    RU.AddKordinata(NoviXRupe, NoviYRupe);
                    Rupe.Add(RU);
                }
            }
            ProveriIscrtavanja();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            labelY.Visible = false;
            labelX.Visible = false;
            labelDiameter.Visible = false;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormZaxis Forma1 = new FormZaxis();
            Forma1.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FormHelp Forma = new FormHelp();
            Forma.Show();
        }

        private void addHoleToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            FormAddHole FormADDHOLE = new FormAddHole();
            FormADDHOLE.Show();             
        }

        public static bool Odredi_uglove(double KooorX , double KooorY)
        {
            double A = 17.9; // mm Prva greda
            double B = 20.5; // mm Druga greda

            A = GredaA/10;
            B = GredaB/10;

            double KoorX = KooorX + SiftPositionX/10;
            double KoorY = KooorY + SiftPositionY/10;

            double Alfa1, Alfa2, Teta1, Teta2;

            Alfa1 = -2 * Math.Atan((Math.Sqrt(-A * A * A * A + 2 * A * A * B * B + 2 * A * A * KoorX * KoorX + 2 * A * A * KoorY * KoorY - B * B * B * B + 2 * B * B * KoorX * KoorX + 2 * B * B * KoorY * KoorY - KoorX * KoorX * KoorX * KoorX - 2 * KoorX * KoorX * KoorY * KoorY - KoorY * KoorY * KoorY * KoorY) - 2 * A * KoorY) / (A * A + 2 * A * KoorX - B * B + KoorX * KoorX + KoorY * KoorY));
            if (Alfa1 < 0) Alfa1 *= -1;
            Alfa2 = -2 * Math.Atan((Math.Sqrt(-A * A * A * A + 2 * A * A * B * B + 2 * A * A * KoorX * KoorX + 2 * A * A * KoorY * KoorY - B * B * B * B + 2 * B * B * KoorX * KoorX + 2 * B * B * KoorY * KoorY - KoorX * KoorX * KoorX * KoorX - 2 * KoorX * KoorX * KoorY * KoorY - KoorY * KoorY * KoorY * KoorY) + 2 * A * KoorY) / (A * A + 2 * A * KoorX - B * B + KoorX * KoorX + KoorY * KoorY));
            if (Alfa2 < 0) Alfa2 *= -1;
            Teta1 = 2 * Math.Atan((2 * B * KoorY + Math.Sqrt((A * A + 2 * A * B + B * B - KoorX * KoorX - KoorY * KoorY) * (-A * A + 2 * A * B - B * B + KoorX * KoorX + KoorY * KoorY))) / (-A * A + B * B + 2 * B * KoorX + KoorX * KoorX + KoorY * KoorY));  
            Teta2 = 2 * Math.Atan((2 * B * KoorY - Math.Sqrt((A * A + 2 * A * B + B * B - KoorX * KoorX - KoorY * KoorY) * (-A * A + 2 * A * B - B * B + KoorX * KoorX + KoorY * KoorY))) / (-A * A + B * B + 2 * B * KoorX + KoorX * KoorX + KoorY * KoorY)); 

           double Teta1p = Math.PI - Alfa1 + Teta1; // uglovi izmedju greda
           double Teta2p = Math.PI - Alfa2 + Teta2;

            Console.WriteLine("Rezultat:");
            Console.WriteLine("Alfa1: " + (Alfa1*180/3.1415).ToString() + "  Alfa2: " + (Alfa2 * 180 / 3.1415).ToString());
            Console.WriteLine("Teta1: " + (Teta1p * 180 / 3.1415).ToString() + "  Teta2: " + (Teta2p * 180 / 3.1415).ToString());

            //provera

            double x1 = A * Math.Cos(Alfa1) + B * Math.Cos(Teta1);
            double x2 = A * Math.Cos(Alfa2) + B * Math.Cos(Teta2);
            double y1 = A * Math.Sin(Alfa1) + B * Math.Sin(Teta1);
            double y2 = A * Math.Sin(Alfa2) + B * Math.Sin(Teta2);

            if ((Alfa1 * 180 / 3.1415) > 60 && (Alfa1 * 180 / 3.1415) < (180-26) && (Teta1p * 180 / 3.1415) > 30 && (Teta1p * 180 / 3.1415) < 120)
            {
                Console.WriteLine("Resenje 1 je validno X1:" + x1.ToString() + "  Y1:" + y1.ToString());
                IzracunatUgaoAlfa = Alfa1;
                IzracunatUgaoTeta = Teta1p;
                return true;
            }

            if ((Alfa2 * 180 / 3.1415) > 60 && (Alfa2 * 180 / 3.1415) < (180-26) && (Teta2p * 180 / 3.1415) > 30 && (Teta2p * 180 / 3.1415) < 120)
            {
                Console.WriteLine("Resenje 2 je validno X2:" + x2.ToString() + "  Y2:" + y2.ToString());
                IzracunatUgaoAlfa = Alfa2;
                IzracunatUgaoTeta = Teta2p;
                return true;
            }

            return false;
        }

        public static double DuzinaSajleA(double UgaoLRad)  // -u radijanima !!
        {
            //double GredaA = 179;
            double GredaAVisak = 40;
            double GredaANormalno = 13.5;
            double Nosac = 142;

            GredaAVisak = VisakA2;
            GredaANormalno = NormalnoA2;
            Nosac = NosacA;
            

            double StatickaStranica = Math.Sqrt(((GredaA - GredaAVisak) * (GredaA - GredaAVisak) + GredaANormalno * GredaANormalno));
            double StatickiUgaoRad = Math.Acos((GredaA - GredaAVisak) / StatickaStranica);

            double UgaoPrim = Math.PI - StatickiUgaoRad - UgaoLRad;

            double DuzinaSajleAVal = Math.Sqrt(Nosac * Nosac + StatickaStranica * StatickaStranica - 2 * Nosac * StatickaStranica * Math.Cos(UgaoPrim));

            Console.WriteLine("Duzina Sajle A: "+DuzinaSajleAVal.ToString()+ " mm");

            return DuzinaSajleAVal; // u mm
        }

        private void goToStartPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrenutnaDuzinaSajleA = 38;  // Tacno upisi !!!
            TrenutnaDuzinaSajleB = 24;

            FizickaDuzinaSajleA = 38;
            FizickaDuzinaSajleB = 24;

            Form1.sserialPort.Write("STR//");

            NCdrillForm.KomandeLista.Clear();
            NCdrillForm.KomandeBool = false;
        }

        public static double DuzinaSajleB(double UgaoTrad) // ugao u radijanima !! Ugao izmedju Grede A i Grede B
        {
           // double GredaA = 179;
           // double GredaB = 205;
            double GredaAVisak = 47;
            double GredaBVisak = 73;
            double GredaANormalno = 28;
            double GredaBNormalno = 13.5;

            GredaAVisak = VisakA1;
            GredaBVisak = VisakB;
            GredaANormalno = NormalnoA1;
            GredaBNormalno = NormalnoB;

            double StatickaStranicaA = Math.Sqrt( (GredaA - GredaAVisak)* ( GredaA - GredaAVisak) + GredaANormalno * GredaANormalno );
            double StatickaStranicaB = Math.Sqrt( (GredaB - GredaBVisak) * (GredaB - GredaBVisak) + GredaBNormalno * GredaBNormalno );

            double StatickiUgaoA = Math.Acos((GredaA - GredaAVisak) / StatickaStranicaA);
            double StatickiUgaoB = Math.Acos((GredaB - GredaBVisak) / StatickaStranicaB);

            double UgaoPrim = UgaoTrad - StatickiUgaoA - StatickiUgaoB;

            double DuzinaSajleBVal =Math.Sqrt(StatickaStranicaA * StatickaStranicaA + StatickaStranicaB * StatickaStranicaB - 2 * StatickaStranicaA * StatickaStranicaB * Math.Cos(UgaoPrim));

            Console.WriteLine("Duzina Sajle B: " + DuzinaSajleBVal.ToString() + " mm");


            return DuzinaSajleBVal;
        }


        public static void PocetneKordinate(double DuzinaSajleA, double DuzinaSajleB)
        {

            //////////////////Ugao Alfa//////////////////////////////////////////////

           // double  GredaA = 179;
            double GredaAVisak = 40;
            double GredaANormalno = 13.5;
            double Nosac = 142;

            GredaAVisak = VisakA2;
            GredaANormalno = NormalnoA2;
            Nosac = NosacA;

            // double DuzinaSajleA = DuzinaSajleAA + FizickaDuzinaSajleAOduzimanje;
            // double DuzinaSajleB = DuzinaSajleBB + FizickaDuzinaSajleBOduzimanje;

            double StatickaStranica = Math.Sqrt(((GredaA - GredaAVisak) * (GredaA - GredaAVisak) + GredaANormalno * GredaANormalno));
            double StatickiUgaoRad = Math.Acos((GredaA - GredaAVisak) / StatickaStranica);

            double Alfa = Math.Acos((DuzinaSajleA * DuzinaSajleA - Nosac * Nosac - StatickaStranica * StatickaStranica) / (-2 * StatickaStranica * Nosac));


            AlfaPocetno = Alfa + StatickiUgaoRad;
            Console.WriteLine("AlfaPocetno: "+(AlfaPocetno*180/3.14).ToString());
            AlfaPocetno = Math.PI - AlfaPocetno;

            //////////////////////////////////////////////////////////////////////




           //        GredaA = 179;
            //double GredaB = 205;
                   GredaAVisak = 47;
            double GredaBVisak = 73;
                   GredaANormalno = 28;
            double GredaBNormalno = 13.5;

            GredaAVisak = VisakA1;
            GredaBVisak = VisakB;
            GredaANormalno = NormalnoA1;
            GredaBNormalno = NormalnoB;


            /////////////// Ugao Teta ////////////////////////////

            double StatickaStranicaA = Math.Sqrt((GredaA - GredaAVisak) * (GredaA - GredaAVisak) + GredaANormalno * GredaANormalno);
            double StatickaStranicaB = Math.Sqrt((GredaB - GredaBVisak) * (GredaB - GredaBVisak) + GredaBNormalno * GredaBNormalno);

            Console.WriteLine("StatickaStranica A : " + StatickaStranicaA.ToString());
            Console.WriteLine("StatickaStranica B : " + StatickaStranicaB.ToString());

            double StatickiUgaoA = Math.Acos((GredaA - GredaAVisak) / StatickaStranicaA);
            double StatickiUgaoB = Math.Acos((GredaB - GredaBVisak) / StatickaStranicaB);

            double Teta=Math.Acos((DuzinaSajleB*DuzinaSajleB-StatickaStranicaA*StatickaStranicaA-StatickaStranicaB*StatickaStranicaB)/(-2*StatickaStranicaB*StatickaStranicaA));

            TetaPocetno = Teta + StatickiUgaoA + StatickiUgaoB;
            Console.WriteLine("TetaPocetno: " + (TetaPocetno * 180 / 3.14).ToString());

            TetaPocetno = -Math.PI + AlfaPocetno + TetaPocetno;

            ///////////////////////////////////////////////////////////////

             PocetnaKoordinataX = (GredaA * Math.Cos(AlfaPocetno) + GredaB * Math.Cos(TetaPocetno))*1000;
             PocetnaKoordinataY = (GredaA * Math.Sin(AlfaPocetno) + GredaB * Math.Sin(TetaPocetno))*1000;

            PocetnaKoordinataX -= (SiftPositionX*1000);
            PocetnaKoordinataY -= (SiftPositionY*1000);

            Console.WriteLine("Pocetna X: "+PocetnaKoordinataX.ToString());
            Console.WriteLine("Pocetna Y: " + PocetnaKoordinataY.ToString());



        }


        public static int BrojKorakaMotorA (double NovaDuzinaSajle) // Nova duzina u mm
        {
            double Pomeraj = NovaDuzinaSajle - TrenutnaDuzinaSajleA;
            TrenutnaDuzinaSajleA = NovaDuzinaSajle;

            int BrojKoraka = 0;

            if(Pomeraj < 0)
            {
                SmerMotoraA = 'U';
                BrojKoraka = (int)((Pomeraj*(-1)) / (10.0 * Math.PI / 4096.0));
            }
            if (Pomeraj > 0)
            {
                SmerMotoraA = 'X';
                BrojKoraka = (int)(Pomeraj / (10.0 * Math.PI / 4096.0));
            }
            return BrojKoraka;
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kordinate tmp = new Kordinate(-177305,199166);
            OdrediBrojeveKoraka(tmp);
            Form1.sserialPort.Write("MA" + MotorABrojKoraka.ToString().PadLeft(5, '0') + toolStripTextBoxSpeed + SmerMotoraA + MotorBBrojKoraka.ToString().PadLeft(5, '0') + toolStripTextBoxSpeed + SmerMotoraB + "//");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.sserialPort.Write("STP//");
            MessageBox.Show("All process is stopped ! ", "Massage");
            KomandeLista.Clear();
            KomandeBool = false;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            IzbuseneRupe.Clear();
            ProveriIscrtavanja();
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                brzinamotora = toolStripTextBox2.Text;
                int Value = Convert.ToInt32(toolStripTextBox2.Text.Split('%')[0]) + 100;
                if (Value > 99 && Value < 201 && speedSend)
                    Form1.sserialPort.Write("GLM" + Value.ToString().PadLeft(3, '0') + "//");
            }
            catch { }
        }

        private void speedMovementToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (speedMovementToolStripMenuItem.Checked)
            {
                speedSend = true;
                try
                {
                    int Value = Convert.ToInt32(toolStripTextBox2.Text.Split('%')[0]) + 100;
                    if (Value > 99 && Value < 201)
                        Form1.sserialPort.Write("GLM" + Value.ToString().PadLeft(3, '0') + "//");
                }
                catch { }
            }
            else
            {
                 speedSend = false;
                 Form1.sserialPort.Write("GLM000//");
            }

        }

        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int MaxX = 0;
            try
            {
                foreach (rupa Value in Rupe)
                    foreach (Kordinate Val in Value.Kordinata)
                    {
                        if (Val.GetX() > MaxX)
                            MaxX = Val.GetX();
                    }

                foreach (rupa Value in Rupe)
                    foreach (Kordinate Val in Value.Kordinata)
                    {
                        Val.SetX(2 * MaxX - Val.GetX());
                    }
                AutoClosingMessageBox.Show("Succses","Message",500);
            }
            catch { }
        }

        private void horisontalMirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int MaxY = 0;
            try
            {
                foreach (rupa Value in Rupe)
                    foreach (Kordinate Val in Value.Kordinata)
                    {
                        if (Val.GetY() > MaxY)
                            MaxY = Val.GetY();
                    }

                foreach (rupa Value in Rupe)
                    foreach (Kordinate Val in Value.Kordinata)
                    {
                        Val.SetY(2 * MaxY - Val.GetY());
                    }
                AutoClosingMessageBox.Show("Succses", "Message",500);
            }
            catch { }
        }

        private void toolStripMenuItem5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int value = 0;
                string[] spliter = toolStripTextBox3.Text.Split('%');
                value = Convert.ToInt32(spliter[0]) + 100;

                if (toolStripMenuItem5.Checked)
                    Form1.sserialPort.Write("FAN" + value.ToString().PadLeft(3, '0') + "//");
                else
                    Form1.sserialPort.Write("FAN100//");
            }
            catch { }
        }

        private void toolStripTextBox3_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            try
            {
                string[] spliter = toolStripTextBox3.Text.Split('%');
                value = Convert.ToInt32(spliter[0]) + 100;

                if (value > 100 && value < 201 && toolStripMenuItem5.Checked)
                { Form1.sserialPort.Write("FAN" + value.ToString().PadLeft(3, '0') + "//");
                    Console.WriteLine("FAN" + value.ToString().PadLeft(3, '0') + "//");
                }
                    
            }
            catch
            {

            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            FormPosition Forma = new FormPosition();
            Forma.Show();
        }

        public static int BrojKorakaMotorB(double NovaDuzinaSajle) // Nova duzina u mm
        {
            double Pomeraj = NovaDuzinaSajle - TrenutnaDuzinaSajleB;
            TrenutnaDuzinaSajleB = NovaDuzinaSajle;

            int BrojKoraka = 0;

            if (Pomeraj < 0)
            {
                SmerMotoraB = 'U';
                BrojKoraka = (int)(Pomeraj * (-1) / (10.0 * Math.PI / 4096.0));
            }
            if (Pomeraj > 0)
            {
                SmerMotoraB = 'X';
                BrojKoraka = (int)(Pomeraj / (10.0 * Math.PI / 4096.0));
            }
            return BrojKoraka;
        }

    }
    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer)
                MessageBox.Show(text, caption);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }


    public class Kordinate
    {
        private
            int X;
        int Y;
        public
            Kordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int GetX()
        { return X; }
        public int GetY()
        { return Y; }
        public void SetX(int x)
        { X = x; }
        public void SetY(int y)
        { Y = y; }
    }

    public class rupa
    {
        private
            double precnik;
        int ime;
        public List<Kordinate> Kordinata = new List<Kordinate>();
        public
        rupa(double pr, int im)
        {
            this.precnik = pr;
            this.ime = im;

        }
        public int GetRupaIme()
        {
            return this.ime;
        }
        public double GetRupaPrecnik()
        {
            return this.precnik;
        }

        public void AddKordinata(int x, int y)
        {
            Kordinate K = new Kordinate(x, y);
            Kordinata.Add(K);
        }
        public void DelateKordinata(int x, int y)
        {
            Kordinate K = new Kordinate(x, y);
            Kordinata.Remove(K);
        }
    }

    public class KomandeClass
    {
        private
            string KSKoma;
            double KSDuzinaA;
            double KSDuzinaB;

        public KomandeClass(string Koma,double DuzA,double DuzB)
        {
            KSKoma = Koma;
            KSDuzinaA = DuzA;
            KSDuzinaB = DuzB;
        }

        public string GetKomanda()
        {
            return KSKoma;
        }
        public double GetDuzinaA()
        {
            return KSDuzinaA;
        }
        public double GetDuzinaB()
        {
            return KSDuzinaB;
        }
    }

}
