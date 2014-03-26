using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.MapEditor.Objects
{
    public interface IMapDirectoryContainer
    {
        void Remove(IMapDirectoryObject toRemove);

        void Add(IMapDirectoryObject toAdd);
    }
}
