using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XNATools.MapEditor.Objects;
using XNATools.MapEditor.Collections;
using XNATools.MapEditor.GUI;

namespace XNATools.MapEditor.GUI
{
    public partial class MapCreator : Form
    {
        public MapCreator()
        {
            InitializeComponent();
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            NewMapProjectWindow win = new NewMapProjectWindow();
            MapProject mp = null;

            if (win.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mp = win.CreatedMapProject;
            }
        }

        private void tsmiManageTilesets_Click(object sender, EventArgs e)
        {

        }
    }
}
