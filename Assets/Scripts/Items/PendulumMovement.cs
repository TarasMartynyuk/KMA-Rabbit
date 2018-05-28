using UnityEngine;
using UnityEngine.Assertions;
using static Utils.VectorUtils;

public class PendulumMovement 
{
    readonly Transform _movingTransform;

    readonly Vector3 _pointA;
    readonly Vector3 _pointB;

    readonly float _speed;
    readonly float _pause;
    
    Vector3 _target;
    // normalized
    //Vector3 _targetDirection;
    
    public PendulumMovement(Transform movingTransform, Vector3 pointB, float speed, float pause)
    {
        _movingTransform = movingTransform;

        _pointA = movingTransform.position;
        _pointB = pointB;
        
        _speed = speed;
        _pause = pause;

        _target = _pointB;
        //_targetDirection = Direction(_movingTransform.position, _target);
    }

    public void Update()
    {
        if(MoveTowardsTarget())
        {
            SwapTarget();
        }
    }

    void SwapTarget()
    {
        Assert.IsTrue(_target == _pointA || _target == _pointB);
        _target = _target == _pointA ?
            _pointB : _pointA;
    }

    bool MoveTowardsTarget()
    {
        //var displacement = _targetDirection * _speed;

        //if(Vector3.Distance(_movingTransform.position, _target) < displacement.magnitude)
        //{
        //    _movingTransform.position = _target;
        //    return true;
        //}

        //_movingTransform.Translate(displacement);
        //return false;
        _movingTransform.position = Vector3.MoveTowards(_movingTransform.position, _target, _speed);
        return ApproximatelyEqual(_movingTransform.position, _target, .1f);
    }
}
