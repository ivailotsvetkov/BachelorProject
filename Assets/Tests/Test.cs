using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test
    {
        //Bullet Bullet;
        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator TestVideoStarts()
        {
            /*if(Overmind.SphereManager.firstVideo.currentState.ToString() == "Initialized")
            {
                Assert.IsTrue(true);
            }*/
            yield return null;
        }
        [UnityTest]
        public IEnumerator TestVideoEnds()
        {
           /* if(Overmind.SphereManager.firstVideo.currentState.ToString() == "End")
            {
                Assert.IsTrue(true);
            }*/
            yield return null;
        }
        
        [Test]
        public void TestMakeDecision()
        {
            /*string before=decision.decision1.name;
            makeDecision(before);
            string after=decision.decision1.name;
            Assert.IsTrue(!before.isEqualTo(after));*/
        }
        [UnityTest]
        public IEnumerator TestTicTacToeWon()
        {
            /*if(ETicGameState==ETicGameState.Win)
            {
                int result=Overmind.TicGameManager.checkIfWin();
                Assert.IsTrue(result==1);
            }*/
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestMemoryGameFinished()
        {
            /*if(EMemoryGameState==EMemoryGameState.Win)
            {
                Assert.IsTrue(true);
            }*/
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestAsteroidCollision()
        {
            /*float score=Overmind.AsteroidGameManager.score;
            if(score==1)
            {
                Assert.IsTrue(true);
            }*/
            yield return null;

        }

        [Test]
        public void TestAddingScore()
        {
            /*float before=Score.instance.healthBar.getAmount;
            Score.instance.healthBar.fillAmount+=0.1;
            float after=Score.instance.healthBar.getAmount;
            Assert.IsTrue(after>before);*/
        }
        
        [Test]
        //UINX_Spot script
        public void TestSpotGameOnSpotEventSuccess()
        {
            /*float before=Overmind.SpotGameManager.score;
            Overmind.SpotGameManager.OnSpotEvent(e.Success);
            float after=Overmind.SpotGameManager.score;
            Assert.IsTrue(after>before);*/
        }
        [Test]
        //UINX_Spot script
        public void TestSpotGameOnSpotEventFail()
        {
            /*float before=Overmind.SpotGameManager.score;
            Overmind.SpotGameManager.OnSpotEvent(!e.Success);
            float after=Overmind.SpotGameManager.score;
            Assert.IsTrue(after==before);*/
        }
        [Test]
        public void TestPlayerControllerIsActive()
        {
            /*Overmind.PlayerStation.SetUINXMode(true);
            var controllers = Resources.FindObjectsOfTypeAll<UINXController>();
            Assert.IsTrue(controllers.length==1);*/
        }
        
    }
}
