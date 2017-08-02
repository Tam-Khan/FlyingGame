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
        
        public Point[] JetBurner()
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 4, RefY + 4 + 3);
            points[1] = new Point(RefX + 0, RefY + 6 + 3);
            points[2] = new Point(RefX + 0, RefY + 9 + 3);
            points[3] = new Point(RefX + 4, RefY + 11 + 3);
            return points;
        }

        public Point[] Fuselage()       //When stationery
        {
            var points = new Point[8];
            points[0] = new Point(RefX + 5, RefY + 5+3);
            points[1] = new Point(RefX + 35, RefY + 5+3);
            points[2] = new Point(RefX + 37, RefY + 7+3);
            points[3] = new Point(RefX + 37, RefY + 8+3);
            points[4] = new Point(RefX + 35, RefY + 10+3);
            points[5] = new Point(RefX + 5, RefY + 10+3);
            points[6] = new Point(RefX + 0, RefY + 8+3);
            points[7] = new Point(RefX + 0, RefY + 7+3);
            return points;
        }

        public Point[] TailPosOne()           //When stationery
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 5, RefY + 5+3);
            points[1] = new Point(RefX + 9+6, RefY + 5+3);
            points[2] = new Point(RefX + 7+2, RefY + 1+1);
            points[3] = new Point(RefX + 4, RefY + 1+1);
            
            return points;
        }

        public Point[] WingPosOne()           //When stationery
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 11, RefY + 7+3);
            points[1] = new Point(RefX + 11, RefY + 8+3);
            points[2] = new Point(RefX + 25, RefY + 8+3);
            points[3] = new Point(RefX + 25, RefY + 7+3);
            return points;
        }

        public Point[] TailPosTwo()           //When climbing up
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 5, RefY + 5+3);
            points[1] = new Point(RefX + 9+6, RefY + 5+3);
            points[2] = new Point(RefX + 8+2, RefY + 0+1);
            points[3] = new Point(RefX + 5, RefY + 0+1);

            return points;
        }

        public Point[] LefWingPosTwo()        //When climbing up
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 11, RefY + 7+3);
            points[1] = new Point(RefX + 9, RefY + 14+3);
            points[2] = new Point(RefX + 11, RefY + 14+3);
            points[3] = new Point(RefX + 25, RefY + 7+3);
            return points;
        }

        public Point[] RightWingPosTwo()      //When climbing up
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 13, RefY + 4 + 3);
            points[1] = new Point(RefX + 19, RefY + 4 + 3);
            points[2] = new Point(RefX + 13, RefY + 2 + 3);
            points[3] = new Point(RefX + 11, RefY + 2 + 3);
            return points;
        }

        public Point[] TailPosThree()         //When climbing down
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 5, RefY + 5 + 3);
            points[1] = new Point(RefX + 9 + 6, RefY + 5 + 3);
            points[2] = new Point(RefX + 6+2, RefY + 2 + 1);
            points[3] = new Point(RefX + 3, RefY + 2 + 1);

            return points;
        }

        public Point[] LefWingPosThree()      //When climbing down
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 11, RefY + 8+3);
            points[1] = new Point(RefX + 9, RefY + 1+3);
            points[2] = new Point(RefX + 11, RefY + 1+3);
            points[3] = new Point(RefX + 25, RefY + 8+3);
            return points;
        }

        public Point[] RightWingPosThree()    //When climbing down
        {
            var points = new Point[4];
            points[0] = new Point(RefX + 13, RefY + 11+3);
            points[1] = new Point(RefX + 19, RefY + 11+3);
            points[2] = new Point(RefX + 13, RefY + 13+3);
            points[3] = new Point(RefX + 11, RefY + 13+3);
            return points;
        }
    }
}
