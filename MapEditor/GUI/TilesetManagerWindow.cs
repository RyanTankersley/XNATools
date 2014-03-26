using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XNATools.MapEditor.GUI
{
    public partial class TilesetManagerWindow : Form
    {
        public TilesetManagerWindow()
        {
            InitializeComponent();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.ucTilesetManagerViewer.Save();
            this.Close();
        }
    }
}
