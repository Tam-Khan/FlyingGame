namespace FlyingGame.Model.Enemy
{
    public class EnemyBullet
    {
        public int X { get; set; }
        public int Y { get; set; }
        public byte Size { get; set; }
        public bool GunFireStart { get; set; }
        
        public bool IsBossGun { get; set; }
        public bool IsBigGun { get; set; }
        
        public bool IsHeliGun { get; set; }
        
        public int InitialX { get; set; }
        public int InitialY { get; set; }
        public int MyJetX { get; set; }
        public int MyJetY { get; set; }

        public int DeltaX { get; set; }

        //try to calculate bullet vertical movement change based on my jet's location (used in boss and heli gun fire)
        public double DeltaGunTargetY
        {
            get
            {
                double diffX = InitialX - MyJetX;                   //X difference between my jet and bullet's initial x              ---------X diff------* Bullet
                double diffY = MyJetY-InitialY;                     //Y difference between my jet and bullet's initial y              |
                double countRoundToCoverDiffX = diffX/DeltaX;       //Calculate timer tick required to cross X difference             |Y diff
                return diffY/countRoundToCoverDiffX;                //Calculate Y distance required to cross Y difference             |
            }                                                                                                                  //Myjet*
        }

        public EnemyBullet()
        {
            Size = 3;
            GunFireStart = false;
            IsBossGun = false;
            IsBigGun = false;
            DeltaX = 8;
            IsHeliGun = false;
        }
    }
}
