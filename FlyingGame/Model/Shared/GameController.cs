
using System.Windows.Forms;

namespace FlyingGame.Model.Shared
{
    public class GameController
    {
        public bool IsGameOver { get; set; }
        public int GameSpeed { get; set; }
        public int Score { get; set; }
        public int NextScoreCheckPoint { get; set; }
        public bool InLevelTransition { get; set; }
        public byte LevelTransitionCountDown { get; set; }

        public byte MaxEnemyPerLvl { get; set; }
        public byte ExplosionVisibleTimeMax { get; set; }
        public byte BombExplosionVisibleTimeMax { get; set; }

        //Sizes
        public byte BossMiniGunBulletSize { get; set; }
        public byte BossBigGunBulletSize { get; set; }
        public byte MyJetBulletSize { get; set; }
        public byte EnemyJetBulletSize { get; set; }

        //Toggles
        public bool GroundObjectOff { get; set; }
        public bool SkyobjectOff { get; set; }

        //Deltas
        public byte MountainDelta { get; set; }
        
        public byte MyJetDelta { get; set; }            
        public byte MyJetBombDelta { get; set; }
        public byte EnemyJetMovementDelta { get; set; }
        public byte EnemyJetBulletDelta { get; set; }
        public byte BossBigGunBulletDelta { get; set; }
        public byte BossMiniGunBulletDelta { get; set; }

        public byte CloudGenerationOdd { get; set; } 

        //Odds
        public byte EnemyJetGenOdd { get; set; }
        public byte EnemyBulletGenOdd { get; set; }
        public byte BossMiniGunBulletGenOdd { get; set; }
        public byte BossBigGunBulletGenOdd { get; set; }
        public byte Type2JetAppearanceOdd { get; set; }
        
        public int PowerUpGenOdd { get; set; }
        
        //Boss related
        public int BossHp { get; set; }
        public int BossAppearScore { get; set; }
        public int BossAppearInterval { get; set; }
        public int BossHpIncrement { get; set; }
        
        //Level related
        public int LevelCompleted { get; set; }
        
        //others
        public bool GodMode { get; set; }
        public bool EnableSound { get; set; }
        public bool IsFullPowered { get; set; }
        
        public GameController()
        {
            IsGameOver = false;
            GameSpeed = 60;
            Score = 0;
            NextScoreCheckPoint = 100;
            InLevelTransition = false;
            LevelTransitionCountDown = 0;

            SkyobjectOff = false;
            GroundObjectOff = false;

            MountainDelta = 5;
            MyJetDelta = 5;
            MyJetBombDelta = 5;
            EnemyJetMovementDelta = 3;
            EnemyJetBulletDelta = 6;
            BossMiniGunBulletDelta = 7;
            BossBigGunBulletDelta = 8;

            CloudGenerationOdd = 50;           //Next cloud may generate right after 75% generation of current cloud and so, creating overlap

            MyJetBulletSize = 3;
            EnemyJetBulletSize = 10;
            BossMiniGunBulletSize = 10;
            BossBigGunBulletSize = 20;
            
            MaxEnemyPerLvl = 5;
            
            EnemyJetGenOdd = 100;
            EnemyBulletGenOdd = 50;
            BossMiniGunBulletGenOdd = 100;
            BossBigGunBulletGenOdd = 200;
            Type2JetAppearanceOdd = 100;

            PowerUpGenOdd = 1500;
            ExplosionVisibleTimeMax = 10;
            
            //Change following 4 values to control level duration (quicker the boss appears and gets killed, quicker the level finishes)
            BossHp = 200;                       
            BossAppearScore = 500;              
            BossAppearInterval = 500;           
            BossHpIncrement = 100;              
            
            LevelCompleted = 0;
            BombExplosionVisibleTimeMax = 60;
            
            GodMode = false;                    //true: No destroying my jet
            EnableSound = true;
            IsFullPowered = false;
        }
    }
}
