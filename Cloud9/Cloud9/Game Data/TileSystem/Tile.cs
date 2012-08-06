using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Cloud9
{
    public class Tile : IGameData
    {
        #region Properties
        // more might be needed later
        public bool Collides;
        public int TileIndex;
        // Going to do something with this later, mayby if it's true add ifs for checking which edge is needed
        public Boolean HasEdges;
        public String FileName;
        #endregion

        #region Static
        // Tile size
        public const int Size = 16;
        
        // List of all tiles
        static Tile[] tiles;
        static Tile air, dirt, stone;
        
        // Get for tiles
        public static Tile Air
        {
            get { return air; }
        }
        public static Tile Dirt
        {
            get { return dirt; }
        }
        public static Tile Stone
        {
            get { return stone; }
        }

        static Tile()
        {
            stone = new Tile
            {
                TileIndex = 20,
                HasEdges = true,
                Collides = true,
                FileName = "Cloud 9/blocks/Terrain"
            };
            dirt = new Tile
            {
                TileIndex = 1,
                HasEdges = true,
                Collides = true,
                FileName = "Cloud 9/blocks/Terrain"
            };
            air = new Tile
            {
                TileIndex = 0,
                HasEdges = false,
                Collides = false,
                FileName = "Cloud 9/blocks/Terrain"
            };

            tiles = new Tile[] { air, dirt, stone };
        }
        public static Tile GetTile(byte i)
        {
            return tiles[i];
        }
        public static byte GetByte(Tile t)
        {
            return (byte)Array.IndexOf(tiles, t);
        }
        public static Rectangle GetBounds(int x, int y)
        {
            return new Rectangle(x * Size,
                y * Size,
                Size,
                Size);
        }
        public Rectangle GetSource()
        {
            return new Rectangle((TileIndex % (getTextureFile(FileName).Width / Tile.Size)) * Tile.Size, (TileIndex / (getTextureFile(FileName).Width / Tile.Size)) * Tile.Size, Tile.Size, Tile.Size);
        }
        public int CompareTo(object obj)
        {
            return 0;
        }
        public Texture2D getTextureFile(String fileName)
        {
            Texture2D textureFile = World.Instance.Game.Content.Load<Texture2D>(fileName);
            return textureFile;
        }
        #endregion

        #region Methods
        public void Draw(int x, int y, Layer layer)
        {
            Rectangle bounds = GetBounds(x, y);
            Rectangle source = GetSource();
            bounds.X -= (int)World.Instance.CameraPosition.X;
            bounds.Y -= (int)World.Instance.CameraPosition.Y;
            World.Instance.SpriteBatch.Draw(getTextureFile(FileName), bounds, source, Color.White);
        }
        #endregion

    }
}
