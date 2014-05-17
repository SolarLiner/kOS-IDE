namespace kOS_IDE
{
    partial class Options
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.SmartIndent = new System.Windows.Forms.CheckBox();
            this.SetFont = new System.Windows.Forms.Button();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.ZoomBar = new System.Windows.Forms.TrackBar();
            this.Zoom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SmartIndent
            // 
            this.SmartIndent.AutoSize = true;
            this.SmartIndent.Checked = true;
            this.SmartIndent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SmartIndent.Location = new System.Drawing.Point(4, 4);
            this.SmartIndent.Name = "SmartIndent";
            this.SmartIndent.Size = new System.Drawing.Size(309, 17);
            this.SmartIndent.TabIndex = 0;
            this.SmartIndent.Text = "Smart Indent: Automatically tabs after a { and resets after a }";
            this.SmartIndent.UseVisualStyleBackColor = true;
            this.SmartIndent.CheckedChanged += new System.EventHandler(this.SmartIndent_CheckedChanged);
            // 
            // SetFont
            // 
            this.SetFont.Location = new System.Drawing.Point(4, 53);
            this.SetFont.Name = "SetFont";
            this.SetFont.Size = new System.Drawing.Size(106, 23);
            this.SetFont.TabIndex = 1;
            this.SetFont.Text = "Set Font";
            this.SetFont.UseVisualStyleBackColor = true;
            this.SetFont.Click += new System.EventHandler(this.SetFont_Click);
            // 
            // fontDialog
            // 
            this.fontDialog.AllowSimulations = false;
            this.fontDialog.AllowVerticalFonts = false;
            this.fontDialog.ScriptsOnly = true;
            this.fontDialog.ShowColor = true;
            this.fontDialog.ShowEffects = false;
            // 
            // ZoomBar
            // 
            this.ZoomBar.Location = new System.Drawing.Point(4, 143);
            this.ZoomBar.Maximum = 50;
            this.ZoomBar.Minimum = -5;
            this.ZoomBar.Name = "ZoomBar";
            this.ZoomBar.Size = new System.Drawing.Size(374, 45);
            this.ZoomBar.TabIndex = 2;
            this.ZoomBar.TickFrequency = 5;
            this.ZoomBar.Scroll += new System.EventHandler(this.ZoomBar_Scroll);
            // 
            // Zoom
            // 
            this.Zoom.AutoSize = true;
            this.Zoom.Location = new System.Drawing.Point(4, 124);
            this.Zoom.Name = "Zoom";
            this.Zoom.Size = new System.Drawing.Size(66, 13);
            this.Zoom.TabIndex = 3;
            this.Zoom.Text = "Zoom: 100%";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Zoom);
            this.Controls.Add(this.ZoomBar);
            this.Controls.Add(this.SetFont);
            this.Controls.Add(this.SmartIndent);
            this.Name = "Options";
            this.Size = new System.Drawing.Size(381, 188);
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox SmartIndent;
        private System.Windows.Forms.Button SetFont;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.TrackBar ZoomBar;
        private System.Windows.Forms.Label Zoom;
    }
}
