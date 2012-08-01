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

namespace Cloud9
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            World.ScreenSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            graphics.PreferredBackBufferWidth = (int)World.ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)World.ScreenSize.Y;

            
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            World.Initialize(this);
            Input.Initialize(this);

            Components.Add(World.Instance);
            Components.Add(Input.Instance);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {          
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            World.Instance.SpriteBatch.Begin();
            World.Instance.SpriteBatch.Draw(Content.Load<Texture2D>("Cloud 9/Backgrounds/Bcrnd_bottom"), new Rectangle(0, 0, (int)World.ScreenSize.X, (int)World.ScreenSize.Y), Color.White);


            World.Instance.SpriteBatch.Draw(Content.Load<Texture2D>("Cloud 9/Backgrounds/Bcrnd_middle"), new Rectangle(0, 0, (int)World.ScreenSize.X, (int)World.ScreenSize.Y), Color.White);
            World.Instance.SpriteBatch.Draw(Content.Load<Texture2D>("Cloud 9/Backgrounds/Bcrnd_top"), new Rectangle(0, 0, (int)World.ScreenSize.X, (int)World.ScreenSize.Y), Color.White);


            //World.Instance.SpriteBatch.Draw(Content.Load<Texture2D>("Cloud 9/Backgrounds/Big clouds"), new Rectangle(0, 0, (int)World.ScreenSize.X, (int)World.ScreenSize.Y), Color.White);
            World.Instance.SpriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "Fps : " + Math.Round(1f / (float)gameTime.ElapsedGameTime.TotalSeconds), Vector2.Zero, Color.White);
            World.Instance.SpriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
