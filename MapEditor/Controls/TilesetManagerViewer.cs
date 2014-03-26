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
using XNATools.MapEditor.GUI;

namespace XNATools.MapEditor.Controls
{
    public partial class TilesetManagerViewer : UserControl
    {
        public TilesetManagerViewer()
        {
            InitializeComponent();
        }

        private void TilesetManagerViewer_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.ucTilesetTree.LoadTilesets(Tilesets.GetTilesets());
            }
        }

        private void ucTilesetTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.bAdd.Enabled = e.Node.Level == 0 || e.Node.Tag is ObjectDirectory;
            this.bEdit.Enabled = e.Node.Level != 0;
            this.bDelete.Enabled = e.Node.Level != 0;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            AddToNode(this.ucTilesetTree.SelectedNode);
        }

        public void AddToNode(TreeNode parentNode)
        {
            INewMapObjectWindow win = null;
            IMapDirectoryContainer container = null;
            IMapDirectoryObject mapObject = null;
            TreeNode newNode = null;

            if (parentNode != null)
            {
                container = parentNode.Tag as IMapDirectoryContainer;

                win = parentNode.Level == 0 ? (INewMapObjectWindow)new NewTilesetWindow() : (INewMapObjectWindow)new NewDirectoryChildWindow();
                if (win.ShowDialog() == DialogResult.OK)
                {
                    mapObject = win.MapDirectoryObject;
                }

                if (mapObject != null && container != null)
                {
                    container.Add(mapObject);
                    newNode = new TreeNode(mapObject.Name);
                    newNode.Tag = mapObject;
                    parentNode.Nodes.Add(newNode);
                }
            }
        }

        /*private void bAdd_Click(object sender, EventArgs e)
        {
            NewTilesetWindow tWin = null;
            NewDirectoryChildWindow dWin = null;
            Tilesets ts = null;
            Tileset t = null;
            ObjectDirectory d = null;
            ObjectDirectory curr = null;
            MapObject m = null;
            TreeNode node = null;

            if (this.ucTilesetTree.SelectedNode != null)
            {
                if (this.ucTilesetTree.SelectedNode.Level == 0)
                {
                    tWin = new NewTilesetWindow();
                    if (tWin.ShowDialog(this) == DialogResult.OK)
                    {
                        t = tWin.Tileset;
                        if (t != null)
                        {
                            ts = this.ucTilesetTree.Tag as Tilesets;
                            if (ts != null)
                            {
                                ts.Add(t);
                                node = new TreeNode(t.Name);
                                node.Tag = t;
                                this.ucTilesetTree.SelectedNode.Nodes.Add(node);
                                this.ucTilesetTree.SelectedNode.Expand();
                            }
                            else
                            {
                                MessageBox.Show("Error: Invalid Loading Tileset into List.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error: Invalid Tileset entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    dWin = new NewDirectoryChildWindow();
                    if (dWin.ShowDialog(this) == DialogResult.OK)
                    {
                        d = dWin.ObjectDirectory;
                        m = dWin.MapObject;
                        curr = this.ucTilesetTree.SelectedNode.Tag as ObjectDirectory;

                        if (curr != null && d != null)
                        {
                            node = new TreeNode(d.Name);
                            node.Tag = d;
                            this.ucTilesetTree.SelectedNode.Nodes.Add(node);
                            curr.ObjectDirectories.Add(d);
                            this.ucTilesetTree.SelectedNode.Expand();
                        }
                        else if (curr != null && m != null)
                        {
                            node = new TreeNode(m.Name);
                            node.Tag = m;
                            this.ucTilesetTree.SelectedNode.Nodes.Add(node);
                            curr.MapObjects.Add(m);
                            this.ucTilesetTree.SelectedNode.Expand();
                        }
                    }
                }
            }
        }*/

        private void bEdit_Click(object sender, EventArgs e)
        {

        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            IMapDirectoryContainer container = null;
            IMapDirectoryObject mapObject = null;
            TreeNode selected = this.ucTilesetTree.SelectedNode;

            if (selected != null && selected.Parent != null)
            {
                mapObject = selected.Tag as IMapDirectoryObject;
                container = selected.Parent.Tag as IMapDirectoryContainer;
                container.Remove(mapObject);
            }
        }

        public void Save()
        {
            Tilesets ts = this.ucTilesetTree.Tag as Tilesets;

            if (ts != null && ts.Count > 0)
            {
                ts.Save();
            }
        }

        private void cmsTree_Opening(object sender, CancelEventArgs e)
        {
            TreeNode node = this.ucTilesetTree.SelectedNode;

            this.tsmiAdd.Enabled = node != null && (node.Level == 0 || node.Tag is ObjectDirectory);
            this.tsmiEdit.Enabled = node != null && node.Level != 0;
            this.tsmiDelete.Enabled = node != null && node.Level != 0;
        }
    }
}
