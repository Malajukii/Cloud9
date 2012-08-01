using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Cloud9
{
    public class World : DrawableGameComponent
    {
        #region Singleton
        static World instance;
        public static World Instance
        {
            get
            {
                if (instance == null)
                    throw new NullReferenceException("World has not yet been initialized");
                return instance;
            }
        }
        #endregion

        #region Initialization
        private World(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            layers = new Layer[5]; // 3 layers and 2 "buffer" layers, that will remain empty, but allow the player to go outside of the 1st and 3rd layers

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer();
                layers[i].InitEntityLists();

                if (i != 0 && i != layers.Length - 1)
                    layers[i].InitTileData();
            }


          


        }
        void InitializePlayer()
        {
            player = new Player();
            layers[2].GetEntities(true).Add(player);
            cameraPosition = player.Position - ScreenSize / 2;

            // testing stuff
            for (int i = 0; i < Width; i++)
            {
                layers[2].SetTile(i, 50, Tile.Dirt);
                layers[2].SetTile(i, 51, Tile.Dirt);
                layers[2].SetTile(i, 52, Tile.Dirt);
                layers[3].SetTile(i, 53, Tile.Dirt);
                if (i > Width / 2)
                    layers[2].SetTile(i, 45, Tile.Dirt);
            }
            for (int i = 0; i < Height; i++)
            {
                layers[2].SetTile(50, i, Tile.Dirt);
            }
        }
        public static void Initialize(Game game)
        {
            if (instance != null)
                throw new System.StackOverflowException("World.Initialize can only be called once.");
            instance = new World(game);
            instance.InitializePlayer();
        }
        #endregion

        #region Properties

        public static Vector2 ScreenSize;

        Layer[] layers;

        GameTime gameTime;

        Vector2 cameraPosition;
        public Vector2 CameraPosition
        {
            get { return cameraPosition; }
        }
        Player player;
        public Player Player
        {
            get { return player;}
        }

        SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;

            Vector2 targetCameraPosition = player.Position - ScreenSize / 2;
            cameraPosition += (targetCameraPosition - cameraPosition) * ElapsedSeconds;
            foreach (Layer l in layers)
                l.Update();

            

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            this.gameTime = gameTime;

            spriteBatch.Begin();
            foreach (Layer l in layers)
                l.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Layer GetLayer(int i)
        {
            return layers[i];
        }
        #endregion

        #region Static Helper Methods
        public static float ElapsedSeconds
        {
            // gets the latest gameTime.ElapsedGameTime.TotalSeconds
            get { return (float)instance.gameTime.ElapsedGameTime.TotalSeconds; }
        }

        public static int Width
        {
            get { return 1000; }
        }
        public static int Height
        {
            get { return 1000; }
        }

        public static ContentManager Content
        {
            get { return instance.Game.Content; }
        }
        #endregion
    }
}
