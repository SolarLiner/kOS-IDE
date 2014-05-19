namespace kOS_IDE
{
    partial class GlobalOptions
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Connect = new System.Windows.Forms.Button();
            this.Remember = new System.Windows.Forms.CheckBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FindKSP = new System.Windows.Forms.Button();
            this.KSPdir = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ZoomLabel = new System.Windows.Forms.Label();
            this.ZoomBar = new System.Windows.Forms.TrackBar();
            this.SmartIndent = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Connect);
            this.groupBox1.Controls.Add(this.Remember);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.Username);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect to the kOS Store";
            // 
            // Connect
            // 
            this.Connect.Enabled = false;
            this.Connect.Location = new System.Drawing.Point(368, 99);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 3;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Remember
            // 
            this.Remember.AutoSize = true;
            this.Remember.Enabled = false;
            this.Remember.Location = new System.Drawing.Point(6, 103);
            this.Remember.Name = "Remember";
            this.Remember.Size = new System.Drawing.Size(209, 17);
            this.Remember.TabIndex = 2;
            this.Remember.Text = "Remember me (will crypt the password)";
            this.Remember.UseVisualStyleBackColor = true;
            // 
            // Password
            // 
            this.Password.Enabled = false;
            this.Password.Location = new System.Drawing.Point(7, 60);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '•';
            this.Password.Size = new System.Drawing.Size(436, 20);
            this.Password.TabIndex = 1;
            this.Password.Text = "Placeholder";
            // 
            // Username
            // 
            this.Username.Enabled = false;
            this.Username.Location = new System.Drawing.Point(7, 20);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(436, 20);
            this.Username.TabIndex = 0;
            this.Username.Text = "Placeholder";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FindKSP);
            this.groupBox2.Controls.Add(this.KSPdir);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 82);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "KSP Directory";
            // 
            // FindKSP
            // 
            this.FindKSP.Location = new System.Drawing.Point(400, 44);
            this.FindKSP.Name = "FindKSP";
            this.FindKSP.Size = new System.Drawing.Size(43, 23);
            this.FindKSP.TabIndex = 3;
            this.FindKSP.Text = "...";
            this.FindKSP.UseVisualStyleBackColor = true;
            // 
            // KSPdir
            // 
            this.KSPdir.Location = new System.Drawing.Point(10, 47);
            this.KSPdir.Name = "KSPdir";
            this.KSPdir.Size = new System.Drawing.Size(384, 20);
            this.KSPdir.TabIndex = 2;
            this.KSPdir.TextChanged += new System.EventHandler(this.KSPdir_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(343, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 23);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Steam Installation";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Specify the root KSP Directory";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Status.Location = new System.Drawing.Point(0, 408);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(41, 13);
            this.Status.TabIndex = 2;
            this.Status.Text = "Ready.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ZoomLabel);
            this.groupBox3.Controls.Add(this.ZoomBar);
            this.groupBox3.Controls.Add(this.SmartIndent);
            this.groupBox3.Location = new System.Drawing.Point(13, 235);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 135);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Window Specific Options (will overwrite)";
            // 
            // ZoomLabel
            // 
            this.ZoomLabel.AutoSize = true;
            this.ZoomLabel.Location = new System.Drawing.Point(7, 68);
            this.ZoomLabel.Name = "ZoomLabel";
            this.ZoomLabel.Size = new System.Drawing.Size(66, 13);
            this.ZoomLabel.TabIndex = 4;
            this.ZoomLabel.Text = "Zoom: 100%";
            // 
            // ZoomBar
            // 
            this.ZoomBar.Location = new System.Drawing.Point(6, 84);
            this.ZoomBar.Maximum = 50;
            this.ZoomBar.Minimum = -5;
            this.ZoomBar.Name = "ZoomBar";
            this.ZoomBar.Size = new System.Drawing.Size(437, 45);
            this.ZoomBar.TabIndex = 3;
            this.ZoomBar.TickFrequency = 5;
            this.ZoomBar.Scroll += new System.EventHandler(this.ZoomBar_Scroll);
            // 
            // SmartIndent
            // 
            this.SmartIndent.AutoSize = true;
            this.SmartIndent.Location = new System.Drawing.Point(10, 19);
            this.SmartIndent.Name = "SmartIndent";
            this.SmartIndent.Size = new System.Drawing.Size(86, 17);
            this.SmartIndent.TabIndex = 0;
            this.SmartIndent.Text = "Smart Indent";
            this.SmartIndent.UseVisualStyleBackColor = true;
            this.SmartIndent.CheckedChanged += new System.EventHandler(this.SmartIndent_CheckedChanged);
            // 
            // GlobalOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 421);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlobalOptions";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Global Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GlobalOptions_FormClosing);
            this.Load += new System.EventHandler(this.GlobalOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.CheckBox Remember;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button FindKSP;
        private System.Windows.Forms.TextBox KSPdir;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox SmartIndent;
        private System.Windows.Forms.Label ZoomLabel;
        private System.Windows.Forms.TrackBar ZoomBar;
    }
}