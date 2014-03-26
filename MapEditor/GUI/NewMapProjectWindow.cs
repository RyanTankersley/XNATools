using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XNATools.MapEditor.Collections;
using XNATools.MapEditor.Objects;

namespace XNATools.MapEditor.GUI
{
    public partial class NewMapProjectWindow : Form
    {
        public MapProject CreatedMapProject 
        { 
            get 
            { 
                MapProject mp = new MapProject(); 
                mp.Name = this.tbProjectName.Text; 
                mp.Tileset = this.cbTileset.SelectedItem as Tileset; 
                return mp; 
            } 
        }

        public NewMapProjectWindow()
        {
            InitializeComponent();

            foreach (Tileset o in Tilesets.GetTilesets())
            {
                this.cbTileset.Items.Add(o);
            }

            if (this.cbTileset.Items.Count > 0)
            {
                this.cbTileset.SelectedItem = this.cbTileset.Items[0];
            }
        }

        private void tbProjectName_TextChanged(object sender, EventArgs e)
        {
            this.bOK.Enabled = !string.IsNullOrEmpty(this.tbProjectName.Text) && this.cbTileset.SelectedItem != null;
        }

        private void cbTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bOK.Enabled = !string.IsNullOrEmpty(this.tbProjectName.Text) && this.cbTileset.SelectedItem != null;
        }
    }
}
