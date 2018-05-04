namespace Gamex.src.Util.DebugWindow
{
    partial class LogWindow
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
            this.LogDataGrid = new System.Windows.Forms.DataGridView();
            this.MessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutoScrollCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // LogDataGrid
            // 
            this.LogDataGrid.AllowUserToAddRows = false;
            this.LogDataGrid.AllowUserToDeleteRows = false;
            this.LogDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MessageColumn});
            this.LogDataGrid.Location = new System.Drawing.Point(3, 29);
            this.LogDataGrid.Name = "LogDataGrid";
            this.LogDataGrid.ReadOnly = true;
            this.LogDataGrid.Size = new System.Drawing.Size(494, 468);
            this.LogDataGrid.TabIndex = 0;
            this.LogDataGrid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LogDataGrid_Scroll);
            // 
            // MessageColumn
            // 
            this.MessageColumn.HeaderText = "Message";
            this.MessageColumn.Name = "MessageColumn";
            this.MessageColumn.ReadOnly = true;
            this.MessageColumn.Width = 400;
            // 
            // AutoScrollCheckBox
            // 
            this.AutoScrollCheckBox.AutoSize = true;
            this.AutoScrollCheckBox.Location = new System.Drawing.Point(3, 6);
            this.AutoScrollCheckBox.Name = "AutoScrollCheckBox";
            this.AutoScrollCheckBox.Size = new System.Drawing.Size(77, 17);
            this.AutoScrollCheckBox.TabIndex = 1;
            this.AutoScrollCheckBox.Text = "Auto Scroll";
            this.AutoScrollCheckBox.UseVisualStyleBackColor = true;
            this.AutoScrollCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AutoScrollCheckBox);
            this.Controls.Add(this.LogDataGrid);
            this.Name = "LogWindow";
            this.Size = new System.Drawing.Size(500, 500);
            ((System.ComponentModel.ISupportInitialize)(this.LogDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.DataGridView LogDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageColumn;
        private System.Windows.Forms.CheckBox AutoScrollCheckBox;
    }
}
