namespace XNATools.MapEditor.GUI
{
    partial class MapEditorWindow
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
            this.ucMapEditor = new XNATools.MapEditor.Controls.MapEditor();
            this.SuspendLayout();
            // 
            // ucMapEditor
            // 
            this.ucMapEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMapEditor.Location = new System.Drawing.Point(0, 0);
            this.ucMapEditor.Name = "ucMapEditor";
            this.ucMapEditor.Size = new System.Drawing.Size(855, 645);
            this.ucMapEditor.TabIndex = 3;
            // 
            // MapEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 645);
            this.Controls.Add(this.ucMapEditor);
            this.Name = "MapEditorWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapEditorWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MapEditor ucMapEditor;
    }
}