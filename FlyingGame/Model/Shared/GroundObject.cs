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
        
        //Process Polygon points for cloud shape drawing
        public Point[] MountainPoints()
        {
            var points = new Point[4];
            points[0] = new Point(RefX - DeltaX, RefY);
            points[1] = new Point(RefX + (int)(Width * 0.45) - DeltaX, RefY-Height);
            points[2] = new Point(RefX + (int)(Width * 0.55) - DeltaX, RefY - Height);
            points[3] = new Point(RefX + Width-DeltaX, RefY);
            return points;
        }
        
    }
}
