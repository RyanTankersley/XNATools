namespace XNATools.MapEditor.Controls
{
    partial class MapEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucSpriteSheet = new XNATools.MapEditor.Controls.MapViewer();
            this.ucMapEdit = new XNATools.MapEditor.Controls.MapViewer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucSpriteSheet);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucMapEdit);
            this.splitContainer1.Size = new System.Drawing.Size(572, 347);
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.TabIndex = 0;
            // 
            // ucSpriteSheet
            // 
            this.ucSpriteSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSpriteSheet.Location = new System.Drawing.Point(0, 0);
            this.ucSpriteSheet.Map = null;
            this.ucSpriteSheet.Name = "ucSpriteSheet";
            this.ucSpriteSheet.Size = new System.Drawing.Size(277, 347);
            this.ucSpriteSheet.TabIndex = 0;
            this.ucSpriteSheet.Text = "mapViewer1";
            // 
            // ucMapEdit
            // 
            this.ucMapEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMapEdit.Location = new System.Drawing.Point(0, 0);
            this.ucMapEdit.Map = null;
            this.ucMapEdit.Name = "ucMapEdit";
            this.ucMapEdit.Size = new System.Drawing.Size(291, 347);
            this.ucMapEdit.TabIndex = 0;
            this.ucMapEdit.Text = "mapViewer2";
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MapEditor";
            this.Size = new System.Drawing.Size(572, 347);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MapViewer ucSpriteSheet;
        private MapViewer ucMapEdit;
    }
}
