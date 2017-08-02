
using System.Runtime.Remoting;
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
        public int LevelCompleted { get; set; }
        public byte DayPhase { get; set; }
        
        //Ground object related
        public bool GroundObjectOff { get; set; }
        
        //Sky object related
        public bool SkyobjectOff { get; set; }
        public byte CloudGenerationOdd { get; set; } 

        //My jet related
        public byte MyJetDelta { get; set; }
        public byte MyJetBombDelta { get; set; }
        public byte MyJetBulletSize { get; set; }
        public int PowerUpGenOdd { get; set; }

        //My jet other

        public byte RegularExplosionVisibleTimeMax { get; set; }
        public byte BombExplosionVisibleTimeMax { get; set; }
        
        //Enemy jet related
        public byte MaxEnemyJetT1PerLvl { get; set; }
        public byte MaxEnemyJetT2PerLvl { get; set; }
        public byte EnemySmallJetBulletSize { get; set; }
        public byte EnemyJetT1MovementDelta { get; set; }
        public byte EnemyJetT2MovementDelta { get; set; }
        public byte EnemyJetT1BulletDelta { get; set; }
        public byte EnemyJetT2BulletDelta { get; set; }
        public byte EnemyJetT1GenOdd { get; set; }
        public byte EnemyJetT2GenOdd { get; set; }
        public byte EnemyJetT1Hp { get; set; }
        public byte EnemyJetT2Hp { get; set; }
        
        //Enemy heli related
        public byte EnemyHeliT1BulletPerRound { get; set; }
        public byte EnemyHeliT1BulletSize { get; set; }
        public byte EnemyHeliT1MovementDelta { get; set; }
        public byte EnemyHeliT1BulletDelta { get; set; }
        public byte EnemyHeliT1GenOdd { get; set; }
        public byte MaxEnemyHeliT1PerLvl { get; set; }
        public byte EnemyHeliT1Hp { get; set; }

        //Enemy shared
        public byte EnemyBulletGenOdd { get; set; }
        public byte SmallEnemyBulletMax { get; set; }
        
        //Boss related
        public byte BossType { get; set; }
        public byte BossMiniGunBulletSize { get; set; }
        public byte BossBigGunBulletSize { get; set; }
        public byte BossBigGunBulletDelta { get; set; }
        public byte BossMiniGunBulletDelta { get; set; }
        public byte BossMiniGunBulletGenOdd { get; set; }
        public byte BossBigGunBulletGenOdd { get; set; }
        
        public int BossHp { get; set; }
        public int BossAppearScore { get; set; }
        public int BossAppearInterval { get; set; }
        public int BossHpIncrement { get; set; }
        
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
            LevelCompleted = 0;
            DayPhase = 1;
            
            //Ground background related
            GroundObjectOff = false;

            //Sky background related
            SkyobjectOff = false;
            CloudGenerationOdd = 50;           //Next cloud may generate right after 50% generation of current cloud and so, overlap

            //Myjet related

            MyJetDelta = 5;
            MyJetBombDelta = 5;
            MyJetBulletSize = 3;
            PowerUpGenOdd = 1500;

            //Myjet other
            RegularExplosionVisibleTimeMax = 10;
            BombExplosionVisibleTimeMax = 60;
            
            //Enemy jet related
            EnemyJetT1MovementDelta = 2;
            EnemyJetT2MovementDelta = 4;
            EnemyJetT1BulletDelta = 4;
            EnemyJetT2BulletDelta = 4;
            EnemySmallJetBulletSize = 10;
            MaxEnemyJetT1PerLvl = 4;
            EnemyJetT1GenOdd = 100;

            MaxEnemyJetT2PerLvl = 0;
            EnemyJetT2GenOdd = 100;

            EnemyJetT1Hp = 1;
            EnemyJetT2Hp = 2;

            //Enemy heli related
            EnemyHeliT1BulletSize = 6;
            EnemyHeliT1BulletDelta = 5;
            EnemyHeliT1MovementDelta = 2;
            EnemyHeliT1BulletPerRound = 2;
            MaxEnemyHeliT1PerLvl = 1;
            EnemyHeliT1GenOdd = 250;

            EnemyHeliT1Hp = 10;
            
            //Enemy shared
            EnemyBulletGenOdd = 100;
            SmallEnemyBulletMax = 10; 

            //Boss related
            BossType = 1;

            BossMiniGunBulletDelta = 5;
            BossMiniGunBulletSize = 10;
            
            BossBigGunBulletSize = 20;
            BossBigGunBulletDelta = 6;
            
            BossMiniGunBulletGenOdd = 100;
            BossBigGunBulletGenOdd = 200;
            
            //follwing four values determine level durations
            BossHp = 400;                       
            BossAppearScore = 500;              
            BossAppearInterval = 600;           
            BossHpIncrement = 200;              
            
            //Debug helpers
            GodMode = false;                    //true: No destroying my jet
            EnableSound = true;
            IsFullPowered = false;
        }
    }
}
