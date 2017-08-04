using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FlyingGame.Model.Enemy
{
    public class EnemyBullet
    {
        private sbyte _deltaGunTargetY;
        private sbyte  _deltaX;
        public int X { get; set; }
        public int Y { get; set; }
        public byte Size { get; set; }
        public bool GunFireStart { get; set; }

        public bool IsHollowBullet { get; set; }
        public Brush Colour { get; set; }
        public Pen ColourPen { get; set; }
        
        public byte Damage { get; set; }

        public int InitialX { get; set; }
        public int InitialY { get; set; }
        public int MyJetX { get; set; }
        public int MyJetY { get; set; }

        private sbyte DeltaDirectionX()
        {
            if (InitialX - MyJetX < 0)
            {
                return -1;
            }

            return 1;
        }

        public int DeltaX
        {
            get
            {
                return _deltaX * DeltaDirectionX();
            }
            set { _deltaX = (sbyte) value; }
        }

        //try to calculate bullet vertical movement change based on my jet's location (used in boss and heli gun fire)
        public double DeltaGunTargetY
        {
            get
            {
                if (_deltaGunTargetY != 0) return _deltaGunTargetY;
                if (MyJetX == 0) return 0;
                double diffX = Math.Abs(InitialX - MyJetX);                     //X difference between my jet and bullet's initial x              ---------X diff------* Bullet
                double diffY = (MyJetY-InitialY);                                 //Y difference between my jet and bullet's initial y              |
                double countRoundToCoverDiffX = diffX/Math.Abs(DeltaX);                   //Calculate timer tick required to cross X difference             |Y diff
                return Math.Round(diffY/countRoundToCoverDiffX,0);              //Calculate Y distance required to cross Y difference             |
            }                                                                                                                              //Myjet*
            set
            {
                _deltaGunTargetY = (sbyte) value;
            }
        }

        public EnemyBullet()
        {
            Size = 3;
            GunFireStart = false;
            DeltaX = 8;
            IsHollowBullet = false;
        }
    }
}
