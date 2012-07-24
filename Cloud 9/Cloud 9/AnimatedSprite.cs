using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Cloud_9
{
    class AnimatedSprite : Sprite
    {
        // The current animation
        public String currentAnimation;

        // The animation dictionary
        public Dictionary<String, Animation> Animations = new Dictionary<String, Animation>(); // Dictionary containing all animations for a variable

        // The current row and column
        public int row;
        public int column;

        // The elapsed time
        public float elapsedTime;

        // The frame source
        public Rectangle animationSource;
        
        /// <summary>
        /// Adds an animation to the dictionary.
        /// </summary>
        /// <param name="name">Animation name</param>
        /// <param name="rows">Amount of rows in the animation</param>
        /// <param name="columns">Amount of columns in the animation</param>
        /// <param name="fps">Frames per second</param>
        /// <param name="startCoords">Start coords in the animation file</param>
        public void AddAnimation(Animation ani, String name, int rows, int columns, float fps, Vector2 startCoords)
        {
            // Copying all information into the animation (NEED SOME CHANGES)
            ani.rows = rows - 1; // Like here
            ani.columns = columns - 1; // And here, need to make it not subtract but still work right
            ani.fps = fps;
            ani.startCoords = startCoords;
            ani.frameSize = new Rectangle(0, 0, texture.Width / columns, texture.Height / rows);

            // Adds the animation to the dictionary
            Animations.Add(name, ani);
        }

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void UpdateAnimation(GameTime gameTime)
        {
            // Updates the elapsed time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // If the elapsed time is bigger than the frames per second
            if (elapsedTime > Animations[currentAnimation].fps)
            {
                // Then go to the next column
                column++;

                // If column is bigger than the amount of columns available
                if (column > Animations[currentAnimation].columns)
                {
                    // Then go to the next row
                    row++;
                    column = 0;

                    // If it's the last row
                    if (row > Animations[currentAnimation].rows)
                    {
                        // Go back to the start
                        column = 0;
                        row = 0;
                    }
                }
                
                // Reset the elapsed time
                elapsedTime = 0f;

                // Update the frame source
                animationSource = new Rectangle((int)(Animations[currentAnimation].startCoords.X + (column * Animations[currentAnimation].frameSize.Width)),
                    (int)(Animations[currentAnimation].startCoords.Y + (row * Animations[currentAnimation].frameSize.Height)),
                    Animations[currentAnimation].frameSize.Width,
                    Animations[currentAnimation].frameSize.Height);
            }
        }

        /// <summary>
        /// Draws the animation.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the animation
            spriteBatch.Draw(texture, position, animationSource, Color.White, rotation, origin, scale, spriteEffect, 0);
        }
    }
}
