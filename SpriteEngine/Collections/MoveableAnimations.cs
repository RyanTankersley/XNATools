using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.SpriteEngine.Objects.Animation;

namespace XNATools.SpriteEngine.Collections
{
    public class MoveableAnimations : Animations
    {
        public const string MOVE_LEFT_NAME = "moveLeft";
        public const string MOVE_RIGHT_NAME = "moveRight";
        public const string MOVE_UP_NAME = "moveUp";
        public const string MOVE_DOWN_NAME = "moveDown";

        private Animation moveLeftAnimation = null;
        public Animation MoveLeftAnimation
        {
            get
            {
                if (moveLeftAnimation == null)
                {
                    this.moveLeftAnimation = FindAnimationByName(MOVE_LEFT_NAME);
                }

                return this.moveLeftAnimation;
            }
        }

        private Animation moveRightAnimation = null;
        public Animation MoveRightAnimation
        {
            get
            {
                if (moveRightAnimation == null)
                {
                    this.moveRightAnimation = FindAnimationByName(MOVE_RIGHT_NAME);
                }

                return this.moveRightAnimation;
            }
        }

        private Animation moveUpAnimation = null;
        public Animation MoveUpAnimation
        {
            get
            {
                if (moveUpAnimation == null)
                {
                    this.moveUpAnimation = FindAnimationByName(MOVE_UP_NAME);
                }

                return this.moveUpAnimation;
            }
        }

        private Animation moveDownAnimation = null;
        public Animation MoveDownAnimation
        {
            get
            {
                if (moveDownAnimation == null)
                {
                    this.moveDownAnimation = FindAnimationByName(MOVE_DOWN_NAME);
                }

                return this.moveDownAnimation;
            }
        }

        public MoveableAnimations(Animation standByAnimation, Animation moveLeft, Animation moveRight,
                                Animation moveUp, Animation moveDown)
            : base(standByAnimation)
        {
            if (moveLeft != null && moveRight != null && moveUp != null && moveDown != null &&
               moveLeft.Name == MOVE_LEFT_NAME && moveRight.Name == MOVE_RIGHT_NAME && moveUp.Name == MOVE_UP_NAME && moveDown.Name == MOVE_DOWN_NAME)
            {
                base.Add(moveLeft);
                base.Add(moveRight);
                base.Add(moveUp);
                base.Add(moveDown);
            }
            else
            {
                throw new Exception("Invalid Moveable Animation Name.");
            }
        }
    }
}
