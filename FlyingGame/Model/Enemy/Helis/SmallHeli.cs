using System.Drawing;
using System.Windows.Forms;
using FlyingGame.Model.Shared;

namespace FlyingGame.Model.Enemy.Helis
{
    public class SmallHeli:GameController
    {
        public int RefX { get; set; }
        public int RefY { get; set; }
        
        public byte Width { get; set; }
        public byte Height { get; set; }

        public int X2 { get { return RefX + Width; } }
        public int Y2 { get { return RefY + Height; } }    
        
        
        public bool RotorAtFront { get; set; }
        public sbyte Direction { get; set; }
        public bool MakeRedundant { get; set; }
        public byte MovmementDelta { get; set; }
        public byte Hp { get; set; }
        public byte StallMovementCounter { get; set; }
        public bool InitiateMove { get; set; }

        public SmallHeli()
        {
            RefX = 0;
            RefY = 0;
            Width = 70;
            Height = 24;
            RotorAtFront = true;
            Direction = 4;
            MakeRedundant = false;
            StallMovementCounter = 0;
            InitiateMove = false;
        }
        
        //Process polygon drawing points for various Enemy Jet parts.
        public Point[] FuselageMain()
        {
            var points = new Point[8];
            points[0] = new Point(RefX + 5 , RefY + 11);
            points[1] = new Point(RefX + 14, RefY + 11);
            points[2] = new Point(RefX + 22, RefY + 3);
            points[3] = new Point(RefX + 39, RefY + 3);
            points[4] = new Point(RefX + 45, RefY + 7);
            points[5] = new Point(RefX + 36, RefY + 16);
            points[6] = new Point(RefX + 8, RefY + 16);
            points[7] = new Point(RefX + 5, RefY + 13);

            return points;
        }
        
        public Point[] FuselageWithTail()
        {
            var points = new Point[13];
            points[0] = new Point(RefX + 46, RefY + 7);
            points[1] = new Point(RefX + 64, RefY + 7);
            points[2] = new Point(RefX + 68, RefY + 3);
            points[3] = new Point(RefX + 70, RefY + 3);
            points[4] = new Point(RefX + 70, RefY + 14);
            points[5] = new Point(RefX + 67, RefY + 14);
            points[6] = new Point(RefX + 63, RefY + 10);
            points[7] = new Point(RefX + 42, RefY + 15);
            points[8] = new Point(RefX + 37, RefY + 20);
            points[9] = new Point(RefX + 17, RefY + 20);
            points[10] = new Point(RefX + 14, RefY + 17);
            points[11] = new Point(RefX + 36, RefY + 17);
            points[12] = new Point(RefX + 45, RefY + 8);
            
            return points;
        }

        public Point[] Cockpit()
        {
            var points = new Point[5];
            points[0] = new Point(RefX + 6, RefY + 10);
            points[1] = new Point(RefX + 14, RefY + 10);
            points[2] = new Point(RefX + 17, RefY + 7);
            points[3] = new Point(RefX + 11, RefY + 7);
            points[4] = new Point(RefX + 6, RefY + 9);
            return points;
        }

        public Point[] RotorLeft()
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 0, RefY + 0);
            points[1] = new Point(RefX + 0, RefY + 1);
            points[2] = new Point(RefX + 28, RefY + 1);
            points[3] = new Point(RefX + 28, RefY + 0);
            
            return points;
        }

        public Point[] RotorRight()
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 30, RefY + 0);
            points[1] = new Point(RefX + 30, RefY + 1);
            points[2] = new Point(RefX + 58, RefY + 1);
            points[3] = new Point(RefX + 58, RefY + 0);

            return points;
        }
    }
}
