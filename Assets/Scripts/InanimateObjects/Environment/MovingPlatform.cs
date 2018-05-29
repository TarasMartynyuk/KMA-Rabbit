using UnityEngine;

namespace InanimateObjects.Environment
{
    public class MovingPlatform : MonoBehaviour 
    {
        [SerializeField]
        Transform _pointB;
        [SerializeField]
        float _speed;
        [SerializeField]
        float _pause;

        PendulumMovement _pendulumMovement;
        StickerTrigger _stickerTrigger;

        void Awake() 
        {
            _pendulumMovement = new PendulumMovement(transform, _pointB.position, _speed, _pause);
            _stickerTrigger = new StickerTrigger(gameObject);
        }
	
        void Update () 
        {
            _pendulumMovement.Update();
        }
    }
}
