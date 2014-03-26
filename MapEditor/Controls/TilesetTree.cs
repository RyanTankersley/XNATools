using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XNATools.MapEditor.Collections;
using XNATools.MapEditor.Objects;

namespace XNATools.MapEditor.Controls
{
    public partial class TilesetTree : TreeView
    {
        public TilesetTree()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the tilesets into the tree
        /// </summary>
        /// <param name="ts">The tilesets to load into the tree</param>
        public void LoadTilesets(Tilesets ts)
        {
            TreeNode top = new TreeNode("Tilesets");
            top.Tag = ts;
            TreeNode node = null;

            this.Nodes.Add(top);
            this.Tag = ts;

            foreach (Tileset t in ts)
            {
                node = new TreeNode(t.Name);
                node.Tag = t;
                top.Nodes.Add(node);
            }
        }

        private void TilesetTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ObjectDirectory od = null;
            TreeNode node = null;

            foreach (TreeNode cNode in e.Node.Nodes)
            {
                if (cNode.Tag is ObjectDirectory && cNode.Nodes.Count == 0)
                {
                    od = cNode.Tag as ObjectDirectory;

                    foreach (ObjectDirectory c in od.ObjectDirectories)
                    {
                        node = new TreeNode(c.Name);
                        node.Tag = c;
                        cNode.Nodes.Add(node);
                    }

                    foreach (MapObject c in od.MapObjects)
                    {
                        node = new TreeNode(c.Name);
                        node.Tag = c;
                        cNode.Nodes.Add(node);
                    }
                }
            }
        }
    }
}
