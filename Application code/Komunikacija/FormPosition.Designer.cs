namespace Komunikacija
{
    partial class FormPosition
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KoordX = new System.Windows.Forms.Label();
            this.KoordY = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "Motor A FORWARD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 57);
            this.button2.TabIndex = 1;
            this.button2.Text = "Motor B BACKWARD";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            this.button2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button2_MouseUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(188, 94);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 57);
            this.button3.TabIndex = 2;
            this.button3.Text = "Motor B FORWARD";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button3_MouseDown);
            this.button3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button3_MouseUp);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(100, 150);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 56);
            this.button4.TabIndex = 3;
            this.button4.Text = "Motor A BACKWARD";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button4_MouseDown);
            this.button4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button4_MouseUp);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(299, 33);
            this.vScrollBar1.Maximum = 65;
            this.vScrollBar1.Minimum = 35;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(30, 173);
            this.vScrollBar1.TabIndex = 4;
            this.vScrollBar1.Value = 35;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(55, 43);
            this.button5.TabIndex = 5;
            this.button5.Text = "Go To Start";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(50, 215);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(30, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Speed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Hz";
            // 
            // KoordX
            // 
            this.KoordX.AutoSize = true;
            this.KoordX.Location = new System.Drawing.Point(61, 268);
            this.KoordX.Name = "KoordX";
            this.KoordX.Size = new System.Drawing.Size(88, 13);
            this.KoordX.TabIndex = 9;
            this.KoordX.Text = "Click Go To Start";
            // 
            // KoordY
            // 
            this.KoordY.AutoSize = true;
            this.KoordY.Location = new System.Drawing.Point(199, 268);
            this.KoordY.Name = "KoordY";
            this.KoordY.Size = new System.Drawing.Size(88, 13);
            this.KoordY.TabIndex = 10;
            this.KoordY.Text = "Click Go To Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Y:";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(188, 232);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(167, 22);
            this.button6.TabIndex = 13;
            this.button6.Text = "This is Start Position";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(215, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(55, 43);
            this.button7.TabIndex = 14;
            this.button7.Text = "Go To 0,0";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // FormPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 290);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.KoordY);
            this.Controls.Add(this.KoordX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FormPosition";
            this.Text = "FormPosition";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPosition_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label KoordX;
        private System.Windows.Forms.Label KoordY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}