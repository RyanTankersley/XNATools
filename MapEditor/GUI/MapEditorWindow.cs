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
    public partial class MapEditorWindow : Form
    {
        public MapEditorWindow()
        {
            InitializeComponent();
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
        }
    }
}
