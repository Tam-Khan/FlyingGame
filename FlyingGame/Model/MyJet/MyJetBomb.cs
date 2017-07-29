
namespace FlyingGame.Model.MyJet
{
    public class MyJetBomb
    {
        public int BombX { get; set; }
        public int BombY { get; set; }
        public byte BombExplodedDisplayTimer { get; set; }
        public bool BombXploded { get; set; }
        public byte BombRemains { get; set; }
        
        public MyJetBomb()
        {
            BombX = 0;
            BombY = 0;
            BombXploded = false;
            BombExplodedDisplayTimer = 0;
            BombRemains = 4;
        }
    }
}
