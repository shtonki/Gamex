namespace Gamex.src.Util.DebugWindow
{
    partial class MouseDebugWindow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MouseCoordinatePixelLabel = new System.Windows.Forms.Label();
            this.MouseCoordinateGLLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MousePosPixelTooltip = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MousePosGLTooltip = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MousePosGameTooltip = new System.Windows.Forms.Label();
            this.MouseCoordinateGameLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MouseCoordinatePixelLabel
            // 
            this.MouseCoordinatePixelLabel.AutoSize = true;
            this.MouseCoordinatePixelLabel.Location = new System.Drawing.Point(29, 40);
            this.MouseCoordinatePixelLabel.Name = "MouseCoordinatePixelLabel";
            this.MouseCoordinatePixelLabel.Size = new System.Drawing.Size(84, 13);
            this.MouseCoordinatePixelLabel.TabIndex = 0;
            this.MouseCoordinatePixelLabel.Text = "MousePosPixels";
            this.MouseCoordinatePixelLabel.Click += new System.EventHandler(this.MouseCoordinatePixelLabel_Click);
            // 
            // MouseCoordinateGLLabel
            // 
            this.MouseCoordinateGLLabel.AutoSize = true;
            this.MouseCoordinateGLLabel.Location = new System.Drawing.Point(29, 42);
            this.MouseCoordinateGLLabel.Name = "MouseCoordinateGLLabel";
            this.MouseCoordinateGLLabel.Size = new System.Drawing.Size(71, 13);
            this.MouseCoordinateGLLabel.TabIndex = 1;
            this.MouseCoordinateGLLabel.Text = "MousePosGL";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.MousePosPixelTooltip);
            this.panel1.Controls.Add(this.MouseCoordinatePixelLabel);
            this.panel1.Location = new System.Drawing.Point(14, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 66);
            this.panel1.TabIndex = 2;
            // 
            // MousePosPixelTooltip
            // 
            this.MousePosPixelTooltip.AutoSize = true;
            this.MousePosPixelTooltip.Location = new System.Drawing.Point(13, 13);
            this.MousePosPixelTooltip.Name = "MousePosPixelTooltip";
            this.MousePosPixelTooltip.Size = new System.Drawing.Size(112, 13);
            this.MousePosPixelTooltip.TabIndex = 1;
            this.MousePosPixelTooltip.Text = "Mouse Position, Pixels";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.MousePosGLTooltip);
            this.panel2.Controls.Add(this.MouseCoordinateGLLabel);
            this.panel2.Location = new System.Drawing.Point(156, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(136, 66);
            this.panel2.TabIndex = 3;
            // 
            // MousePosGLTooltip
            // 
            this.MousePosGLTooltip.AutoSize = true;
            this.MousePosGLTooltip.Location = new System.Drawing.Point(12, 13);
            this.MousePosGLTooltip.Name = "MousePosGLTooltip";
            this.MousePosGLTooltip.Size = new System.Drawing.Size(99, 13);
            this.MousePosGLTooltip.TabIndex = 2;
            this.MousePosGLTooltip.Text = "Mouse Position, GL";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.MousePosGameTooltip);
            this.panel3.Controls.Add(this.MouseCoordinateGameLabel);
            this.panel3.Location = new System.Drawing.Point(298, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(136, 66);
            this.panel3.TabIndex = 4;
            // 
            // MousePosGameTooltip
            // 
            this.MousePosGameTooltip.AutoSize = true;
            this.MousePosGameTooltip.Location = new System.Drawing.Point(12, 13);
            this.MousePosGameTooltip.Name = "MousePosGameTooltip";
            this.MousePosGameTooltip.Size = new System.Drawing.Size(113, 13);
            this.MousePosGameTooltip.TabIndex = 2;
            this.MousePosGameTooltip.Text = "Mouse Position, Game";
            // 
            // MouseCoordinateGameLabel
            // 
            this.MouseCoordinateGameLabel.AutoSize = true;
            this.MouseCoordinateGameLabel.Location = new System.Drawing.Point(29, 42);
            this.MouseCoordinateGameLabel.Name = "MouseCoordinateGameLabel";
            this.MouseCoordinateGameLabel.Size = new System.Drawing.Size(85, 13);
            this.MouseCoordinateGameLabel.TabIndex = 1;
            this.MouseCoordinateGameLabel.Text = "MousePosGame";
            // 
            // MouseDebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MouseDebugWindow";
            this.Size = new System.Drawing.Size(500, 500);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label MouseCoordinateGLLabel;
        public System.Windows.Forms.Label MouseCoordinatePixelLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label MousePosPixelTooltip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label MousePosGLTooltip;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label MousePosGameTooltip;
        public System.Windows.Forms.Label MouseCoordinateGameLabel;
    }
}
