using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XNATools.MapEditor.Objects;

namespace XNATools.MapEditor.GUI
{
    public partial class NewDirectoryChildWindow : NewMapItemWindow
    {
        public NewDirectoryChildWindow()
        {
            InitializeComponent();
            this.cbType.SelectedItem = this.cbType.Items[0];
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            this.bOK.Enabled = !string.IsNullOrEmpty(this.tbName.Text);
        }

        private override IMapDirectoryObject GetMapDirectoryObject()
        {
            IMapDirectoryObject toReturn = null;

            if (this.cbType.SelectedIndex == 0 && !string.IsNullOrEmpty(this.tbName.Text))
            {
                toReturn = GetObjectDirectory();
            }
            else if (this.cbType.SelectedIndex == 1 && !string.IsNullOrEmpty(this.tbName.Text))
            {
                toReturn = GetMapObject();
            }

            return toReturn;
        }

        private ObjectDirectory GetObjectDirectory()
        {
            ObjectDirectory toReturn = null;

            if (this.cbType.SelectedIndex == 0 && !string.IsNullOrEmpty(this.tbName.Text))
            {
                toReturn = new ObjectDirectory();
                toReturn.MapObjects = new Collections.MapObjects();
                toReturn.ObjectDirectories = new Collections.ObjectDirectories();
                toReturn.Name = this.tbName.Text;
            }

            return toReturn;
        }

        private MapObject GetMapObject()
        {
            MapObject toReturn = null;

            if (this.cbType.SelectedIndex == 1 && !string.IsNullOrEmpty(this.tbName.Text))
            {
                toReturn = new MapObject();
                toReturn.TileMap = null;
                toReturn.Name = this.tbName.Text;
            }

            return toReturn;
        }
    }
}
