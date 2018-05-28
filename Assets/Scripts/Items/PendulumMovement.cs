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
    float _pauseTimer;
    bool _paused;
    
    Vector3 _target;
    
    public PendulumMovement(Transform movingTransform, Vector3 pointB, float speed, float pause)
    {
        _movingTransform = movingTransform;

        _pointA = movingTransform.position;
        _pointB = pointB;
        
        _speed = speed;
        _pause = pause;

        _target = _pointB;

        ResetPauseTimer();
    }

    public void Update()
    {
        if(_paused)
        {
            _pauseTimer -= Time.deltaTime;
            if(_pauseTimer <= 0f)
            {
                _paused = false;
            }
            return;
        }

        if(MoveTowardsTarget(Time.deltaTime))
        {
            SwapTarget();
            _paused = true;
            ResetPauseTimer();
        }
    }

    void SwapTarget()
    {
        Assert.IsTrue(_target == _pointA || _target == _pointB);
        _target = _target == _pointA ?
            _pointB : _pointA;
    }

    bool MoveTowardsTarget(float deltaTime)
    {
        //var displacement = _targetDirection * _speed;

        //if(Vector3.Distance(_movingTransform.position, _target) < displacement.magnitude)
        //{
        //    _movingTransform.position = _target;
        //    return true;
        //}

        //_movingTransform.Translate(displacement);
        //return false;
        _movingTransform.position = Vector3.MoveTowards(_movingTransform.position, _target, _speed * deltaTime);
        return ApproximatelyEqual(_movingTransform.position, _target, .1f);
    }

    void ResetPauseTimer()  { _pauseTimer = _pause;  }
}
