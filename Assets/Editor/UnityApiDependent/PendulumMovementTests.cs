using FluentAssertions;
using InanimateObjects.Environment;
using NUnit.Framework;
using UnityEngine;

namespace UnityApiDependent
{
    [TestFixture]
    public class PendulumMovementTests
    {
        Transform _movingTransform;
        readonly Vector3 _pointA = Vector3.zero;
        readonly Vector3 _pointB = new Vector3(5f, 5f, 0f);

        const float Speed = 1f;
        const float Pause = 1f;
        //float _dist;
        int _secondsForOneWayTrip;


        [SetUp]
        public void SetUp()
        {
            _movingTransform = new GameObject("moving").transform;
            _movingTransform.position = _pointA;
            float dist = Vector3.Distance(_pointA, _pointB);
            _secondsForOneWayTrip = Mathf.CeilToInt(dist / Speed);
        }

        [Test]
        public void Transform_MustReachPointB()
        {
            var movement = TestInstance();

            bool visitedPointB = false;
            TestUtils.EmulateUpdate(() => 
                {
                    movement.Update(1f);
                    if(Visits(_pointB)) 
                    { visitedPointB = true; }

                }, _secondsForOneWayTrip + 1);

            visitedPointB.Should().BeTrue();
        }

    
        [Test]
        public void Transform_MustReturnToPointA()
        {
            var movement = TestInstance();

            bool visitedPointA = false;
            TestUtils.EmulateUpdate(() => 
                {
                    movement.Update(1f);

                    //Debug.Log($"pos: ")
                    if(Visits(_pointA)) 
                    { visitedPointA = true; }

                }, 2 * _secondsForOneWayTrip + 2);

            visitedPointA.Should().BeTrue();
        }

        [Test]
        public void Transform_MustMovePerpetually()
        {
            var movement = TestInstance();

            const int twoWayTrips = 10;

            int timesVisitedA = 0;
            int timesVisitedB = 0;

            TestUtils.EmulateUpdate(() => 
                {
                    movement.Update(1f);

                    if(Visits(_pointA)) 
                    { timesVisitedA++; }

                    if(Visits(_pointB)) 
                    { timesVisitedB++; }

                }, twoWayTrips * 2 * _secondsForOneWayTrip + 1);

            timesVisitedA.Should().BeGreaterOrEqualTo(twoWayTrips);
            timesVisitedB.Should().BeGreaterOrEqualTo(twoWayTrips);
        }

        /// <summary>
        /// true if _movementTransform comes sufficiently close to the point
        /// </summary>
        /// <returns></returns>
        bool Visits(Vector3 point)
        {
            float dist = Vector3.Distance(_movingTransform.position, point);
            return dist < Speed;
        }

        PendulumMovement TestInstance()
        {
            return  new PendulumMovement(_movingTransform, _pointB, Speed, Pause);
        }
    }
}
