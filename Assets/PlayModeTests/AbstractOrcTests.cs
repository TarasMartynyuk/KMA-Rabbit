using System;
using System.Collections;
using System.Threading.Tasks;
using Actors;
using Actors.Orcs;
using InanimateObjects.Collectables;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.TestTools;
using static ReflectionUtils;
using PlayerControl;

namespace UnityApiDependent
{
    public class AbstractOrcTests
    {
        GameObject _rabbitGo;
        GameObject _orcGo;
        //BoxCollider2D _orcCollider;
        ILivesComponent _livesMock;
        Rabbit _mockedDependenciesRabbit;
        TestDerivedOrc _orcInstance;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _livesMock = Substitute.For<ILivesComponent>();
            var statsMock = Substitute.For<IRabbitStats>();
            _mockedDependenciesRabbit = new Rabbit(_livesMock, statsMock);
            InitRabbit();
            InitOrc();
        }

        //[Test]
        public void PatrollingOnTurningAround_SwapsGameobject()
        {
            //var startScale = _orcGo.transform.localScale;
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

            var listener = _rabbitGo.AddComponent<Collision2DListener>();
            listener.EnterredColission += coll =>
            {
                Debug.Log("collided");
                _orcInstance.ManageCollision(coll);

                _livesMock.Received(1).LoseLife();
            };


            yield return new WaitForSeconds(3);
            //_livesMock.Received(1).LoseLife();
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

        void InitRabbit()
        {
            _rabbitGo = new GameObject(nameof(_rabbitGo));
            var rabbitMB = _rabbitGo.AddComponent<MockRabbitMonoBehaviour>();

            var coll = _rabbitGo.AddComponent<BoxCollider2D>();
            coll.size = Vector2.one * 2;

            InsertProperty<RabbitMonoBehaviour>(nameof(RabbitMonoBehaviour.Rabbit), _mockedDependenciesRabbit, rabbitMB);
        }

        void InitOrc()
        {
            _orcGo = new GameObject();
            var orcCol = _orcGo.AddComponent<BoxCollider2D>();
            orcCol.size = Vector2.one * 2;

            _orcInstance = new TestDerivedOrc(_orcGo);
        }
    }

    class TestDerivedOrc : AbstractOrc
    {
        public TestDerivedOrc(GameObject gameObject) : base(gameObject)
        {
        }
    }
    /// <summary>
    /// extending to remove RabbitMonoBehaviour's Awake when adding it to gameobject 
    /// (the members that we need in test are mocked inserted through reflection)
    /// </summary>
    class MockRabbitMonoBehaviour : RabbitMonoBehaviour
    {
        //new public Rabbit Rabbit { get; private set; }
        void Awake() {}
    }
}
