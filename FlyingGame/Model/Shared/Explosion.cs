using System.Drawing;

namespace FlyingGame.Model.Shared
{
    public class Explosion
    {
        public byte VisibilityCounter { get; set; }
        public Point[] Points { get; set; }
        public Pen Colour { get; set; }
        
        //Polygon drawing point for explosion (like a 'X' shape)
        public static Point[] Explode(int refX, int refY)
        {
            var points = new Point[12];
            points[0] = new Point(refX + 0, refY + 3);
            points[1] = new Point(refX + 3, refY + 0);
            points[2] = new Point(refX + 9, refY + 6);
            points[3] = new Point(refX + 15, refY + 0);
            points[4] = new Point(refX + 18, refY + 3);
            points[5] = new Point(refX + 12, refY + 9);
            points[6] = new Point(refX + 18, refY + 15);
            points[7] = new Point(refX + 15, refY + 18);
            points[8] = new Point(refX + 9, refY + 12);
            points[9] = new Point(refX + 3, refY + 18);
            points[10] = new Point(refX + 0, refY + 15);
            points[11] = new Point(refX + 6, refY + 9);

            return points;
        }
    }
}
