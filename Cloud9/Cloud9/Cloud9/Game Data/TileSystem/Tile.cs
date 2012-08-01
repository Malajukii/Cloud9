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
        public Texture2D Texture;
        public Texture2D EdgeTexture;
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
                EdgeTexture = World.Content.Load<Texture2D>("Cloud 9/blocks/dirtedge"),
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
        public int CompareTo(object obj)
        {
            return 0;
        }
        #endregion

        #region Methods
        public void Draw(int x, int y, Layer layer)
        {
            Rectangle bounds = GetBounds(x, y);
            bounds.X -= (int)World.Instance.CameraPosition.X;
            bounds.Y -= (int)World.Instance.CameraPosition.Y;            
            World.Instance.SpriteBatch.Draw(Texture, bounds, Color.White);
            if (EdgeTexture != null)
            {
                Vector2 origin = new Vector2(EdgeTexture.Width / 2, EdgeTexture.Height / 2);
                bounds.X += bounds.Width / 2;
                bounds.Y += bounds.Height / 2;
                if (x > 0)
                {
                    if (layer.GetTile(x - 1, y) == this)
                    {
                        // 90
                        World.Instance.SpriteBatch.Draw(EdgeTexture, bounds, null, Color.White, MathHelper.PiOver2, origin, SpriteEffects.None, 0);
                    }
                }
                if (x < World.Width - 1)
                {
                    if (layer.GetTile(x + 1, y) == this)
                    {
                        // -90
                        World.Instance.SpriteBatch.Draw(EdgeTexture, bounds, null, Color.White, -MathHelper.PiOver2, origin, SpriteEffects.None, 0);
                    }
                }
                if (y > 0)
                {
                    if (layer.GetTile(x, y - 1) == this)
                    {
                        // 0
                        World.Instance.SpriteBatch.Draw(EdgeTexture, bounds, null, Color.White, 0, origin, SpriteEffects.None, 0);
                    }
                }
                if (y < World.Height - 1)
                {
                    if (layer.GetTile(x, y + 1) == this)
                    {
                        // 180
                        World.Instance.SpriteBatch.Draw(EdgeTexture, bounds, null, Color.White, MathHelper.Pi, origin, SpriteEffects.None, 0);
                    }
                }
            }
        }
        #endregion

    }
}
