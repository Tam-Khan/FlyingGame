using FlyingGame.Model.Shared;

namespace FlyingGame.Model.Enemy.Boss
{
    public abstract class Boss: GameController
    {
        public bool IsBossInitiated { get; set; }

        //Boss to get chunkier as level goes up
        public byte BossSizeDeltaX { get; set; }        
        public byte BossSizeDeltaY { get; set; }

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
        
        public byte MovementDelta { get; set; }

        public int HitPerLevel{get { return OriginalHp/3; }}

        public int BossDestroyedFireWorkCounter { get; set; }
        
        public Boss()
        {
            RefX = 0;
            RefY = 0;
            CurrHp = BossHp;
            OriginalHp = BossHp;
            IsBossInitiated = false;
            CurrHitLevel = 1;
            FireLvlBigGun = 1;
            FireLvlMiniGun = 1;
            Direction = 0;
            ToggleBurner = false;
            BossDestroyedFireWorkCounter = 0;
            MovementDelta = 2;
        }
    }
}
