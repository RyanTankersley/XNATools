using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using XNATools.SpriteEngine.Objects.Animation;

namespace XNATools.SpriteEngine.Collections
{
    public class Animations : List<Animation>
    {
        public const string STANDBY_NAME = "standby";
        private Animation standByAnimation = null;
        public Animation StandbyAnimation 
        { 
            get 
            {
                if (standByAnimation == null)
                {
                    this.standByAnimation = FindAnimationByName(STANDBY_NAME);
                }

                return this.standByAnimation;
            } 
        }

        /// <summary>
        /// The constructor.  Requires a standby animation with the name of the constant "STANDBY_NAME".
        /// Otherwise throws an exception
        /// </summary>
        /// <param name="standbyAnimation">The standby animation  with the name of the constant "STANDBY_NAME"</param>
        public Animations(Animation standbyAnimation)
        {
            if (standbyAnimation != null && standbyAnimation.Name == STANDBY_NAME)
            {
                base.Add(standbyAnimation);
            }
            else
            {
                throw new Exception("Invalid Standby Animation.");
            }

        }

        /// <summary>
        /// Finds the animation in the list of animations with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Animation FindAnimationByName(string name)
        {
            Animation toReturn = null;

            foreach (Animation a in this)
            {
                if (a.Name == name)
                {
                    toReturn = a;
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Loads in the content of all the animations in the list
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            foreach (Animation a in this)
            {
                a.LoadContent(content);
            }
        }
    }
}
