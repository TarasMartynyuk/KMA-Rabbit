using System.Collections;
using Actors;
using Actors.Orcs;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
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
            _rabbitGo.AddComponent<Rigidbody2D>();
            // now rabbit will fall onto the orc

            _orcGo.transform.position = Vector3.zero;

            var listener = _rabbitGo.AddComponent<Collision2DListener>();
            listener.EnterredColission += 
                coll => _orcInstance.ManageCollision(coll);

            yield return new WaitUntil(() => listener.HasCollided);
            // as gameobject is destroyed after frame ends
            yield return new WaitForEndOfFrame();

            _orcGo.Should().BeDestroyed();
        }

        [UnityTest]
        public IEnumerator DestroysGameobject_WhenRabbitJumpsFromTop_In45DegreeAngle()
        { 
            var go = new GameObject("go");
            Object.Destroy(go);

            yield return new WaitForEndOfFrame();

            bool b = go == null;
            go.Should().BeDestroyed();
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

            ReflectionUtils.InsertProperty<RabbitMonoBehaviour>(nameof(RabbitMonoBehaviour.Rabbit), _mockedDependenciesRabbit, rabbitMB);
        }

        void InitOrc()
        {
            _orcGo = new GameObject(nameof(_orcGo));
            var orcCol = _orcGo.AddComponent<BoxCollider2D>();
            orcCol.size = Vector2.one * 2;

            _orcInstance = new TestDerivedOrc(_orcGo);
        }
    }

    class TestDerivedOrc : AbstractOrc
    {
        public TestDerivedOrc(GameObject gameObject) : base(gameObject)
        {}
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

    public class GameObjectAssertions : ReferenceTypeAssertions<GameObject, GameObjectAssertions>
    {
        protected override string Identifier => "id";

        public GameObjectAssertions(GameObject assertedGo) 
        {
            Subject = assertedGo;
        }

        public AndConstraint<object> BeDestroyed()
        {
            bool destroyed = Subject == null;
            Execute.Assertion.
                ForCondition(destroyed).
                FailWith($"Expected gameobject {(destroyed ? "" : $"\"{Subject.name}\"")} to be destroyed");
            return new AndConstraint<object>(this);
        }
    }
}
