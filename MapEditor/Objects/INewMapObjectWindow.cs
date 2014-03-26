using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.MapEditor.Objects
{
    public interface INewMapObjectWindow
    {
        IMapDirectoryObject MapDirectoryObject { get; }

        System.Windows.Forms.DialogResult ShowDialog();
    }
}
