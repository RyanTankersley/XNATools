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
    public abstract partial class NewMapItemWindow : Form
    {
        public IMapDirectoryObject MapDirectoryObject { get { return GetMapDirectoryObject(); } set { SetMapDirectoryObject(value); } }

        public NewMapItemWindow()
        {
            InitializeComponent();
        }

        protected abstract IMapDirectoryObject GetMapDirectoryObject();

        protected abstract void SetMapDirectoryObject(IMapDirectoryObject MapDirectoryObject);
    }
}
