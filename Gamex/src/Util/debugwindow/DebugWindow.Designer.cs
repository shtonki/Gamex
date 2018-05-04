namespace Gamex.src.Util.DebugWindow
{
    partial class DebugWindow
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GamexConsoleWindow = new Gamex.src.Util.DebugWindow.ConsoleWindow();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MouseInfoWindow = new Gamex.src.Util.DebugWindow.MouseDebugWindow();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.LogWindow = new Gamex.src.Util.DebugWindow.LogWindow();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(509, 527);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GamexConsoleWindow);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(501, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Console";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GamexConsoleWindow
            // 
            this.GamexConsoleWindow.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GamexConsoleWindow.Location = new System.Drawing.Point(1, 1);
            this.GamexConsoleWindow.Name = "GamexConsoleWindow";
            this.GamexConsoleWindow.Size = new System.Drawing.Size(500, 500);
            this.GamexConsoleWindow.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.MouseInfoWindow);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mouse Info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MouseInfoWindow
            // 
            this.MouseInfoWindow.Location = new System.Drawing.Point(7, 7);
            this.MouseInfoWindow.Name = "MouseInfoWindow";
            this.MouseInfoWindow.Size = new System.Drawing.Size(488, 488);
            this.MouseInfoWindow.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.LogWindow);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(501, 501);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // LogWindow
            // 
            this.LogWindow.Location = new System.Drawing.Point(3, 3);
            this.LogWindow.Name = "LogWindow";
            this.LogWindow.Size = new System.Drawing.Size(495, 495);
            this.LogWindow.TabIndex = 0;
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(536, 553);
            this.Controls.Add(this.tabControl1);
            this.Name = "DebugWindow";
            this.Text = "DebugWindow";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public ConsoleWindow GamexConsoleWindow;
        public MouseDebugWindow MouseInfoWindow;
        private System.Windows.Forms.TabPage tabPage3;
        public LogWindow LogWindow;
    }
}