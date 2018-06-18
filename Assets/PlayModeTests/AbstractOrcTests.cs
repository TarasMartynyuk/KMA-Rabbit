using System.Collections;
using Actors;
using Actors.Orcs;
using InanimateObjects.Collectables;
using NSubstitute;
using NUnit.Framework;
using PlayModeTests.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayModeTests
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
        public void OneTimeSetUp() { }

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

        [UnityTest, Timeout(10000)]
        public IEnumerator DestroysGameobject_WhenRabbitJumpsStraightFromTop()
        {
            SetupRabbitToFallFromTop();

            var listener = AddManageCollisionsListener(_rabbitGo, _orcInstance);

            yield return WaitUntilCollidedPlusOneFrame(listener);
            _orcGo.Should().BeDestroyed();
        }

        [UnityTest, Timeout(10000)]
        public IEnumerator DestroysGameobject_WhenRabbitJumpsFromTop_In45DegreeAngle()
        {
            _rabbitGo.transform.position = new Vector3(- 5f, 5f, 0f);
            var rb = _rabbitGo.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.useFullKinematicContacts = true;

            _orcGo.transform.position = Vector3.zero;

            // move orc in the top-left direction for rabbit, until they collide
            rb.velocity = new Vector2(1f, - 1f);

            var listener = _rabbitGo.AddComponent<Collision2DListener>();
            listener.EnterredColission += _ => Debug.Log("collided");
            listener.EnterredColission += coll => _orcInstance.ManageCollision(coll);

            while (! listener.HasCollided) { yield return new WaitForEndOfFrame(); }

            yield return new WaitForEndOfFrame();
            _orcGo.Should().BeDestroyed();
        }

        [UnityTest]
        public IEnumerator ManageCollision_Ignores_ObjectsNotTaggedPlayer()
        {
            SetupRabbitToFallFromTop();

            var listener = AddManageCollisionsListener(_rabbitGo, _orcInstance);

            yield return WaitUntilCollidedPlusOneFrame(listener);
            _orcGo.Should().NotBeDestroyed();
        }

        //[Test]
        public void DoesNotDestroyItself_WhenRabbitJumpsFromSides() { }

        //[Test]
        public void RabbitLosesLife_WhenJumpsFromSides() { }

        void InitRabbit()
        {
            _rabbitGo = new GameObject(nameof(_rabbitGo));
            var rabbitMB = _rabbitGo.AddComponent<MockRabbitMonoBehaviour>();

            var coll = _rabbitGo.AddComponent<BoxCollider2D>();
            coll.size = Vector2.one * 2;

            ReflectionUtils.InsertProperty<RabbitMonoBehaviour>(nameof(RabbitMonoBehaviour.Rabbit),
                _mockedDependenciesRabbit, rabbitMB);
        }

        void InitOrc()
        {
            _orcGo = new GameObject(nameof(_orcGo));
            var orcCol = _orcGo.AddComponent<BoxCollider2D>();
            orcCol.size = Vector2.one * 2;

            _orcInstance = new TestDerivedOrc(_orcGo);
        }

        IEnumerator WaitUntilCollidedPlusOneFrame(Collision2DListener listener)
        {
            yield return new WaitUntil(() => listener.HasCollided);
            // as gameobject is destroyed after frame ends
            yield return new WaitForEndOfFrame();
        }

        static Collision2DListener AddManageCollisionsListener(GameObject rabbit, TestDerivedOrc orc)
        {
            var listener = rabbit.AddComponent<Collision2DListener>();
            listener.EnterredColission += coll => orc.ManageCollision(coll);
            listener.EnterredColission += _ => Debug.Log("collided");

            return listener;
        }

        void SetupRabbitToFallFromTop()
        {
            _rabbitGo.transform.position = new Vector3(0f, 4.1f, 0f);
            _rabbitGo.AddComponent<Rigidbody2D>();

            _orcGo.transform.position = Vector3.zero;
            // now rabbit will fall onto the orc
        }

        #region mocks

        class TestDerivedOrc : AbstractOrc
        {
            public TestDerivedOrc(GameObject gameObject) : base(gameObject) { }
        }

        /// <summary>
        /// extending to remove RabbitMonoBehaviour's Awake when adding it to gameobject 
        /// (the members that we need in test are mocked inserted through reflection)
        /// </summary>
        class MockRabbitMonoBehaviour : RabbitMonoBehaviour
        {
            //new public Rabbit Rabbit { get; private set; }
            void Awake() { }
        }

        #endregion
    }
}
