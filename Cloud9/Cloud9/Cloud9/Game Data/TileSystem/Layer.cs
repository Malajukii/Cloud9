using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Cloud9
{
    public class Layer
    {
        #region Properties
        byte[] tileData;

        int Width
        {
            get { return World.Width; }
        }
        int Height
        {
            get { return World.Height; }
        }


        Collection<Entity> InActiveEntities;
        Collection<Entity> ActiveEntities;
        #endregion

        #region Initialize
        // these are seperated into 2 functions so we can have layers that have just tiles, and layers with just entities.
        // mainly for the 2 buffer layers
        public void InitTileData()
        {
            tileData = new byte[Width * Height];
        }
        public void InitEntityLists()
        {
            InActiveEntities = new Collection<Entity>();
            ActiveEntities = new Collection<Entity>();
        }
        #endregion

        #region Methods
        public bool HasTiles()
        {
            return tileData != null;
        }
        public void Update()
        {
            foreach (Entity e in ActiveEntities)
                e.Update();

            InActiveEntities.Update();
            ActiveEntities.Update();
        }

        public void Draw()
        {
            Entity[] sortedEntites = ActiveEntities.ToArray();
            Array.Sort(sortedEntites);
            foreach (Entity e in sortedEntites)
                e.Draw();

            if (tileData != null)
            {
                int left = (int)((World.Instance.CameraPosition.X) / Tile.Size);
                int right = (int)((World.Instance.CameraPosition.X + World.ScreenSize.X) / Tile.Size) + 1;
                int top = (int)((World.Instance.CameraPosition.Y) / Tile.Size);
                int bottom = (int)((World.Instance.CameraPosition.Y + World.ScreenSize.Y) / Tile.Size) + 1;

                if (left < 0)
                    left = 0;
                if (right > Width)
                    right = Width;
                if (top < 0)
                    top = 0;
                if (bottom > Height)
                    bottom = Height;

                for (int x = left; x < right; x++)
                {
                    for (int y = top; y < bottom; y++)
                    {
                        if (!isValidTile(x, y))
                            continue;
                        
                        Tile t = GetTile(x, y);
                        if (t == Tile.Air)
                            continue;                        
                        t.Draw(x, y, this);
                    }
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (!isValidTile(x, y))
                throw new IndexOutOfRangeException("Tile out of range in Tile.GetTile");
            return Tile.GetTile(tileData[y * Width + x]);
        }
        public bool isValidTile(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
        public void SetTile(int x, int y, Tile t)
        {
            if (!isValidTile(x, y))
                throw new IndexOutOfRangeException("Tile out of range in Tile.SetTile");
            tileData[y * Width + x] = Tile.GetByte(t);
        }

        public Collection<Entity> GetEntities(bool active)
        {
            if (active)
                return ActiveEntities;
            return InActiveEntities;
        }
        #endregion

    }
}
