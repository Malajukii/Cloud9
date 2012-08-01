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
    class Animation
    {
        public int rows;
        public int columns;
        public float fps;
        public Vector2 startCoords;
        public Rectangle frameSize;

        public Animation Copy()
        {
            Animation ani = new Animation();

            ani.rows = rows;
            ani.columns = columns;
            ani.fps = fps;
            ani.startCoords = startCoords;
            ani.frameSize = frameSize;

            return ani;
        }
    }
}
