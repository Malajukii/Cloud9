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

        // Moving horizontal
        Vector2 velocity;

        // Moving vertical
        Vector2 startPosition;
        bool isJumping = false;
        float jumpHeight = 150.0f;
        float gravity = 1.5f;

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
            // It's not override, because the parameters are different.

            // Gets the current keyboardstate
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Updates the player movement
            UpdateMovement(currentKeyboardState);
            UpdateJump(currentKeyboardState);

            // Sets the previous keyboardstate to the current;
            previousKeyboardState = currentKeyboardState;

            base.Update(gameTime, velocity);
        }

        /// <summary>
        /// Updates the player movement.
        /// </summary>
        /// <param name="currentKeyboardState">The current keyboardstate</param>
        public void UpdateMovement(KeyboardState currentKeyboardState)
        {
            // Sets both the variables to zero to stop the player from moving
            velocity = Vector2.Zero;

            // Testing if jumping lowers position too much
            Console.WriteLine(position);

            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                // Sets the speed to 150f and direction to left
                velocity.X = -movingSpeed;

                // Flips the sprite to face left
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                // Sets the speed to 150f and direction to right
                velocity.X = movingSpeed;

                // Flips the sprite back to let it face right
                spriteEffect = SpriteEffects.None;
            }
        }

        /// <summary>
        /// Updates the player jump (PROBABLY NEED A BETTER WAY TO DO JUMPING AND GRAVITY)
        /// </summary>
        /// <param name="currentKeyboardState">The current keyboardstate</param>
        public void UpdateJump(KeyboardState currentKeyboardState)
        {
            if (!isJumping && (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
            {
                // Updates the start position to the current position
                startPosition = position;

                // Set state to jumping
                isJumping = true;
            }
            if (isJumping)
            {               
                // Updates the velocity
                velocity.Y -= jumpHeight;
                jumpHeight -= gravity;

                // Updates the start position
                if (position.Y > startPosition.Y)
                {
                    startPosition = position;
                    jumpHeight = 150.0f;
                    isJumping = false;
                }
            }
        }
    }
}
