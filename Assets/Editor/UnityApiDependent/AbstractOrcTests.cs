using Actors;
using Actors.Orcs;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;

namespace UnityApiDependent
{
    class AbstractOrcTests
    {
        GameObject _rabbitGo;
        GameObject _orcGo;
        TestDerivedOrc _orcInstance;

        [SetUp]
        public void SetUp()
        {
            _rabbitGo = new GameObject(nameof(_rabbitGo));
            _rabbitGo.AddComponent<Rabbit>();

            _orcGo = new GameObject();
            _orcInstance = _orcGo.AddComponent<TestDerivedOrc>();

            var k = Substitute.For<AbstractOrc>();
        }

        [Test]
        public void PatrollingOnTurningAround_SwapsGameobject()
        {
            var startScale = _orcGo.transform.localScale;
            //var

        }

        [Test]
        public void DestroysItself_WhenRabbitJumpsFromTop()
        { 
            MonoBehaviour m;


        }

        [Test]
        public void DoesNotDestroyItself_WhenRabbitJumpsFromSides()
        { 


        }

        [Test]
        public void RabbitLosesLife_WhenJumpsFromSides()
        { 
            MonoBehaviour m;


        }
    }

    class TestDerivedOrc : AbstractOrc
    {

    }
}
