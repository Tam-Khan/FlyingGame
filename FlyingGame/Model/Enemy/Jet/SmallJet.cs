using System.Drawing;
using FlyingGame.Model.Shared;

namespace FlyingGame.Model.Enemy.Jet
{
    public class SmallJet:GameController
    {
        public int RefX { get; set; }
        public int RefY { get; set; }
        public sbyte MovementState { get; set; }
        public sbyte Direction { get; set; }
        public bool MakeRedundant { get; set; }
        public byte JetType { get; set; }
        public byte Hp { get; set; }
        public byte MovementDelta { get; set; }

        public byte Width { get; set; }
        public byte Height { get; set; }

        public int X2
        { get { return RefX + Width; } } //Approximate length of jet

        public int Y2
        { get { return RefY + Height; } } //Approximate height of jet (wing to wing)


        public SmallJet()
        {
            RefX = 0;
            RefY = 0;
            Width = 40;
            Height = 25;
            MovementState = 0;
            Direction = 4;
            MakeRedundant = false;
            JetType = 1;
        }
        
        //Process polygon drawing points for various Enemy Jet parts.
        public Point[] FuselagePosOne()
        {
            var points = new Point[6];
            points[0] = new Point(RefX + 5 , RefY + 10);
            points[1] = new Point(RefX + 0, RefY + 12);
            points[2] = new Point(RefX + 5, RefY + 14);
            points[3] = new Point(RefX + 35, RefY + 14);
            points[4] = new Point(RefX + 40, RefY + 12);
            points[5] = new Point(RefX + 35, RefY + 10);
            return points;
        }
        
        public Point[] WingBottomPosOne()
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 9, RefY + 15);
            points[1] = new Point(RefX + 35, RefY + 15);
            points[2] = new Point(RefX + 35, RefY + 15 + 10);
            points[3] = new Point(RefX + 25, RefY + 15 + 10);
            return points;
        }

        public Point[] WingTopPosOne()
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 9, RefY + 9);
            points[1] = new Point(RefX + 35, RefY + 9);
            points[2] = new Point(RefX + 35, RefY + 9 - 10);
            points[3] = new Point(RefX + 25, RefY + 9 - 10);
            return points;
        }
       
    }
}
