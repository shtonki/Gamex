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
            this.SettingInfoPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // SettingsTreeView
            // 
            this.SettingsTreeView.Location = new System.Drawing.Point(3, 3);
            this.SettingsTreeView.Name = "SettingsTreeView";
            this.SettingsTreeView.Size = new System.Drawing.Size(493, 377);
            this.SettingsTreeView.TabIndex = 0;
            this.SettingsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.HandleSelectChanged);
            // 
            // SettingInfoPanel
            // 
            this.SettingInfoPanel.Location = new System.Drawing.Point(4, 387);
            this.SettingInfoPanel.Name = "SettingInfoPanel";
            this.SettingInfoPanel.Size = new System.Drawing.Size(492, 111);
            this.SettingInfoPanel.TabIndex = 1;
            // 
            // DebugSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingInfoPanel);
            this.Controls.Add(this.SettingsTreeView);
            this.Name = "DebugSettingsWindow";
            this.Size = new System.Drawing.Size(499, 501);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView SettingsTreeView;
        private System.Windows.Forms.Panel SettingInfoPanel;
    }
}
