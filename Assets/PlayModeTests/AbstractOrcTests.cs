using System.Collections;
using Actors;
using Actors.Orcs;
using InanimateObjects.Collectables;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.TestTools;
using static ReflectionUtils;

namespace UnityApiDependent
{
    public class AbstractOrcTests
    {
        GameObject _rabbitGo;
        GameObject _orcGo;
        //BoxCollider2D _orcCollider;
        ILivesComponent _livesMock;
        TestDerivedOrc _orcInstance;

        [SetUp]
        public void SetUp()
        {
            _rabbitGo = new GameObject(nameof(_rabbitGo));
            var rabbitMB = _rabbitGo.AddComponent<RabbitMonoBehaviour>();
            var coll = _rabbitGo.AddComponent<BoxCollider2D>();
            coll.size = Vector2.one * 2;

            _livesMock = Substitute.For<ILivesComponent>();
            var statsMock = Substitute.For<IRabbitStats>();
            var rabbit = new Rabbit(_livesMock, statsMock);

            InsertProperty(nameof(RabbitMonoBehaviour.Rabbit), rabbit, rabbitMB);

            _orcGo = new GameObject();
            var orcCol = _rabbitGo.AddComponent<BoxCollider2D>();
            orcCol.size = Vector2.one * 2;
            _orcInstance = new TestDerivedOrc(_orcGo);
        }

        //[Test]
        public void PatrollingOnTurningAround_SwapsGameobject()
        {
            var startScale = _orcGo.transform.localScale;
            //var

        }

        [UnityTest]
        public IEnumerator DestroysGameobject_WhenRabbitJumpsStraightFromTop()
        {
            _rabbitGo.transform.position = new Vector3(0f, 4.1f, 0f);
            var rigibBody = _rabbitGo.AddComponent<Rigidbody2D>();
            rigibBody.bodyType = RigidbodyType2D.Dynamic;
            // now rabbit will fall onto the orc

            _orcGo.transform.position = Vector3.zero;

            //var listener = _rabbitGo.AddComponent<Collision2DListener>();
            //listener.EnterredColission += async () => {
            //    Debug.Log("collided");
            //    await Task.Delay(TimeSpan.FromSeconds(1));
            //    _livesMock.Received(1).LoseLife();
            //};

            yield return new WaitForSeconds(2);
            _livesMock.Received(1).LoseLife();
        }

        //[Test]
        public void DestroysGameobject_WhenRabbitJumpsFromTop_In45DegreeAngle()
        { 
            //_orcGo.transform.position = Vector3.zero;

            //_rabbitGo.transform.position = new Vector3(0, )


        }

        //[Test]
        public void DoesNotDestroyItself_WhenRabbitJumpsFromSides()
        { 


        }

        //[Test]
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
