using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.MapEditor.Collections;

namespace XNATools.MapEditor.Objects
{
    public class ObjectDirectory : IMapDirectoryObject, IMapDirectoryContainer
    {
        protected long id;
        protected string name = string.Empty;
        protected ObjectDirectories objectDirectories;
        protected MapObjects mapObjects;

        public long ID { get { return this.id; } set { this.id = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public ObjectDirectories ObjectDirectories
        {
            get
            {
                if (this.objectDirectories == null)
                {
                    this.objectDirectories = ObjectDirectories.GetObjectDirectories(this.id);
                }

                return this.objectDirectories;
            }
            set { this.objectDirectories = value; }
        }
        public MapObjects MapObjects 
        { 
            get 
            {
                if (this.mapObjects == null)
                {
                    this.mapObjects = MapObjects.GetMapObjects(this.id);
                }

                return this.mapObjects; 
            } 
            set { this.mapObjects = value; } 
        }

        public ObjectDirectory()
        {

        }

        public ObjectDirectory(string name)
        {
            this.id = 0;
            this.name = name;
            this.objectDirectories = new ObjectDirectories();
            this.mapObjects = new MapObjects();
        }

        public void Remove(IMapDirectoryObject toRemove)
        {
            if (toRemove is MapObject)
            {
                this.mapObjects.Remove(toRemove);
            }
            else if(toRemove is ObjectDirectory)
            {
                this.objectDirectories.Remove(toRemove);
            }
        }

        public void Add(IMapDirectoryObject toAdd)
        {
            if (toAdd is MapObject)
            {
                this.mapObjects.Add(toAdd);
            }
            else if (toAdd is ObjectDirectory)
            {
                this.objectDirectories.Add(toAdd);
            }
        }
    }
}
