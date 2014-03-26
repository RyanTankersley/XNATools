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

        /// <summary>
        /// Starts a new Map Project
        /// </summary>
        /// <param name="spriteFileName">The path to the sprite sheet that will be used to create the maps.</param>
        public void StartNew(string spriteFileName)
        {
            if (!string.IsNullOrEmpty(spriteFileName) && File.Exists(spriteFileName))
            {
                this.ucMapEditor.LoadViewers(spriteFileName, new Vector2(32, 32));
            }
            else
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
