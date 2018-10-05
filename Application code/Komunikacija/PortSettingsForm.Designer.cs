namespace Komunikacija
{
    partial class PortSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortSettingsForm));
            this.labelCurrently = new System.Windows.Forms.Label();
            this.listActivePort = new System.Windows.Forms.ListBox();
            this.labelConnected = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelCurrently
            // 
            this.labelCurrently.AutoSize = true;
            this.labelCurrently.Location = new System.Drawing.Point(19, 32);
            this.labelCurrently.Name = "labelCurrently";
            this.labelCurrently.Size = new System.Drawing.Size(69, 13);
            this.labelCurrently.TabIndex = 0;
            this.labelCurrently.Text = "Active ports :";
            // 
            // listActivePort
            // 
            this.listActivePort.FormattingEnabled = true;
            this.listActivePort.Location = new System.Drawing.Point(22, 48);
            this.listActivePort.Name = "listActivePort";
            this.listActivePort.Size = new System.Drawing.Size(60, 56);
            this.listActivePort.TabIndex = 1;
            // 
            // labelConnected
            // 
            this.labelConnected.AutoSize = true;
            this.labelConnected.Location = new System.Drawing.Point(152, 32);
            this.labelConnected.Name = "labelConnected";
            this.labelConnected.Size = new System.Drawing.Size(0, 13);
            this.labelConnected.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Auto connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(215, 140);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Test Communication";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PortSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(346, 196);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelConnected);
            this.Controls.Add(this.listActivePort);
            this.Controls.Add(this.labelCurrently);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PortSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect with device";
            this.Load += new System.EventHandler(this.PortSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCurrently;
        private System.Windows.Forms.ListBox listActivePort;
        private System.Windows.Forms.Label labelConnected;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}