using System;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private Settings _set = Settings.Init;

        [UITestMethod]
        public void GameCycleWon()
        {
            Engine engine = new Engine(_set)
            {
                Enemies = new System.Collections.Generic.List<Enemy>()
            };
            Assert.AreEqual(GameState.Won,engine.GameCycle(new PlayerInput()));
        }

        [UITestMethod]
        public void GameCycleLost()
        {
            Engine engine = new Engine(_set);
            engine.Enemies[0].PlaceOnBoard(0,0);
            double enemyAndPlayerRad = engine.Enemies[0].Radius + engine.Human.Radius;
            engine.Human.PlaceOnBoard(enemyAndPlayerRad,0);
            Assert.AreEqual(GameState.Lost,engine.GameCycle(new PlayerInput()));
        }

        [UITestMethod]
        public void GameCycleOn()
        {
            Engine engine = new Engine(_set);
            Assert.AreEqual(GameState.On,engine.GameCycle(new PlayerInput()));
        }
    }
}
