using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using XNATools.SpriteEngine.Collections;
using XNATools.SpriteEngine.Objects.Animation;

namespace XNATools.SpriteEngine.Objects.Sprite
{
    public class Sprite
    {
        protected XNATools.SpriteEngine.Collections.Animations animations = null;
        protected Animation.Animation currentAnimation = null;
        protected Animation.Animation previousAnimation = null;
        protected Vector2 position = new Vector2(200, 200);
        protected Vector2 prevPosition = new Vector2(200, 200);
        public Animation.Animation CurrentAnimation { get { return this.currentAnimation; } }
        public Animation.Animation PreviousAnimation { get { return this.previousAnimation; } }

        public Sprite(Vector2 position, XNATools.SpriteEngine.Collections.Animations animations)
        {
            this.position = position;
            this.prevPosition = position;
            this.animations = animations;
            this.currentAnimation = this.animations.StandbyAnimation;
        }

        public void LoadContent(ContentManager content)
        {
            this.animations.LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentAnimation != null)
            {
                spriteBatch.Draw(this.currentAnimation.SpriteTexture, position, this.currentAnimation.Source, Color.White, 0f, this.currentAnimation.Origin, 1.0f, this.currentAnimation.Effects, 0);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.currentAnimation != null)
            {
                this.currentAnimation.Animate(gameTime);
            }
        }

        protected void ChangeAnimation(Animation.Animation animation)
        {
            if (currentAnimation != animation)
            {
                previousAnimation = currentAnimation;
                previousAnimation.CurrentFrame = 0;
                currentAnimation = animation;
            }
        }
    }
}
