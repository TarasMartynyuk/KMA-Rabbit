using System;
using System.Collections;
using Actors;
using Actors.Orcs;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.TestTools;

namespace UnityApiDependent
{
    class AbstractOrcTests
    {
        GameObject _rabbitGo;
        GameObject _orcGo;
        BoxCollider2D _orcCollider;

        TestDerivedOrc _orcInstance;

        [SetUp]
        public void SetUp()
        {
            //_rabbitGo = new GameObject(nameof(_rabbitGo));
            ////_rabbitGo.AddComponent<Rabbit>();

            //_orcGo = new GameObject();
            //_orcCollider = _orcGo.AddComponent<BoxCollider2D>();
            //_orcCollider.offset = Vector2.zero;
            //_orcCollider.size = new Vector2(5f, 5f);

            //_orcInstance = new TestDerivedOrc(_orcGo);

            //var k = Substitute.For<AbstractOrc>();
        }

        [Test]
        public void PatrollingOnTurningAround_SwapsGameobject()
        {
            var startScale = _orcGo.transform.localScale;
            //var

        }

        [UnityTest]
        public IEnumerator DestroysGameobject_WhenRabbitJumpsStraightFromTop()
        {
            //var collision = Substitute.For<Collision2D>();

            //var perpendicularNormalPoint = Substitute.For<ContactPoint2D>();
            //var points = new ContactPoint2D[] { new ContactPoint2D()  }
            //collision.GetContacts(Arg.Any<ContactPoint2D[]>()).Returns


            throw new NotImplementedException();
        }

        [Test]
        public void DestroysGameobject_WhenRabbitJumpsFromTop_In45DegreeAngle()
        { 
            //_orcGo.transform.position = Vector3.zero;

            //_rabbitGo.transform.position = new Vector3(0, )


        }

        [Test]
        public void DoesNotDestroyItself_WhenRabbitJumpsFromSides()
        { 


        }

        [Test]
        public void RabbitLosesLife_WhenJumpsFromSides()
        { 


        }
    }

    class TestDerivedOrc : AbstractOrc
    {
        public TestDerivedOrc(GameObject gameObject) : base(gameObject)
        {
        }
    }
}
