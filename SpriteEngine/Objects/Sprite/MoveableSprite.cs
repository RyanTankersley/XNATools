using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATools.SpriteEngine.Collections;

namespace XNATools.SpriteEngine.Objects.Sprite
{
    public class MoveableSprite : Sprite
    {
        public enum Direction
        {
            Standby,
            Up,
            Down,
            Left,
            Right,
            UpRight,
            DownRight,
            UpLeft,
            DownLeft
        }

        protected int baseSpeed = 0;
        protected int sprintSpeed = 0;
        protected int currentSpeed = 0;
        protected Direction currentDirection = Direction.Standby;
        protected Direction prevDirection = Direction.Standby;

        protected MoveableAnimations MoveableAnimations { get { return this.animations as MoveableAnimations; } }
        public bool IsSprinting { get { return this.currentSpeed != baseSpeed; } set { currentSpeed = value ? sprintSpeed : baseSpeed; } }

        public MoveableSprite(Vector2 position, MoveableAnimations animations, int baseSpeed, int sprintSpeed)
            : base(position, animations)
        {
            this.baseSpeed = baseSpeed;
            this.currentSpeed = baseSpeed;
            this.sprintSpeed = sprintSpeed;
        }

        /// <summary>
        /// Updates the position of the sprite.
        /// </summary>
        /// <param name="gameTime">The time of the game</param>
        public override void Update(GameTime gameTime)
        {
            UpdatePosition();
            base.Update(gameTime);
        }

        /// <summary>
        /// Updates the position of the sprite.
        /// </summary>
        protected void UpdatePosition()
        {
            prevPosition = position;

            //Changes the position based on what direction is pushed, and changes the animation
            switch (currentDirection)
            {
                case Direction.Up:
                    position.Y -= currentSpeed;
                    ChangeAnimation(this.MoveableAnimations.MoveUpAnimation);
                    break;
                case Direction.Down:
                    position.Y += currentSpeed;
                    ChangeAnimation(this.MoveableAnimations.MoveDownAnimation);
                    break;
                case Direction.Left:
                    position.X -= currentSpeed;
                    ChangeAnimation(this.MoveableAnimations.MoveLeftAnimation);
                    break;
                case Direction.Right:
                    position.X += currentSpeed;
                    ChangeAnimation(this.MoveableAnimations.MoveRightAnimation);
                    break;
                case Direction.Standby:
                    ChangeAnimation(this.MoveableAnimations.StandbyAnimation);
                    break;
                case Direction.UpRight:
                    position.Y -= currentSpeed;
                    position.X += currentSpeed;
                    if (prevDirection != Direction.UpRight)
                    {
                        if (prevDirection == Direction.Right || prevDirection == Direction.DownRight)
                        {
                            ChangeAnimation(this.MoveableAnimations.MoveUpAnimation);
                        }
                        else
                        {
                            ChangeAnimation(this.MoveableAnimations.MoveRightAnimation);
                        }
                    }
                    break;
                case Direction.UpLeft:
                    position.Y -= currentSpeed;
                    position.X -= currentSpeed;
                    if (prevDirection != Direction.UpLeft)
                    {
                        if (prevDirection == Direction.Left || prevDirection == Direction.DownLeft)
                        {
                            ChangeAnimation(this.MoveableAnimations.MoveUpAnimation);
                        }
                        else
                        {
                            ChangeAnimation(this.MoveableAnimations.MoveLeftAnimation);
                        }
                    }
                    break;
                case Direction.DownRight:
                    position.Y += currentSpeed;
                    position.X += currentSpeed;
                    if (prevDirection != Direction.DownRight)
                    {
                        if (prevDirection == Direction.Right || prevDirection == Direction.UpRight)
                        {
                            ChangeAnimation(this.MoveableAnimations.MoveDownAnimation);
                        }
                        else
                        {
                            ChangeAnimation(this.MoveableAnimations.MoveRightAnimation);
                        }
                    }
                    break;
                case Direction.DownLeft:
                    position.Y += currentSpeed;
                    position.X -= currentSpeed;
                    if (prevDirection != Direction.DownLeft)
                    {
                        if (prevDirection == Direction.Left || prevDirection == Direction.UpLeft)
                        {
                            ChangeAnimation(this.MoveableAnimations.MoveDownAnimation);
                        }
                        else
                        {
                            ChangeAnimation(this.MoveableAnimations.MoveLeftAnimation);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Changes the direction of the sprite
        /// </summary>
        /// <param name="direction">Changes the direction of the sprite to the new direction</param>
        public void ChangeDirection(Direction direction)
        {
            this.prevDirection = this.currentDirection;
            this.currentDirection = direction;
        }

        /// <summary>
        /// Keeps the sprite inside of the client bounds
        /// </summary>
        /// <param name="clientBounds">The client bounds to keep the sprite within</param>
        public void KeepInClientBounds(Rectangle clientBounds)
        {
            if (position.Y < 20)
            {
                position = prevPosition;
                ChangeAnimation(this.MoveableAnimations.StandbyAnimation);
            }
            if (position.Y > clientBounds.Height - this.CurrentAnimation.Height + 25)
            {
                position = prevPosition;
                ChangeAnimation(this.MoveableAnimations.StandbyAnimation);
            }
            if (position.X < 15)
            {
                position = prevPosition;
                ChangeAnimation(this.MoveableAnimations.StandbyAnimation);
            }
            if (position.X > clientBounds.Width - this.CurrentAnimation.Width + 10)
            {
                position = prevPosition;
                ChangeAnimation(this.MoveableAnimations.StandbyAnimation);
            }
        }
    }
}
