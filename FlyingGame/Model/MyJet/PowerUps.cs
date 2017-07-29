using System.Drawing;

namespace FlyingGame.Model.MyJet
{
    public class PowerUps
    {
        public int PowerUpType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blink { get; set; }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
        public bool IsRightToLeft { get; set; }
        public bool IsTopToBottom { get; set; }
        public byte BounceCounter { get; set; }
        
        
        //Calculate poewr up size based on type
        public byte Size
        {
            get
            {
                if (PowerUpType == 1)       //Size Bomb up
                {
                    return 20;
                }
                else if (PowerUpType == 2)  //Size Bullet limit up
                {
                    return 18;
                }
                return 15;                  //Size Active Gun up
            }
        }

        public Brush PowerUpColour
        {
            get
            {
                if (Blink)
                {
                    return Brushes.Teal;            //Try power up blink in every other turn
                }
                else if (PowerUpType == 1)          //Colour for bomb up
                {
                    return Brushes.Orange;
                }
                else if (PowerUpType == 2)          //Colour for Bullet limit up
                {
                    return Brushes.CornflowerBlue;
                }
                else if(PowerUpType==3)             //Colour for Active Gun up
                {
                    return Brushes.DarkMagenta;
                }
                return null;
            }
        }

        public PowerUps()
        {
            X = 0;
            Y = 0;
            DeltaX = 5;
            DeltaY = 5;
            IsRightToLeft = true;
            IsTopToBottom = true;
            PowerUpType = 0;
            BounceCounter = 0;
        }
    }
}
