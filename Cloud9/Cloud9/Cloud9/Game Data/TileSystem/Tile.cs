using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Cloud9
{
    public class Tile
    {
        #region Properties
        // more might be needed later
        public bool Collides;
        public Texture2D Texture;
        #endregion

        #region Static
        public const int Size = 16;
        static Tile[] tiles;
        static Tile dirt, air;
        public static Tile Dirt
        {
            get { return dirt; }
        }
        public static Tile Air
        {
            get { return air; }
        }
        static Tile()
        {
            dirt = new Tile
            {
                Texture = World.Content.Load<Texture2D>("Cloud 9/blocks/Dirt"),
                Collides = true
            };
            air = new Tile
            {
                Texture = World.Content.Load<Texture2D>("Cloud 9/blankpixel"),
                Collides = false
            };


            tiles = new Tile[] { air, dirt };
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
        #endregion

    }
}
