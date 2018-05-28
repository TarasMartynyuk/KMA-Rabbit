using UnityEngine;

public class MovingPlatform : MonoBehaviour 
{
    [SerializeField]
    Transform _pointB;
    [SerializeField]
    float _speed;
    [SerializeField]
    float _pause;

    PendulumMovement _pendulumMovement;

	void Awake() 
	{
		_pendulumMovement = new PendulumMovement(transform, _pointB.position, _speed, _pause);
	}
	
	void Update () 
	{
		_pendulumMovement.Update();
	}
}
