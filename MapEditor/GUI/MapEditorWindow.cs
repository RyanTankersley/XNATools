using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNATools.MapEditor.GUI
{
    public partial class MapEditorWindow : Form
    {
        public MapEditorWindow()
        {
            InitializeComponent();
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open sprite sheet to load";
            string fileName = string.Empty;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = ofd.FileName;
            }

            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                this.ucMapEditor.LoadViewers(fileName, new Vector2(32, 32));
            }
            else
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Map Editor FIle (.mep)|*.mep";
            sfd.DefaultExt = ".mep";
            if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
    }
}
