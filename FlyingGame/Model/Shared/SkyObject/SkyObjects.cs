using System;
using System.Drawing;

namespace FlyingGame.Model.Shared.SkyObject
{
    public class SkyObjects
    {
        public int RefX { get; set; }
        public int RefY { get; set; }
        public byte Width { get; set; }
        public byte Height { get; set; }
        public Brush FillColor { get; set; }
        public byte DeltaX { get; set; }
        public bool HasBorder { get; set; }
       
        public SkyObjects()
        {
            RefX = 0;
            RefY = 0;
            Width = 0;
            Height = 0;
            DeltaX = 1;
        }

        //Process Polygon points for cloud shape drawing
        public Point[] CloudPoints()
        {
            byte xDiv = (byte)(Width / 3);
            byte yDiv = (byte)(Height / 3);
            var rand = new Random();
            
            byte dY1 = (byte) rand.Next(0, yDiv);
            byte dY2 = (byte) rand.Next(yDiv * 2, Height);
            byte dY3 = (byte) rand.Next(yDiv * 2,Height);
            byte dY4 = (byte) rand.Next(0, yDiv);
            
            byte dX1 = (byte) rand.Next(0, xDiv);
            byte dX2 = (byte) rand.Next(0, xDiv);
            byte dX3 = (byte) rand.Next(xDiv*2, Width);
            byte dX4 = (byte) rand.Next(xDiv * 2, Width);
            
            /*
             base cloud shape:
             *                   (RefX,RefY)   dy1 |xDiv|dy4
             *                                     |    |
             *                             dx1 ----      -----dx4
             *                           yDiv |               | yDiv         
             *                             dx2 ----      -----dx3
             *                                     |    |
             *                                  dy2|xDiv|dy3                          
             */

            var points = new Point[12];
            points[0] = new Point(RefX + 0, RefY + yDiv);
            points[1] = new Point(RefX + dX1, RefY + dY1);
            points[2] = new Point(RefX + xDiv, RefY + 0);
            points[3] = new Point(RefX + xDiv + xDiv, RefY + 0);
            points[4] = new Point(RefX + dX4, RefY + dY4);
            points[5] = new Point(RefX + Width, RefY + yDiv);
            points[6] = new Point(RefX + Width, RefY + yDiv + yDiv);
            points[7] = new Point(RefX + dX3, RefY + dY3);
            points[8] = new Point(RefX + xDiv + xDiv, RefY + Height);
            points[9] = new Point(RefX + xDiv, RefY + Height);
            points[10] = new Point(RefX + dX2, RefY + dY2);
            points[11] = new Point(RefX + 0, RefY + yDiv + yDiv);

            return points;
        }
    }
}
