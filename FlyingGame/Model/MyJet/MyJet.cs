using System.Drawing;

namespace FlyingGame.Model.MyJet
{
    public class MyJet
    {
        public int RefX { get; set; }
        public int RefY { get; set; }
        public sbyte Hp { get; set; }

        public byte CurrentBulletLimit { get; set; }
        public byte CurrentActiveGun { get; set; }
        
        public sbyte MovementState { get; set; }
        public byte JetBurnerController { get; set; }


        public MyJet()
        {
            RefX = 0;
            RefY = 0;
            Hp = 5;
            CurrentBulletLimit = 2;
            CurrentActiveGun = 1;
            MovementState = 0;
            JetBurnerController = 0;
        }

        public int X2
        {get { return RefX + 35; }} //Approximate width of jet

        public int Y2
        { get { return RefY + 20; } } //Approximate height of jet

        //Polygon drawing points for various parts of jet in different movement state
        
        public Point[] JetBurner(int refX, int refY)
        {
            var points = new Point[4];
            points[0] = new Point(refX + 4, refY + 4 + 3);
            points[1] = new Point(refX + 0, refY + 6 + 3);
            points[2] = new Point(refX + 0, refY + 9 + 3);
            points[3] = new Point(refX + 4, refY + 11 + 3);
            return points;
        }

        public Point[] FuselagePosOne(int refX, int refY)       //When stationery
        {
            var points = new Point[8];
            points[0] = new Point(refX + 5, refY + 5+3);
            points[1] = new Point(refX + 35, refY + 5+3);
            points[2] = new Point(refX + 37, refY + 7+3);
            points[3] = new Point(refX + 37, refY + 8+3);
            points[4] = new Point(refX + 35, refY + 10+3);
            points[5] = new Point(refX + 5, refY + 10+3);
            points[6] = new Point(refX + 0, refY + 8+3);
            points[7] = new Point(refX + 0, refY + 7+3);
            return points;
        }

        public Point[] TailPosOne(int refX, int refY)           //When stationery
        {
            var points = new Point[4];
            points[0] = new Point(refX + 5, refY + 5+3);
            points[1] = new Point(refX + 9+6, refY + 5+3);
            points[2] = new Point(refX + 7+2, refY + 1+1);
            points[3] = new Point(refX + 4, refY + 1+1);
            
            return points;
        }

        public Point[] WingPosOne(int refX, int refY)           //When stationery
        {
            var points = new Point[4];
            points[0] = new Point(refX + 11, refY + 7+3);
            points[1] = new Point(refX + 11, refY + 8+3);
            points[2] = new Point(refX + 25, refY + 8+3);
            points[3] = new Point(refX + 25, refY + 7+3);
            return points;
        }

        public Point[] TailPosTwo(int refX, int refY)           //When climbing up
        {
            var points = new Point[4];
            points[0] = new Point(refX + 5, refY + 5+3);
            points[1] = new Point(refX + 9+6, refY + 5+3);
            points[2] = new Point(refX + 8+2, refY + 0+1);
            points[3] = new Point(refX + 5, refY + 0+1);

            return points;
        }

        public Point[] LefWingPosTwo(int refX, int refY)        //When climbing up
        {
            var points = new Point[4];
            points[0] = new Point(refX + 11, refY + 7+3);
            points[1] = new Point(refX + 9, refY + 14+3);
            points[2] = new Point(refX + 11, refY + 14+3);
            points[3] = new Point(refX + 25, refY + 7+3);
            return points;
        }

        public Point[] RightWingPosTwo(int refX, int refY)      //When climbing up
        {
            var points = new Point[4];
            points[0] = new Point(refX + 13, refY + 4 + 3);
            points[1] = new Point(refX + 19, refY + 4 + 3);
            points[2] = new Point(refX + 13, refY + 2 + 3);
            points[3] = new Point(refX + 11, refY + 2 + 3);
            return points;
        }

        public Point[] TailPosThree(int refX, int refY)         //When climbing down
        {
            var points = new Point[4];
            points[0] = new Point(refX + 5, refY + 5 + 3);
            points[1] = new Point(refX + 9 + 6, refY + 5 + 3);
            points[2] = new Point(refX + 6+2, refY + 2 + 1);
            points[3] = new Point(refX + 3, refY + 2 + 1);

            return points;
        }

        public Point[] LefWingPosThree(int refX, int refY)      //When climbing down
        {
            var points = new Point[4];
            points[0] = new Point(refX + 11, refY + 8+3);
            points[1] = new Point(refX + 9, refY + 1+3);
            points[2] = new Point(refX + 11, refY + 1+3);
            points[3] = new Point(refX + 25, refY + 8+3);
            return points;
        }

        public Point[] RightWingPosThree(int refX, int refY)    //When climbing down
        {
            var points = new Point[4];
            points[0] = new Point(refX + 13, refY + 11+3);
            points[1] = new Point(refX + 19, refY + 11+3);
            points[2] = new Point(refX + 13, refY + 13+3);
            points[3] = new Point(refX + 11, refY + 13+3);
            return points;
        }
    }
}
