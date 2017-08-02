using System.Drawing;

namespace FlyingGame.Model.Enemy.Boss
{
    public class PlaneBoss:Boss
    {
        public int MiniGun1X { get { return RefX - 3; } }
        public int MiniGun1Y { get { return RefY + 27; } }
        
        public int MiniGun2X { get { return RefX - 3; } }
        public int MiniGun2Y { get { return RefY + 40 + BossSizeDeltaY; } }
        
        public int BigGunX { get { return RefX + 33; } }
        public int BigGunY { get { return RefY + 33 + BossSizeDeltaY; } }
        
        public int X2{get { return RefX + Width; }}
        public int HitY1 {get { return RefY + 30; }}
        public int HitY2 { get { return HitY1 + 25 + BossSizeDeltaY; } }
        
        public int Height { get; set; }
        public int Width { get; set; }

        public PlaneBoss()
        {
            RefX = 0;
            RefY = 0;
            IsBossInitiated = false;
            CurrHitLevel = 1;
            FireLvlBigGun = 1;
            FireLvlMiniGun = 1;
            Direction = 0;
            ToggleBurner = false;
            BossDestroyedFireWorkCounter = 0;
            Height = 55 + BossSizeDeltaY;
            Width = 130 + BossSizeDeltaX;
            MovementDelta = 2;
        }

        public void ResetBasics()
        {
            RefX = 0;
            RefY = 0;
            IsBossInitiated = false;
            CurrHitLevel = 1;
            FireLvlBigGun = 1;
            FireLvlMiniGun = 1;
            Direction = 0;
            ToggleBurner = false;
            BossDestroyedFireWorkCounter = 0;
            Height = 55 + BossSizeDeltaY;
            Width = 130 + BossSizeDeltaX;
            MovementDelta = 2;
        }

        //Process polygon drawing points for various parts of boss

        #region Level1 Boss

        public Point[] FuselageMain()
        {
            var points = new Point[10];
            
            points[0] = new Point(RefX + 0, RefY + 30);
            points[1] = new Point(RefX + 7, RefY + 24);
            points[2] = new Point(RefX + 12, RefY + 24);
            points[3] = new Point(RefX + 16, RefY + 20);
            points[4] = new Point(RefX + 115 + BossSizeDeltaX, RefY + 20);
            points[5] = new Point(RefX + 117 + BossSizeDeltaX, RefY + 21);
            points[6] = new Point(RefX + 117 + BossSizeDeltaX, RefY + 43 + BossSizeDeltaY);
            points[7] = new Point(RefX + 115 + BossSizeDeltaX, RefY + 44 + BossSizeDeltaY);
            points[8] = new Point(RefX + 14, RefY + 43 + BossSizeDeltaY);
            points[9] = new Point(RefX + 0, RefY + 36 + BossSizeDeltaY);
            
            return points;
        }

        public Point[] MiniGunTop()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 5, RefY + 26);
            points[1] = new Point(RefX + -3, RefY + 26);
            points[2] = new Point(RefX + -3, RefY + 28);
            points[3] = new Point(RefX + 4, RefY + 28);

            return points;
        }

        public Point[] MiniGunBottom()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 8, RefY + 39 + BossSizeDeltaY);
            points[1] = new Point(RefX + -3, RefY + 39 + BossSizeDeltaY);
            points[2] = new Point(RefX + -3, RefY + 41 + BossSizeDeltaY);
            points[3] = new Point(RefX + 8, RefY + 41 + BossSizeDeltaY);

            return points;
        }
        
        public Point[] MiddleWingType()
        {
            var points = new Point[8];

            points[0] = new Point(RefX + 37, RefY + 32);
            points[1] = new Point(RefX + 38, RefY + 31);
            points[2] = new Point(RefX + 109 + BossSizeDeltaX, RefY + 31);
            points[3] = new Point(RefX + 110 + BossSizeDeltaX, RefY + 32);
            points[4] = new Point(RefX + 110 + BossSizeDeltaX, RefY + 34 + BossSizeDeltaY);
            points[5] = new Point(RefX + 109 + BossSizeDeltaX, RefY + 35 + BossSizeDeltaY);
            points[6] = new Point(RefX + 38, RefY + 35 + BossSizeDeltaY);
            points[7] = new Point(RefX + 37, RefY + 34 + BossSizeDeltaY);

            return points;
        }

        public Point[] BigGun()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 36, RefY + 31);
            points[1] = new Point(RefX + 31, RefY + 31);
            points[2] = new Point(RefX + 30, RefY + 32);
            points[3] = new Point(RefX + 30, RefY + 34 + BossSizeDeltaY);
            points[4] = new Point(RefX + 31, RefY + 35 + BossSizeDeltaY);
            points[5] = new Point(RefX + 36, RefY + 35 + BossSizeDeltaY);

            return points;
        }

        public Point[] FuselageBottom()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 65, RefY + 45 + BossSizeDeltaY);
            points[1] = new Point(RefX + 76, RefY + 50 + BossSizeDeltaY);
            points[2] = new Point(RefX + 106 + BossSizeDeltaX, RefY + 50 + BossSizeDeltaY);
            points[3] = new Point(RefX + 110 + BossSizeDeltaX, RefY + 45 + BossSizeDeltaY);
            return points;
        }

        public Point[] FuselageTop()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 40, RefY + 20);
            points[1] = new Point(RefX + 43, RefY + 17);
            points[2] = new Point(RefX + 56, RefY + 17);
            points[3] = new Point(RefX + 63, RefY + 13);
            points[4] = new Point(RefX + 111 + BossSizeDeltaX, RefY + 13);
            points[5] = new Point(RefX + 116 + BossSizeDeltaX, RefY + 20);

            return points;
        }

        public Point[] TopFin()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 81 + BossSizeDeltaX/2 , RefY + 13);
            points[1] = new Point(RefX + 91 + BossSizeDeltaX/2 , RefY + 0);
            points[2] = new Point(RefX + 103 + BossSizeDeltaX, RefY + 0);
            points[3] = new Point(RefX + 108 + BossSizeDeltaX, RefY + 13);

            return points;
        }

        public Point[] BackExhaust()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 118 + BossSizeDeltaX, RefY + 22);
            points[1] = new Point(RefX + 124 + BossSizeDeltaX, RefY + 26);
            points[2] = new Point(RefX + 124 + BossSizeDeltaX, RefY + 39 + BossSizeDeltaY);
            points[3] = new Point(RefX + 118 + BossSizeDeltaX, RefY + 43 + BossSizeDeltaY);
           
            return points;
        }

        public Point[] BackBurner()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 125 + BossSizeDeltaX, RefY + 27);
            points[1] = new Point(RefX + 128 + BossSizeDeltaX, RefY + 27);
            points[2] = new Point(RefX + 131 + BossSizeDeltaX, RefY + 30);
            points[3] = new Point(RefX + 131 + BossSizeDeltaX, RefY + 35 + BossSizeDeltaY);
            points[4] = new Point(RefX + 128 + BossSizeDeltaX, RefY + 38 + BossSizeDeltaY);
            points[5] = new Point(RefX + 125 + BossSizeDeltaX, RefY + 38 + BossSizeDeltaY);

            return points;
        }
        #endregion
    }
}
