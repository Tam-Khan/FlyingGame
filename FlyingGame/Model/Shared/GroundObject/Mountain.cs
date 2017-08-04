using System;
using System.Drawing;
using System.Linq;

namespace FlyingGame.Model.Shared.GroundObject
{
    public class Mountain: GroundObject
    {
        public Mountain()
        {
            MoveDeltaX = 5;
        }

        //Process Polygon points for cloud shape drawing
        public Point[] MountainPoints()
        {
            var points = new Point[8];

            if (ProcessedPoints == null)
            {
                int xDiv = Width/7, yDiv = Height/3;
                const int seaClearance = -10;
                var rand = new Random();

                points[0] = new Point(RefX, RefY + seaClearance);
                points[1] = new Point(rand.Next(RefX + xDiv * 0 + 2, RefX + xDiv * 1), rand.Next(RefY + seaClearance - yDiv * 1, RefY + seaClearance - 10));
                points[2] = new Point(rand.Next(RefX + xDiv * 1 + 2, RefX + xDiv * 2), rand.Next(RefY + seaClearance - yDiv * 2, RefY + seaClearance - yDiv * 1 - 2));
                points[3] = new Point(rand.Next(RefX + xDiv * 2 + 2, RefX + xDiv * 3), rand.Next(RefY + seaClearance - Height, RefY + seaClearance - yDiv * 2 - 2));
                points[4] = new Point(rand.Next(RefX + xDiv * 3 + 2, RefX + xDiv * 4), rand.Next(RefY + seaClearance - Height, RefY + seaClearance - yDiv * 2 - 2));
                points[5] = new Point(rand.Next(RefX + xDiv * 4 + 2, RefX + xDiv * 5), rand.Next(RefY + seaClearance - yDiv * 2, RefY + seaClearance - yDiv * 1 - 2));
                points[6] = new Point(rand.Next(RefX + xDiv * 5 + 2, RefX + xDiv * 6), rand.Next(RefY + seaClearance - yDiv * 1, RefY + seaClearance - 10));
                points[7] = new Point(RefX + Width, RefY + seaClearance);

                ProcessedPoints = points;
            }
            else
            {
                for(int i=0; i<ProcessedPoints.Count();i++)
                {
                    points[i] = new Point(ProcessedPoints[i].X-MoveDeltaX,ProcessedPoints[i].Y);
                }

                ProcessedPoints = points;
            }
            return points;
        }
    }
}
