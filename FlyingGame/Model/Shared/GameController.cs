
namespace FlyingGame.Model.Shared
{
    public class GameController
    {
        public bool IsGameOver { get; set; }
        public int GameSpeed { get; set; }
        public int Score { get; set; }
        public int NextScoreCheckPoint { get; set; }

        public bool GroundObjectOff { get; set; }
        public bool SkyobjectOff { get; set; }

        public byte CloudGenerationRate { get; set; } 

        public byte MyJetDelta { get; set; }            
        public byte MyJetBombDelta { get; set; }        

        public byte EnemyJetBulletDelta { get; set; } 
        public byte MaxEnemyPerLvl { get; set; }
        public byte EnemyJetGenOdd { get; set; }
        public byte EnemyBulletGenOdd { get; set; }
        public byte BossMiniGunBulletGenOdd { get; set; }
        public byte BossBigGunBulletGenOdd { get; set; }
        
        public int PowerUpGenOdd { get; set; }
        public byte ExplosionVisibleMaxTime { get; set; }
        public byte BombExplosionVisibleTimeMax { get; set; }

        public int BossHp { get; set; }
        public int BossAppearScore { get; set; }
        public int BossAppearInterval { get; set; }
        public int BossHpIncrement { get; set; }
        public int LevelCompleted { get; set; }
        public byte BossMiniGunBulletSize { get; set; }
        public byte BossBigGunBulletSize { get; set; }

        public bool GodMode { get; set; }
        public bool EnableSound { get; set; }
        
        public GameController()
        {
            IsGameOver = false;
            GameSpeed = 60;
            Score = 0;
            NextScoreCheckPoint = 100;

            SkyobjectOff = false;
            GroundObjectOff = false;

            CloudGenerationRate = 75;           //Next cloud may generate right after 75% generation of current cloud and so, creating overlap
            
            MyJetDelta = 5;
            MyJetBombDelta = 5;
            EnemyJetBulletDelta = 7;
            
            MaxEnemyPerLvl = 5;
            EnemyJetGenOdd = 100;
            EnemyBulletGenOdd = 50;
            
            BossMiniGunBulletGenOdd = 100;
            BossBigGunBulletGenOdd = 200;
            BossMiniGunBulletSize = 10;
            BossBigGunBulletSize = 20;

            PowerUpGenOdd = 1500;
            ExplosionVisibleMaxTime = 10;
            BossHp = 300;
            BossAppearScore = 1000;              //default, 1000
            BossAppearInterval = 1000;
            BossHpIncrement = 1000;
            LevelCompleted = 0;
            BombExplosionVisibleTimeMax = 60;
            
            GodMode = false;                    //true: No destroying my jet
            EnableSound = true;
        }
    }
}
