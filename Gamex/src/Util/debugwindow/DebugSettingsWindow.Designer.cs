namespace Gamex.src.Util.DebugWindow
{
    partial class DebugSettingsWindow
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
            this.SettingsTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // SettingsTreeView
            // 
            this.SettingsTreeView.Location = new System.Drawing.Point(3, 3);
            this.SettingsTreeView.Name = "SettingsTreeView";
            this.SettingsTreeView.Size = new System.Drawing.Size(493, 495);
            this.SettingsTreeView.TabIndex = 0;
            // 
            // DebugSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingsTreeView);
            this.Name = "DebugSettingsWindow";
            this.Size = new System.Drawing.Size(499, 501);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView SettingsTreeView;
    }
}
