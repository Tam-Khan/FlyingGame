using System;
using FlyingGame.Model.Shared.GroundObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Sea sea = new Sea();
            
            sea.RefY2 = 380;
            
            var points = sea.SeaLevelPoints();
        }
    }
}
