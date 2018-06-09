using Background;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace UnityApiDependent
{
    [TestFixture]
    public class GameObjectMovementDependencyTests 
    {
        GameObject _freeGo;
        GameObject _dependentGo;

        static readonly Vector3 DependentPos = new Vector3(100, 50, 0);

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _freeGo = new GameObject(nameof(_freeGo));
            _dependentGo = new GameObject(nameof(_dependentGo));
            _dependentGo.transform.position = DependentPos;
        }

        [TearDown]
        public void TearDown()
        {
            _freeGo.transform.position = Vector3.zero;
            _dependentGo.transform.position = DependentPos;
        }
    
        [Test]
        public void Dependent_StaysStill_IfFreeStandsStill()
        {
            var movementDep = new GameObjectMovementDependency(_freeGo, _dependentGo, 1f);
            var startPos = _dependentGo.transform.position;

            TestUtils.EmulateUpdate(() => {
                movementDep.LateUpdate();
            }, 10);

            _dependentGo.transform.position.Should().Be(startPos);
        }

        [Test]
        public void Dependent_PassesRatioTimesTheDistanceThatFreePasses()
        {
            const float ratio = 0.5f;
            var movementDep = new GameObjectMovementDependency(_freeGo, _dependentGo, ratio);

            var freeStartPos = _freeGo.transform.position;
            var depStartPos = _dependentGo.transform.position;

            TestUtils.EmulateUpdate(() => 
            {
                _freeGo.transform.Translate(Vector3.right * 10f);
                movementDep.LateUpdate();
            }, 10);

            float freePassedDist = (_freeGo.transform.position - freeStartPos).magnitude;
            float depPassedDist = (_dependentGo.transform.position - depStartPos).magnitude;

            depPassedDist.Should().Be(freePassedDist * ratio);
        }
    }
}
