using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNATools.SpriteEngine.Collections;

namespace XNATools.SpriteEngine.Objects.Sprite
{
    public class UserMoveableSprite : MoveableSprite
    {
        protected KeyboardState currentKBState;
        protected KeyboardState previousKBState;
        protected double spaceTimer = 0;

        public UserMoveableSprite(Vector2 position, MoveableAnimations animations, int baseSpeed, int sprintSpeed)
            : base(position, animations, baseSpeed, sprintSpeed)
        {
            currentKBState = Keyboard.GetState();
        }

        /// <summary>
        /// On update, sets the direction of the sprite based on which keys are set.
        /// </summary>
        /// <param name="gameTime">The time of the game</param>
        public override void Update(GameTime gameTime)
        {
            previousKBState = currentKBState;
            currentKBState = Keyboard.GetState();
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            bool right = currentKBState.IsKeyDown(Keys.Right);
            bool left = currentKBState.IsKeyDown(Keys.Left);
            bool up = currentKBState.IsKeyDown(Keys.Up);
            bool down = currentKBState.IsKeyDown(Keys.Down);
            bool space = currentKBState.IsKeyDown(Keys.Space);
            
            spaceTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (space && spaceTimer > 200)
            {
                this.IsSprinting = !this.IsSprinting;
                spaceTimer = 0;
            }

            if (right && left && up && down)
            {
                ChangeDirection(MoveableSprite.Direction.Standby);
            }
            else if (right && left && up)
            {
                ChangeDirection(MoveableSprite.Direction.Up);
            }
            else if (right && left && down)
            {
                ChangeDirection(MoveableSprite.Direction.Down);
            }
            else if (right && up && down)
            {
                ChangeDirection(MoveableSprite.Direction.Right);
            }
            else if (left && up && down)
            {
                ChangeDirection(MoveableSprite.Direction.Left);
            }
            else if ((left && right) || (up && down))
            {
                ChangeDirection(MoveableSprite.Direction.Standby);
            }
            else if (left && down)
            {
                ChangeDirection(MoveableSprite.Direction.DownLeft);
            }
            else if (left && up)
            {
                ChangeDirection(MoveableSprite.Direction.UpLeft);
            }
            else if (right && down)
            {
                ChangeDirection(MoveableSprite.Direction.DownRight);
            }
            else if (right && up)
            {
                ChangeDirection(MoveableSprite.Direction.UpRight);
            }
            else if (currentKBState.IsKeyDown(Keys.Right))
            {
                ChangeDirection(MoveableSprite.Direction.Right);
            }
            else if (currentKBState.IsKeyDown(Keys.Left))
            {
                ChangeDirection(MoveableSprite.Direction.Left);
            }
            else if (currentKBState.IsKeyDown(Keys.Up))
            {
                ChangeDirection(MoveableSprite.Direction.Up);
            }
            else if (currentKBState.IsKeyDown(Keys.Down))
            {
                ChangeDirection(MoveableSprite.Direction.Down);
            }
            else
            {
                ChangeDirection(MoveableSprite.Direction.Standby);
            }

            base.Update(gameTime);
        }
    }
}
