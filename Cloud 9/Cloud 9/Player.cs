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
    class Player : Sprite
    {
        // Constants to make the code more understandable
        const float movingSpeed = 150;
        const int up = -1;
        const int down = 1;
        const int left = -1;
        const int right = 1;

        // Moving
        Vector2 speed;
        Vector2 direction;

        // Spawn coords (Once the World class is made it will be moved)
        public Vector2 spawn = new Vector2(200, 200);

        // Keyboard
        KeyboardState previousKeyboardState;

        /// <summary>
        /// Loads the player.
        /// </summary>
        /// <param name="content">ContentManager</param>
        public void LoadContent(ContentManager content)
        {
            base.LoadContent(content, "Player", spawn.X, spawn.Y);
        }

        /// <summary>
        /// Updates the player.
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime)
        {
            // Gets the current keyboardstate
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Updates the player movement
            UpdateMovement(currentKeyboardState);

            // Sets the previous keyboardstate to the current;
            previousKeyboardState = currentKeyboardState;

            base.Update(gameTime, speed, direction);
        }

        /// <summary>
        /// Updates the player movement.
        /// </summary>
        /// <param name="currentKeyboardState">The current keyboardstate</param>
        public void UpdateMovement(KeyboardState currentKeyboardState)
        {
            // Sets both the variables to zero to stop the player from moving
            speed = Vector2.Zero;
            direction = Vector2.Zero;

            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                // Sets the speed to 150f and direction to left
                speed.X = movingSpeed;
                direction.X = left;

                // Flips the sprite to face left
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                // Sets the speed to 150f and direction to right
                speed.X = movingSpeed;
                direction.X = right;

                // Flips the sprite back to let it face right
                spriteEffect = SpriteEffects.None;
            }
        }
    }
}
