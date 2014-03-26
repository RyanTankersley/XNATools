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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.nmLayer = new System.Windows.Forms.NumericUpDown();
            this.ucSpriteSheet = new XNATools.MapEditor.Controls.MapViewer();
            this.ucMapEdit = new XNATools.MapEditor.Controls.MapViewer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmLayer)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(572, 306);
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.nmLayer);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(572, 347);
            this.splitContainer2.SplitterDistance = 37;
            this.splitContainer2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layer:";
            // 
            // nmLayer
            // 
            this.nmLayer.Location = new System.Drawing.Point(55, 10);
            this.nmLayer.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmLayer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmLayer.Name = "nmLayer";
            this.nmLayer.Size = new System.Drawing.Size(36, 20);
            this.nmLayer.TabIndex = 1;
            this.nmLayer.TabStop = false;
            this.nmLayer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmLayer.ValueChanged += new System.EventHandler(this.nmLayer_ValueChanged);
            // 
            // ucSpriteSheet
            // 
            this.ucSpriteSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSpriteSheet.Location = new System.Drawing.Point(0, 0);
            this.ucSpriteSheet.Map = null;
            this.ucSpriteSheet.Name = "ucSpriteSheet";
            this.ucSpriteSheet.SelectedMapCells = null;
            this.ucSpriteSheet.Size = new System.Drawing.Size(277, 306);
            this.ucSpriteSheet.TabIndex = 0;
            this.ucSpriteSheet.Text = "mapViewer1";
            // 
            // ucMapEdit
            // 
            this.ucMapEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMapEdit.Location = new System.Drawing.Point(0, 0);
            this.ucMapEdit.Map = null;
            this.ucMapEdit.Name = "ucMapEdit";
            this.ucMapEdit.SelectedMapCells = null;
            this.ucMapEdit.Size = new System.Drawing.Size(291, 306);
            this.ucMapEdit.TabIndex = 0;
            this.ucMapEdit.Text = "mapViewer2";
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "MapEditor";
            this.Size = new System.Drawing.Size(572, 347);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmLayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MapViewer ucSpriteSheet;
        private MapViewer ucMapEdit;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.NumericUpDown nmLayer;
        private System.Windows.Forms.Label label1;
    }
}
