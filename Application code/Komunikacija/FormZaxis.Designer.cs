namespace Komunikacija
{
    partial class FormZaxis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormZaxis));
            this.vScrollBarZaxis = new System.Windows.Forms.VScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SPUPbutton = new System.Windows.Forms.Button();
            this.SPDOWNbutton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // vScrollBarZaxis
            // 
            this.vScrollBarZaxis.LargeChange = 1;
            this.vScrollBarZaxis.Location = new System.Drawing.Point(142, 61);
            this.vScrollBarZaxis.Maximum = 65;
            this.vScrollBarZaxis.Minimum = 35;
            this.vScrollBarZaxis.Name = "vScrollBarZaxis";
            this.vScrollBarZaxis.Size = new System.Drawing.Size(55, 229);
            this.vScrollBarZaxis.TabIndex = 0;
            this.vScrollBarZaxis.Value = 35;
            this.vScrollBarZaxis.ValueChanged += new System.EventHandler(this.vScrollBarZaxis_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(150, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Change  position   Z axis.";
            // 
            // SPUPbutton
            // 
            this.SPUPbutton.Location = new System.Drawing.Point(16, 61);
            this.SPUPbutton.Name = "SPUPbutton";
            this.SPUPbutton.Size = new System.Drawing.Size(83, 39);
            this.SPUPbutton.TabIndex = 2;
            this.SPUPbutton.Text = "Save Position UP";
            this.SPUPbutton.UseVisualStyleBackColor = true;
            this.SPUPbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SPDOWNbutton
            // 
            this.SPDOWNbutton.Location = new System.Drawing.Point(16, 155);
            this.SPDOWNbutton.Name = "SPDOWNbutton";
            this.SPDOWNbutton.Size = new System.Drawing.Size(83, 39);
            this.SPDOWNbutton.TabIndex = 3;
            this.SPDOWNbutton.Text = "Save Position DOWN";
            this.SPDOWNbutton.UseVisualStyleBackColor = true;
            this.SPDOWNbutton.Click += new System.EventHandler(this.SPDOWNbutton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 273);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(123, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "ON/OFF Drill Engine";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Enabled = false;
            this.hScrollBar1.Location = new System.Drawing.Point(2, 296);
            this.hScrollBar1.Maximum = 200;
            this.hScrollBar1.Minimum = 100;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(130, 17);
            this.hScrollBar1.TabIndex = 6;
            this.hScrollBar1.Value = 100;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "0% ------Speed------ 100%";
            // 
            // FormZaxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 335);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.SPDOWNbutton);
            this.Controls.Add(this.SPUPbutton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vScrollBarZaxis);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormZaxis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Position Z axis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBarZaxis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SPUPbutton;
        private System.Windows.Forms.Button SPDOWNbutton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Label label2;
    }
}