using System.Drawing;
using System.Linq;

namespace FlyingGame.Model.Shared
{
    public class CloudFactory
    {
        public bool CloudInitiated { get; set; }
        public int RefX { get; set; }
        public int DeltaX { get; set; }
        public byte Width { get; set; }
        public Brush FillColor { get; set; }
        public Point[] ShapePoints { get; set; }
        public bool IsGenerated { get; set; }
        public bool HasBorder { get; set; }


        //Initial shape points to be processed for X delta so that cloud moves to left.
        public Point[] ProcessedShapePoints()
        {
            var newPoints = new Point[ShapePoints.Count()];

            for (int i = 0; i < ShapePoints.Count(); i++)
            {
                newPoints[i].X = ShapePoints[i].X - DeltaX;
                newPoints[i].Y = ShapePoints[i].Y;
            }
            return newPoints;
        }
    }
}
