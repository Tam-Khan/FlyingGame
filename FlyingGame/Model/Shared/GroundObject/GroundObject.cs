using System;
using System.Drawing;

namespace FlyingGame.Model.Shared.GroundObject
{
    public abstract class GroundObject
    {
        public bool DrawInitiated { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Brush Color { get; set; }
        public byte MoveDeltaX { get; set; }

        public Point[] ProcessedPoints { get; set; }

        public int RefX { get; set; }
        public int RefX2 { get; set; }
        public int RefY2 { get; set; }
        public int RefY { get; set; }

        public Random Rand = new Random();
        public byte Counter { get; set; }
    }
}
