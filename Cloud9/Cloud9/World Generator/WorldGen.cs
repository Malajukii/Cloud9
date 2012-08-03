using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Cloud9
{
    enum IslandSize
    {
        Tiny,
        Small,
        Medium,
        Large,
        Giant
    }
    public static class WorldGen
    {
        // each island size takes up a certain amount of points
        // tiny = 1  (will have a couple of rocks... trees, maybe a pig, its very small
        // small = 3 (maybe a house or something)
        // medium = 5 (a village could be here, if it was small. or a big house with a farm)
        // large = 10 (small city)
        // giant = 20 (big city)
        
        static int islandPoints = 1000;
        static Random rnd;

        public static Layer[] Generate()
        {
            rnd = new Random();
            Layer[] layers = new Layer[5]; // 3 layers and 2 "buffer" layers, that will remain empty, but allow the player to go outside of the 1st and 3rd layers

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer();
                layers[i].InitEntityLists();

                if (i == 2)
                    layers[i].InitTileData();
                if (i != 0 && i != layers.Length - 1)
                    layers[i].InitTileData();
            }

            Loop(layers);


            return layers;
        }

        private static void Loop(Layer[] layers)
        {
            // i am purposely making it so the can overlap eachother
            if (islandPoints <= 0)
                return;
            // will generate one island
            int i = rnd.Next(100);

            int sizeXMin = 0, sizeXMax = 0;
            int sizeYMin = 0, sizeYMax = 0;

            // 33% tiny
            if (i < 33)
            {
                islandPoints -= 1;
                sizeXMin = 15;
                sizeXMax = 30;

                sizeYMin = 5;
                sizeYMax = 8;
            }
            // 22% small
            else if (i < 55 && islandPoints >= 3)
            {
                islandPoints -= 3;
                sizeXMin = 30;
                sizeXMax = 60;

                sizeYMin = 8;
                sizeYMax = 14;
            }
            // 10% medium
            else if (i < 65 && islandPoints >= 5)
            {
                islandPoints -= 5;
                sizeXMin = 60;
                sizeXMax = 120;

                sizeYMin = 15;
                sizeYMax = 21;
            }
            //5% large
            else if (i < 70 && islandPoints >= 10)
            {
                islandPoints -= 10;
                sizeXMin = 120;
                sizeXMax = 240;

                sizeYMin = 25;
                sizeYMax = 35;
            }
            //2% giant
            else if (i < 73 && islandPoints >= 20)
            {
                islandPoints -= 20;
                sizeXMin = 240;
                sizeXMax = 480;

                sizeYMin = 40;
                sizeYMax = 80;
            }//other reroll
            else
            {
                Loop(layers);
                return;
            }


            int sizeX = rnd.Next(sizeXMin, sizeXMax);
            int sizeY = rnd.Next(sizeYMin, sizeYMax);

            GenerateIsland(layers, sizeX, sizeY);

            Loop(layers);
        }

        private static void GenerateIsland(Layer[] layers, int sizeX, int sizeY)
        {
            int startX = rnd.Next(0, World.Width - sizeX);
            int endX = startX + sizeX;
            // cant be RIGHT on the roof
            int startY = rnd.Next(30, World.Height - sizeY);
            int endY = startY + sizeY;

            // first we get the starting ground spot
            // will be in the upper half of the island
            int YPosition = startY + rnd.Next((endY - startY) / 2);

            for (int x = startX; x < endX; x++)
            {
                YPosition = (int)MathHelper.Clamp(YPosition, 0, World.Height - 1);

                for (int y = YPosition; y < YPosition + Math.Min(x - startX, endX - x); y++)
                {
                    if (y >= World.Height)
                        break;
                    layers[2].SetTile(x, y, Tile.Dirt);
                }
                





                int i = rnd.Next(100);
                // 30% chance to stay still
                // 20% chance to go up 1, 20% chance to go down 1
                // 10% chance to go up 2, 10% chance to go down 2
                // 5% chance to go up 3, 5% chance to go down 3
                if (i < 30)
                {
                    //nothing
                }
                else if (i < 50)
                {
                    YPosition++;
                }
                else if (i < 70)
                {
                    YPosition--;
                }
                else if (i < 80)
                {
                    YPosition += 2;
                }
                else if (i < 90)
                {
                    YPosition -= 2;
                }
                else if (i < 95)
                {
                    YPosition += 3;
                }
                else if (i < 100)
                {
                    YPosition -= 3;
                }
            }
        }
    }
}
