using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace FlyingGame.Model.Shared.GroundObject
{
    public class Sea: GroundObject
    {

        private byte _waveHeight;
        
        public Sea()
        {
            Counter = 0;
            _waveHeight = (byte)Rand.Next(1, 2);                        //generate varied wave height
        }

        public Point[] SeaLevelPoints()
        {
            var waveWidth = Rand.Next(40, 60);                          //generate varied wave width 
            var points = new Point[Width / waveWidth + 5];
            var lastI = 0;

            for (int i = 0; i < (Width / waveWidth + 2); i++)
            {
                if (i == 0)
                {
                    points[0] = new Point(0, RefY2 - 15 - _waveHeight); 
                }
                else
                {
                    if (i > 0 && i%2 == 1)
                    {
                        points[i] = new Point(0 + waveWidth * i, RefY2 - 15 + _waveHeight * 2);
                    }
                    else
                    {
                        points[i] = new Point(0 + waveWidth * i, RefY2 - 15 - _waveHeight * 2);
                    }

                    if (i == (Width / waveWidth + 2)-1) lastI = i;
                }
            }
            
            //Last three points to add smooth end on right side.
            
            points[lastI] = new Point(Width + waveWidth, RefY2-15);
            points[lastI + 1] = new Point(Width + waveWidth, RefY2);
            points[lastI + 2] = new Point(0, RefY2);
            return points;
        }
    }
}
