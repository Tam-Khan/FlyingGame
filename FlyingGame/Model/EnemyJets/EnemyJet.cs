using System.Drawing;
using FlyingGame.Model.Shared;

namespace FlyingGame.Model.EnemyJets
{
    public class EnemyJet:GameController
    {
        public int RefX { get; set; }
        public int RefY { get; set; }
        public sbyte MovementState { get; set; }
        public bool TwistEnemy { get; set; }
        public sbyte Direction { get; set; }
        public bool MakeRedundant { get; set; }
        public byte JetType { get; set; }
        public byte Hp { get; set; }
       
        public EnemyJet()
        {
            RefX = 0;
            RefY = 0;
            MovementState = 0;
            TwistEnemy = false;
            Direction = 4;
            MakeRedundant = false;
            JetType = 1;
        }
        
        public int X2
        {get { return RefX - 40; }} //Approximate length of jet

        public int Y2
        {get { return RefY + 20; }} //Approximate height of jet (wing to wing)


        //Process polygon drawing points for various Enemy Jet parts.
        public Point[] FuselagePosOne(int refX, int refY)
        {
            var points = new Point[6];
            points[0] = new Point(refX - 5 , refY + 10);
            points[1] = new Point(refX - 0, refY + 12);
            points[2] = new Point(refX - 5, refY + 14);
            points[3] = new Point(refX - 35, refY + 14);
            points[4] = new Point(refX - 40, refY + 12);
            points[5] = new Point(refX - 35, refY + 10);
            return points;
        }
        
        public Point[] WingBottomPosOne(int refX, int refY)
        {
            var points = new Point[5];
            points[0] = new Point(refX - 9, refY + 15);
            points[1] = new Point(refX - 9, refY + 15 + 10);
            points[2] = new Point(refX - 15, refY + 15 + 10);
            points[3] = new Point(refX - 18, refY + 15 + 10);
            points[4] = new Point(refX - 28, refY + 15);
            return points;
        }

        public Point[] WingTopPosOne(int refX, int refY)
        {
            var points = new Point[5];
            points[0] = new Point(refX - 9, refY + 9);
            points[1] = new Point(refX - 9, refY + 9 - 10);
            points[2] = new Point(refX - 15, refY + 9 - 10);
            points[3] = new Point(refX - 18, refY + 9 - 10);
            points[4] = new Point(refX - 28, refY + 9);
            return points;
        }

        public Point[] WingBottomPosTwo(int refX, int refY)
        {
            var points = new Point[5];
            points[0] = new Point(refX - 9, refY + 15);
            points[1] = new Point(refX - 9, refY + 15+15);
            points[2] = new Point(refX - 18, refY + 15 + 15);
            points[3] = new Point(refX - 21, refY + 15 + 15);
            points[4] = new Point(refX - 31, refY + 15);
            
            return points;
        }

        public Point[] WingTopPosTwo(int refX, int refY)
        {
            var points = new Point[5];
            points[0] = new Point(refX - 9, refY + 9);
            points[1] = new Point(refX - 9, refY + 9 - 15);
            points[2] = new Point(refX - 18, refY + 9 - 15);
            points[3] = new Point(refX - 21, refY + 9 - 15);
            points[4] = new Point(refX - 31, refY + 9);

            return points;
        }
    }
}
