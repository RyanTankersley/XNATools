using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XNATools.MapEditor.Objects;

namespace XNATools.MapEditor.GUI
{
    public partial class NewTilesetWindow : Form, INewMapObjectWindow
    {
        public IMapDirectoryObject MapDirectoryObject { get { return GetTileSet(); } }

        public NewTilesetWindow()
        {
            InitializeComponent();
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this.tbTilesheet.Text = ofd.FileName;
                }
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.tbTilesheet.Text))
            {
                byte[] read = File.ReadAllBytes(this.tbTilesheet.Text);
                File.WriteAllBytes(Path.Combine(DAL.MapDAL.GetTilesetPath(), this.tbName.Text + System.IO.Path.GetExtension(this.tbTilesheet.Text)), read);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: " + this.tbTilesheet.Text + " does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Textbox_TextChange(object sender, EventArgs e)
        {
            this.bOK.Enabled = !string.IsNullOrEmpty(this.tbName.Text) && !string.IsNullOrEmpty(this.tbTilesheet.Text);
        }

        private Tileset GetTileSet()
        {
            Tileset toReturn = null;

            if (!string.IsNullOrEmpty(this.tbTilesheet.Text) && !string.IsNullOrEmpty(this.tbName.Text))
            {
                toReturn = new Tileset();
                toReturn.Name = this.tbName.Text;
                toReturn.TileWidth = (int)this.nmTileWidth.Value;
                toReturn.TileHeight = (int)this.nmTileHeight.Value;
                toReturn.MapObjects = new Collections.MapObjects();
                toReturn.ObjectDirectories = new Collections.ObjectDirectories();
            }

            return toReturn;
        }
    }
}
