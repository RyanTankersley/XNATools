using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNATools.TileEngine.Objects;

namespace XNATools.TileEngine.Collections
{
    public class MapRows : List<MapRow>
    {
        public MapRows()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Draw(spriteBatch, camera, i);
            }
        }
    }
}
