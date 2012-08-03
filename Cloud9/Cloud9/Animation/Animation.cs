using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Cloud9
{    
    public class Animation
    {
        #region Properties
        public float FrameTime;
        public Texture2D Texture;
        public int FrameCount;
        public bool Looping;
        public float FrameWidth
        {
            get { return Texture.Width / FrameCount; }
        }
        #endregion

        #region Initialization
        public Animation()
        {
        }
        public Animation(float frameTime, int frameCount, bool looping, string texturePath)
        {
            FrameTime = frameTime;
            FrameCount = frameCount;
            Looping = looping;
            Texture = World.Content.Load<Texture2D>(texturePath);
        }
        #endregion
    }
    public class AnimationPlayer
    {
        #region Properties
        public Dictionary<string, Animation> Animations;
        Animation playingAnimation;
        float frameTime;
        int frameCount;
        Rectangle srcRect;
        #endregion

        #region Methods

        public void PlayAnimation(string name)
        {
            Animation a;
            if (!Animations.TryGetValue(name, out a))
                throw new KeyNotFoundException("Could not find animation : " + name);
            if (a == playingAnimation)
                return;

            playingAnimation = a;
            frameCount = 0;
            frameTime = 0;
            CalculateSourceRectangle();
        }

        private void CalculateSourceRectangle()
        {
            srcRect = new Rectangle(frameCount * (int)playingAnimation.FrameWidth,
                0,
                (int)playingAnimation.FrameWidth,
                playingAnimation.Texture.Height);
        }


        public void Update()
        {
            if (playingAnimation == null)
                return;

            frameTime += World.ElapsedSeconds; 

            if (frameTime >= playingAnimation.FrameTime)
            {
                frameTime = 0;
                frameCount++;
                if (frameCount >= playingAnimation.FrameCount)
                {
                    if (playingAnimation.Looping)
                        frameCount -= 2;
                    else
                        frameCount--;
                    // if it isn't looping, and we are finished the animation, we just stay on the last frame
                }
                
                
            }

            CalculateSourceRectangle();
        }

        public Rectangle GetSourceRectangle()
        {
            return srcRect;
        }

        public Texture2D GetTexture()
        {
            return playingAnimation.Texture;
        }

        public Vector2 GetOrigin()
        {
            return new Vector2(srcRect.Width / 2, srcRect.Height / 2);
        }
        #endregion
    }

    // the sprite class is the link between entities and animationplayers
    // atm it does nothing, but making it now will make it easier to implement
    // things with multiple animationplayers
    public class Sprite
    {
        #region Properties
        AnimationPlayer animationPlayer;
        #endregion

        #region Initializeation
        public Sprite(Dictionary<string, Animation> animations)
        {
            animationPlayer = new AnimationPlayer
            {
                Animations = animations
            };
        }
        #endregion

        #region Methods
        // at the moment this is just linking animationplayer to entity
        // eventually it will link multiple animtionplayers to entity
        public void PlayAnimation(string name)
        {
            animationPlayer.PlayAnimation(name);
        }
        public void Update()
        {
            animationPlayer.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, float rotation, SpriteEffects spriteEffects, Color color)
        {
            spriteBatch.Draw(animationPlayer.GetTexture(),
                position,
               animationPlayer.GetSourceRectangle(),
                color,
                rotation,
               animationPlayer.GetOrigin(),
                scale,
                spriteEffects,
                0);
        }


        #endregion
    }
}
