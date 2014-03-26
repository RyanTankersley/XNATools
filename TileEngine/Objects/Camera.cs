using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XNATools.TileEngine.Objects
{
    public class Camera
    {
        private Vector2 location = Vector2.Zero;
        private Vector2 size = Vector2.Zero;
        private Vector2 speed = Vector2.Zero;
        private System.Diagnostics.Stopwatch timer = null;
        private bool moveable = true;

        public Vector2 Location { get { return this.location; } set { this.location = value; } }
        public Vector2 Size { get { return this.size; } set { this.size = value; } }
        public Vector2 Speed { get { return this.speed; } set { this.speed = value; } }
        public bool Moveable { get { return this.moveable; } set { this.moveable = value; } }

        public Camera(Vector2 location, Vector2 size, Vector2 speed)
        {
            this.location = location;
            this.size = size;
            this.speed = speed;
            this.moveable = true;
            timer = new System.Diagnostics.Stopwatch();
            timer.Start();
        }

        public Camera(Vector2 location, Vector2 size, Vector2 speed, bool moveable)
        {
            this.location = location;
            this.size = size;
            this.speed = speed;
            this.moveable = moveable;
            timer = new System.Diagnostics.Stopwatch();
            timer.Start();
        }

        /// <summary>
        /// Updates the location of the camera, keeping it in bounds of the map size.  
        /// There is a very short timer to keep it from allowing instant keypresses.  
        /// Also, the flag Moveable will fix the camera in place
        /// </summary>
        /// <param name="mapSize">The bounds of the map that the camera cannot move past</param>
        public void Update(Vector2 mapSize)
        {
            KeyboardState ks = Keyboard.GetState();
            if (timer.ElapsedMilliseconds >= 5 && this.moveable)
            {
                if (ks.IsKeyDown(Keys.Left))
                {
                    this.location.X = MathHelper.Clamp(this.Location.X - speed.X, 0, (mapSize.X - size.X));
                }

                if (ks.IsKeyDown(Keys.Right))
                {
                    this.location.X = MathHelper.Clamp(this.Location.X + speed.X, 0, (mapSize.X - size.X));
                }

                if (ks.IsKeyDown(Keys.Up))
                {
                    this.location.Y = MathHelper.Clamp(this.Location.Y - speed.Y, 0, (mapSize.Y - size.Y));
                }

                if (ks.IsKeyDown(Keys.Down))
                {
                    this.location.Y = MathHelper.Clamp(this.Location.Y + speed.Y, 0, (mapSize.Y - size.Y));
                }
                timer.Restart();
            }
        }
    }
}
