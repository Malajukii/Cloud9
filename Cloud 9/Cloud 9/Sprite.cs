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
    class Sprite
    {
        // Scaling, sizing, rotating and other drawing properties
        public Texture2D texture;
        public Vector2 position;
        public float scale = 1.0f;
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;

                // Recalculates the size with the new scale
                Size = new Rectangle(0, 0, (int)(texture.Width * scale), (int)(texture.Height * scale));
            }
        }
        public float rotation = 0.0f;
        public SpriteEffects spriteEffect = SpriteEffects.None;
        public Rectangle size;
        public Rectangle Size
        {
            get { return size; }
            set
            {
                size = value;

                // Recalculates the origin with the new size;
                origin = new Vector2(size.Width / 2, size.Height);
            }
        }
        public Vector2 origin;
        public Rectangle source;
        
        /// <summary>
        /// Initializes the sprite.
        /// </summary>
        /// <param name="content">ContentManager</param>
        /// <param name="fileName">The file name</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void LoadContent(ContentManager content, String fileName, float x, float y)
        {
            texture = content.Load<Texture2D>(fileName);
            position = new Vector2(x, y);
            Size = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(size.Width / 2, size.Height);
            source = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Updates the sprite position.
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        /// <param name="velocity">Velocity</param>
        public virtual void Update(GameTime gameTime, Vector2 velocity)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Draws the sprite on the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, source, Color.White, rotation, origin, scale, spriteEffect, 0);
        }
    }
}
