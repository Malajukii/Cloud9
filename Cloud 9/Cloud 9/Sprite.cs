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
        // Texture and position.
        Texture2D texture;
        Vector2 position;

        /// <summary>
        /// Initializes the sprite.
        /// </summary>
        /// <param name="content">ContentManager</param>
        /// <param name="fileName">The file name</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Sprite(ContentManager content, String fileName, int x, int y)
        {
            texture = content.Load<Texture2D>(fileName);
            position = new Vector2(x, y);
        }

        /// <summary>
        /// Updates the sprite position.
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        /// <param name="speed">Moving speed</param>
        /// <param name="direction">Moving direction</param>
        public void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
        {
            position += speed * direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Draws the sprite on the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
