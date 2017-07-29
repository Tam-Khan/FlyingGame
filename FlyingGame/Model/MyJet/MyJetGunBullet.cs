namespace FlyingGame.Model.MyJet
{
    public class MyJetGunBullet
    {
        public int X { get; set; }
        public int Y { get; set; }
        public byte Size { get; set; }
        public bool GunFireStart { get; set; }
        public int DeltaY { get; set; }
       
        public MyJetGunBullet()
        {
            Size = 3;
            GunFireStart = false;
            DeltaY = 0;
        }

    }
}
