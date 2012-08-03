using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Cloud9
{
   public static class RectangleExtentions
    {
       public static bool CollidesWithLine(this Rectangle rect, Vector2 linestart, Vector2 lineend)
       {
           float topLeft = LineCornerIntersec(new Vector2(rect.Left, rect.Top), linestart, lineend);
           float topRight = LineCornerIntersec(new Vector2(rect.Right, rect.Top), linestart, lineend);
           float bottomLeft = LineCornerIntersec(new Vector2(rect.Left, rect.Bottom), linestart, lineend);
           float bottomRight = LineCornerIntersec(new Vector2(rect.Right, rect.Bottom), linestart, lineend);

           if (topLeft < 0 && topRight < 0 && bottomLeft < 0 && bottomRight < 0)
               return false;
           if (topLeft >= 0 && topRight >= 0 && bottomLeft >= 0 && bottomRight >= 0)
               return false;


           if (linestart.X > rect.Right && lineend.X > rect.Right)
               return false;

           if (linestart.X < rect.Left && lineend.X < rect.Left)
               return false;

           if (linestart.Y < rect.Top && lineend.Y < rect.Top)
               return false;

           if (linestart.Y > rect.Bottom && lineend.Y > rect.Bottom)
               return false;

           return true;
       }
       static float LineCornerIntersec(Vector2 corner, Vector2 linestart, Vector2 lineend)
       {
           return corner.X * (lineend.Y - linestart.Y) + corner.Y * (linestart.X - lineend.X) + (lineend.X * linestart.Y - linestart.X * lineend.Y);
       }
    }
}
