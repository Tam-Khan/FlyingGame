using System.Drawing;
using FlyingGame.Model.Shared;

namespace FlyingGame.Model.EnemyJets
{
    public class EnemyBoss:GameController
    {
        public bool IsBossInitiated { get; set; }
        
        public int RefX { get; set; }       
        public int RefY { get; set; }
        
        public int OriginalHp { get; set; }
        public int CurrHp { get; set; }
        public byte CurrHitLevel { get; set; }
        
        public byte FireLvlMiniGun { get; set; }
        public byte FireLvlBigGun { get; set; }
        
        public byte Direction { get; set; }
        public byte PrevDirection { get; set; }
        
        public bool ToggleBurner { get; set; }

        public int MiniGun1X { get { return RefX - 3; } }
        public int MiniGun1Y { get { return RefY + 27; } }
        
        public int MiniGun2X { get { return RefX - 3; } }
        public int MiniGun2Y { get { return RefY + 40; } }
        
        public int BigGunX { get { return RefX - 3; } }
        public int BigGunY { get { return RefY + 33; } }
        
        public int X2{get { return RefX + 130; }}
        public int HitY1 {get { return RefY + 30; }}
        public int HitY2 { get { return HitY1 + 25; } }
        public byte Width { get; set; }
        public byte Delta { get; set; }

        public int HitPerLevel{get { return OriginalHp/3; }}

        public int BossDestroyedFireWorkCounter { get; set; }

        public EnemyBoss()
        {
            RefX = 0;
            RefY = 0;
            IsBossInitiated = false;
            OriginalHp = BossHp;
            CurrHp = BossHp;
            CurrHitLevel = 1;
            FireLvlBigGun = 1;
            FireLvlMiniGun = 1;
            Direction = 0;
            ToggleBurner = false;
            BossDestroyedFireWorkCounter = 0;
            Width = 55;
            Delta = 2;
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
            points[4] = new Point(RefX + 115, RefY + 20);
            points[5] = new Point(RefX + 117, RefY + 21);
            points[6] = new Point(RefX + 117, RefY + 43);
            points[7] = new Point(RefX + 115, RefY + 44);
            points[8] = new Point(RefX + 14, RefY + 43);
            points[9] = new Point(RefX + 0, RefY + 36);
            
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

            points[0] = new Point(RefX + 8, RefY + 39);
            points[1] = new Point(RefX + -3, RefY + 39);
            points[2] = new Point(RefX + -3, RefY + 41);
            points[3] = new Point(RefX + 8, RefY + 41);

            return points;
        }

        public Point[] BigGun()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 0, RefY + 31);
            points[1] = new Point(RefX - 3, RefY + 31);
            points[2] = new Point(RefX - 3, RefY + 35);
            points[3] = new Point(RefX + 0, RefY + 35);

            return points;
        }

        public Point[] WingMiddle()
        {
            var points = new Point[8];

            points[0] = new Point(RefX + 37, RefY + 32);
            points[1] = new Point(RefX + 38, RefY + 31);
            points[2] = new Point(RefX + 109, RefY + 31);
            points[3] = new Point(RefX + 110, RefY + 32);
            points[4] = new Point(RefX + 110, RefY + 34);
            points[5] = new Point(RefX + 109, RefY + 35);
            points[6] = new Point(RefX + 38, RefY + 35);
            points[7] = new Point(RefX + 37, RefY + 34);

            return points;
        }

        public Point[] FuselageBottom()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 65, RefY + 45);
            points[1] = new Point(RefX + 76, RefY + 50);
            points[2] = new Point(RefX + 106, RefY + 50);
            points[3] = new Point(RefX + 110, RefY + 45);
            return points;
        }

        public Point[] FuselageTop()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 40, RefY + 20);
            points[1] = new Point(RefX + 43, RefY + 17);
            points[2] = new Point(RefX + 56, RefY + 17);
            points[3] = new Point(RefX + 63, RefY + 13);
            points[4] = new Point(RefX + 111, RefY + 13);
            points[5] = new Point(RefX + 116, RefY + 20);

            return points;
        }

        public Point[] TopFin()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 81, RefY + 13);
            points[1] = new Point(RefX + 91, RefY + 0);
            points[2] = new Point(RefX + 103, RefY + 0);
            points[3] = new Point(RefX + 108, RefY + 13);

            return points;
        }

        public Point[] BackExhaust()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 118, RefY + 22);
            points[1] = new Point(RefX + 124, RefY + 26);
            points[2] = new Point(RefX + 124, RefY + 39);
            points[3] = new Point(RefX + 118, RefY + 43);
           
            return points;
        }

        public Point[] BackBurner()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 125, RefY + 27);
            points[1] = new Point(RefX + 128, RefY + 27);
            points[2] = new Point(RefX + 131, RefY + 30);
            points[3] = new Point(RefX + 131, RefY + 35);
            points[4] = new Point(RefX + 128, RefY + 38);
            points[5] = new Point(RefX + 125, RefY + 38);

            return points;
        }
        #endregion
    }
}
