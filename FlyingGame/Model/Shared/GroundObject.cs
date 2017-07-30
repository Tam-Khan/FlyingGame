using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FlyingGame.Model.Shared
{
    public class GroundObject
    {
        public bool MountainDrawInitiated { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Brush MountainColor { get; set; }
        public int RefX { get; set; }
        public int RefY { get; set; }
        public byte DeltaX { get; set; }
        public Point[] ProcessedPoints { get; set; }
        
        //Process Polygon points for cloud shape drawing
        public Point[] MountainPoints()
        {
            var points = new Point[8];

            if (ProcessedPoints == null)
            {
                int xDiv = Width/7, yDiv = Height/3;
                var rand = new Random();
     
                points[0] = new Point(RefX,RefY);
                points[1] = new Point(rand.Next(RefX + xDiv * 0 + 2, RefX + xDiv * 1), rand.Next(RefY - yDiv * 1,RefY - 2));
                points[2] = new Point(rand.Next(RefX + xDiv * 1 + 2, RefX + xDiv * 2), rand.Next(RefY - yDiv * 2,RefY - yDiv * 1 - 2));
                points[3] = new Point(rand.Next(RefX + xDiv * 2 + 2, RefX + xDiv * 3), rand.Next(RefY - Height,RefY - yDiv * 2 - 2));
                points[4] = new Point(rand.Next(RefX + xDiv * 3 + 2, RefX + xDiv * 4), rand.Next(RefY - Height, RefY - yDiv * 2 - 2));
                points[5] = new Point(rand.Next(RefX + xDiv * 4 + 2, RefX + xDiv * 5), rand.Next(RefY - yDiv * 2, RefY - yDiv * 1 -2));
                points[6] = new Point(rand.Next(RefX + xDiv * 5 + 2, RefX + xDiv * 6), rand.Next(RefY - yDiv * 1, RefY - 2));
                points[7] = new Point(RefX + Width,RefY);

                ProcessedPoints = points;

                //points[0] = new Point(RefX - DeltaX, RefY);
                //points[1] = new Point(RefX + (int)(Width * 0.45) - DeltaX, RefY - Height);
                //points[2] = new Point(RefX + (int)(Width * 0.55) - DeltaX, RefY - Height);
                //points[3] = new Point(RefX + Width - DeltaX, RefY);
            }
            else
            {
                for(int i=0; i<ProcessedPoints.Count();i++)
                {
                    points[i] = new Point(ProcessedPoints[i].X-DeltaX,ProcessedPoints[i].Y);
                }

                ProcessedPoints = points;
            }
            return points;
        }
    }
}
