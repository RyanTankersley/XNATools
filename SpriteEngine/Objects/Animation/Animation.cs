using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNATools.SpriteEngine.Objects.Animation
{
    public class Animation
    {
        protected Texture2D spriteTexture;
        protected string contentName;
        protected string name = string.Empty;
        protected int frameCount = 0;
        protected int pictureOffset = 0;
        protected int currentFrame = 0;
        protected int startX = 0;
        protected int startY = 0;
        protected int width = 0;
        protected int height = 0;
        //Tells if the frame starts over at 0 (true) or reverses 0, 1, 2, 1, 0, 1... (false)
        protected bool loop = true;
        //Tells if the animation is in reverse (only happens when loop is false)
        protected bool reverse = false;
        protected int interval = 0;
        protected float timer = 0;
        protected SpriteEffects effects = SpriteEffects.None;
        protected Rectangle source = Rectangle.Empty;
        protected Vector2 origin = new Vector2(0, 0);

        public Texture2D SpriteTexture { get { return this.spriteTexture; } }
        public string Name { get { return this.name; } }
        public Rectangle Source { get { return this.source; } }
        public Vector2 Origin { get { return this.origin; } }
        public SpriteEffects Effects { get { return this.effects; } }
        public int CurrentFrame { set { this.currentFrame = value; } }
        public int Width { get { return this.width; } }
        public int Height { get { return this.height; } }

        public Animation(string contentName, string name, int frameCount,
                         int pictureOffset, int startX, int startY,
                         int width, int height, bool loop, int interval, SpriteEffects effects)
        {
            this.contentName = contentName;
            this.name = name;
            this.frameCount = frameCount;
            this.pictureOffset = pictureOffset;
            this.startX = startX;
            this.startY = startY;
            this.width = width;
            this.height = height;
            this.loop = loop;
            this.reverse = false;
            this.interval = interval;
            this.effects = effects;
            this.source = new Rectangle(startX, startY, width, height);
            this.origin = new Vector2(source.Width / 2, source.Height / 2);
        }

        /// <summary>
        /// Loads the sprite texture into the content manager
        /// </summary>
        /// <param name="content">The content manager to use to load the sprite texture</param>
        public void LoadContent(ContentManager content)
        {
            this.spriteTexture = content.Load<Texture2D>(this.contentName);
        }

        /// <summary>
        /// Changes the status of the animation of the sprite
        /// </summary>
        /// <param name="gameTime">The time of the game</param>
        public void Animate(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //If the timer has passed the given interval to change sprite frames
            if (timer > interval)
            {
                //Changes the current frame, up if not reversing, backwards if reversing
                currentFrame += !reverse ? 1 : -1;

                //If it has reached the end of the frames
                if (currentFrame >= frameCount)
                {
                    //Starts over if in a loop
                    if (loop)
                    {
                        currentFrame = 0;
                    }
                    //Goes to the previous frame and puts the animation into reverse
                    else
                    {
                        currentFrame -= 2;
                        reverse = true;
                    }
                }
                //If it has gotten below the first frame
                else if (currentFrame < 0)
                {
                    //Turns off reverse and sets it at the frame after the first frame
                    reverse = false;
                    currentFrame += 2;
                }

                timer = 0f;
                //Re sets the source based on the current change of frame
                source = new Rectangle(startX + (currentFrame * pictureOffset), startY, width, height);
            }
        }
    }
}
