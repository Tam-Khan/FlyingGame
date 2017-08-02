using System.Drawing;

namespace FlyingGame.Model.Enemy.Boss
{
    public class RocketBoss: Boss
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int X2 { get { return RefX + Width; } }

        public Point DamageAreaCT { get{return new Point(RefX,RefY + 26);} }    //Top left corner
        public Point DamageAreaCB { get { return new Point(RefX + 212 + BossSizeDeltaX,RefY + 63 + BossSizeDeltaY); } }     //Bottom right corner

        public Point DamageAreaTT { get { return new Point(RefX + 70, RefY + 0); } }    //Top left corner
        public Point DamageAreaTB { get { return new Point(RefX + 212 + BossSizeDeltaX, RefY + 25); } }     //Bottom right corner

        public Point DamageAreaBT { get { return new Point(RefX + 70, RefY + 64 ); } }      //Top left corner
        public Point DamageAreaBB { get { return new Point(RefX + 212 + BossSizeDeltaX, RefY + 89 + BossSizeDeltaY);}}  //Bottom left corner
        
        public int MiniGun1X { get { return RefX + 69; } }
        public int MiniGun1Y { get { return RefY + 9-4; } }
        
        public int MiniGun2X { get { return RefX + 69; } }
        public int MiniGun2Y { get { return RefY + 77 + BossSizeDeltaY/2 -4; } }

        public int BigGunXDelta { get; set; }
        public int BigGunMovtCounter { get; set; }

        public int BigGunX { get { return RefX + 74 + BigGunXDelta;}}
        public int BigGunY { get { return RefY + 38 + BossSizeDeltaY/2;}}

        public bool NoseGunInitiated { get; set; }
        public int NoseGunDelayCounter { get; set; }
        public int NoseGunDelayMax { get; set; }
        public byte NoseGunCharge { get; set; }
        
        public RocketBoss()
        {
            RefX = 0;
            RefY = 0;
            Height = 90 + BossSizeDeltaY;
            Width = 255 + BossSizeDeltaX;
            IsBossInitiated = false;
            CurrHitLevel = 1;
            FireLvlBigGun = 1;
            FireLvlMiniGun = 1;
            Direction = 0;
            ToggleBurner = false;
            BossDestroyedFireWorkCounter = 0;
            MovementDelta = 2;
            BigGunXDelta = 0;
            NoseGunDelayCounter = 0;
            NoseGunInitiated = false;
        }

        public void ResetBasics()
        {
            RefX = 0;
            RefY = 0;
            Height = 90 + BossSizeDeltaY;
            Width = 255 + BossSizeDeltaX;
            IsBossInitiated = false;
            CurrHitLevel = 1;
            FireLvlBigGun = 1;
            FireLvlMiniGun = 1;
            Direction = 0;
            ToggleBurner = false;
            BossDestroyedFireWorkCounter = 0;
            MovementDelta = 2;
            BigGunXDelta = 0;
            NoseGunDelayCounter = 0;
            NoseGunInitiated = false;
        }

        //Process polygon drawing points for various parts of boss

        //centre y point 43/44
        //centre x point 102/103
        public Point[] Nose()
        {
            var points = new Point[6];
            
            points[0] = new Point(RefX + 0, RefY + 43);
            points[1] = new Point(RefX + 20, RefY + 38);
            points[2] = new Point(RefX + 31, RefY + 38);
            points[3] = new Point(RefX + 31, RefY + 51 + BossSizeDeltaY);
            points[4] = new Point(RefX + 19, RefY + 51 + BossSizeDeltaY);
            points[5] = new Point(RefX + 0, RefY + 46 + BossSizeDeltaY);
            
            return points;
        }

        public Point[] Head()
        {
            var points = new Point[8];

            points[0] = new Point(RefX + 32, RefY + 37);
            points[1] = new Point(RefX + 38, RefY + 34);
            points[2] = new Point(RefX + 63, RefY + 34);
            points[3] = new Point(RefX + 68, RefY + 30);
            points[4] = new Point(RefX + 68, RefY + 59 + BossSizeDeltaY);
            points[5] = new Point(RefX + 63, RefY + 55 + BossSizeDeltaY);
            points[6] = new Point(RefX + 38, RefY + 55 + BossSizeDeltaY);
            points[7] = new Point(RefX + 32, RefY + 52 + BossSizeDeltaY);
            
            return points;
        }

        public Point[] Cockpit()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 38, RefY + 43);
            points[1] = new Point(RefX + 41, RefY + 40);
            points[2] = new Point(RefX + 60, RefY + 40);
            points[3] = new Point(RefX + 60, RefY + 49 + BossSizeDeltaY);
            points[4] = new Point(RefX + 41, RefY + 49 + BossSizeDeltaY);
            points[5] = new Point(RefX + 38, RefY + 46 + BossSizeDeltaY);

            return points;
        }

        public Point[] MainFuselage()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 69, RefY + 30);
            points[1] = new Point(RefX + 206 + BossSizeDeltaX, RefY + 30);
            points[2] = new Point(RefX + 206 + BossSizeDeltaX, RefY + 59 + BossSizeDeltaY);
            points[3] = new Point(RefX + 69, RefY + 59 + BossSizeDeltaY);
            
            return points;
        }

        public Point[] WingTop()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 71, RefY + 29);
            points[1] = new Point(RefX + 71, RefY + 26);
            points[2] = new Point(RefX + 135 + BossSizeDeltaX, RefY + 0);
            points[3] = new Point(RefX + 192 + BossSizeDeltaX, RefY + 0);
            points[4] = new Point(RefX + 203 + BossSizeDeltaX, RefY + 26);
            points[5] = new Point(RefX + 203 + BossSizeDeltaX, RefY + 29);

            return points;
        }

        public Point[] WingBottom()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 71, RefY + 60 + BossSizeDeltaY);
            points[1] = new Point(RefX + 71, RefY + 63 + BossSizeDeltaY);
            points[2] = new Point(RefX + 135 + BossSizeDeltaX, RefY + 89 + BossSizeDeltaY);
            points[3] = new Point(RefX + 192 + BossSizeDeltaX, RefY + 89 + BossSizeDeltaY);
            points[4] = new Point(RefX + 203 + BossSizeDeltaX, RefY + 63 + BossSizeDeltaY);
            points[5] = new Point(RefX + 203 + BossSizeDeltaX, RefY + 60 + BossSizeDeltaY);

            return points;
        }

        public Point[] BigGunRail()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 71, RefY + 42);
            points[1] = new Point(RefX + 71, RefY + 46 + BossSizeDeltaY);
            points[2] = new Point(RefX + 203 + BossSizeDeltaX, RefY + 46 + BossSizeDeltaY);
            points[3] = new Point(RefX + 203 + BossSizeDeltaX, RefY + 42);
            
            return points;
        }

        public Point[] BigGun()
        {
            var points = new Point[12];

            points[0] = new Point(RefX + 74 + BossSizeDeltaX + BigGunXDelta, RefY + 40);
            points[1] = new Point(RefX + 76 + BossSizeDeltaX + BigGunXDelta, RefY + 38);
            points[2] = new Point(RefX + 82 + BossSizeDeltaX + BigGunXDelta, RefY + 38);
            points[3] = new Point(RefX + 85 + BossSizeDeltaX + BigGunXDelta, RefY + 35);
            points[4] = new Point(RefX + 106 + BossSizeDeltaX + BigGunXDelta, RefY + 35);
            points[5] = new Point(RefX + 109 + BossSizeDeltaX + BigGunXDelta, RefY + 38);
            points[6] = new Point(RefX + 109 + BossSizeDeltaX + BigGunXDelta, RefY + 50 + BossSizeDeltaY);
            points[7] = new Point(RefX + 106 + BossSizeDeltaX + BigGunXDelta, RefY + 53 + BossSizeDeltaY);
            points[8] = new Point(RefX + 85 + BossSizeDeltaX + BigGunXDelta, RefY + 53 + BossSizeDeltaY);
            points[9] = new Point(RefX + 82 + BossSizeDeltaX + BigGunXDelta, RefY + 50 + BossSizeDeltaY);
            points[10] = new Point(RefX + 76 + BossSizeDeltaX + BigGunXDelta, RefY + 50 + BossSizeDeltaY);
            points[11] = new Point(RefX + 74 + BossSizeDeltaX + BigGunXDelta, RefY + 48 + BossSizeDeltaY);
            
            return points;
        }

        public Point[] BackExhaust()
        {
            var points = new Point[4];

            points[0] = new Point(RefX + 207 + BossSizeDeltaX, RefY + 31);
            points[1] = new Point(RefX + 213 + BossSizeDeltaX, RefY + 33);
            points[2] = new Point(RefX + 213 + BossSizeDeltaX, RefY + 55 + BossSizeDeltaY);
            points[3] = new Point(RefX + 207 + BossSizeDeltaX, RefY + 58 + BossSizeDeltaY);

            return points;
        }

        public Point[] BackBurnerLarge()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 214 + BossSizeDeltaX, RefY + 34);
            points[1] = new Point(RefX + 246 + BossSizeDeltaX, RefY + 38);
            points[2] = new Point(RefX + 251 + BossSizeDeltaX, RefY + 43);
            points[3] = new Point(RefX + 251 + BossSizeDeltaX, RefY + 45 + BossSizeDeltaY);
            points[4] = new Point(RefX + 246 + BossSizeDeltaX, RefY + 50 + BossSizeDeltaY);
            points[5] = new Point(RefX + 214 + BossSizeDeltaX, RefY + 54 + BossSizeDeltaY);
            
            return points;
        }

        public Point[] BackBurnerSmall()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 214 + BossSizeDeltaX, RefY + 34);
            points[1] = new Point(RefX + 232 + BossSizeDeltaX, RefY + 38);
            points[2] = new Point(RefX + 237 + BossSizeDeltaX, RefY + 43);
            points[3] = new Point(RefX + 237 + BossSizeDeltaX, RefY + 45 + BossSizeDeltaY);
            points[4] = new Point(RefX + 232 + BossSizeDeltaX, RefY + 50 + BossSizeDeltaY);
            points[5] = new Point(RefX + 214 + BossSizeDeltaX, RefY + 54 + BossSizeDeltaY);

            return points;
        }

        public Point[] SmallGunTop()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 70, RefY + 9);
            points[1] = new Point(RefX + 111 + BossSizeDeltaX, RefY + 9);
            points[2] = new Point(RefX + 103 + BossSizeDeltaX, RefY + 12);
            points[3] = new Point(RefX + 70, RefY + 12);
            points[4] = new Point(RefX + 69, RefY + 11);
            points[5] = new Point(RefX + 69, RefY + 10);

            return points;
        }

        public Point[] SmallGunBottom()
        {
            var points = new Point[6];

            points[0] = new Point(RefX + 70, RefY + 77 + BossSizeDeltaY);
            points[1] = new Point(RefX + 103 + BossSizeDeltaX, RefY + 77 + BossSizeDeltaY);
            points[2] = new Point(RefX + 111 + BossSizeDeltaX, RefY + 80 + BossSizeDeltaY);
            points[3] = new Point(RefX + 70, RefY + 80 + BossSizeDeltaY);
            points[4] = new Point(RefX + 69, RefY + 79);
            points[5] = new Point(RefX + 69, RefY + 78);

            return points;
        }
    }
}
