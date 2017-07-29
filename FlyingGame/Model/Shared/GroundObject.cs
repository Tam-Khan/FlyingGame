using System.Drawing;
using System.Windows.Forms;

namespace FlyingGame.Model.Shared
{
    public class GroundObject
    {
        public bool MountainDrawInitiated { get; set; }
        public int CountToMountainHgt { get; set; }
        public int CurrCountToMountainHgt { get; set; }
        
        public int Xdelta { get; set; }
        public int Ydelta { get; set; }
        
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int X3 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
        public int Y3 { get; set; }

        public Brush MountainColor { get; set; }
    }
}
