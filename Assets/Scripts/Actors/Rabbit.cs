using InanimateObjects.Collectables;
using PlayerControl;
using UnityEngine;

namespace Actors
{
    public class Rabbit : MonoBehaviour 
    {
        public LivesComponent Lives {get; private set;}

        [SerializeField] 
        int _startingLives;

        RabbitStats _stats;
        Animator _anim;

        #region monobehaviour
        void Awake()
        {
            Lives = new LivesComponent(_startingLives);
        }

        void Start()
        {
            var playerController = GetComponent<PlayerController>();

            if(playerController == null)
            { throw new MissingComponentException(
                "the Rabbit monobehaviour requires that it's parent gameobject has a PlayerController component");  }

            _stats = new RabbitStats(this, playerController.PlayerMovement);
        }

        void Update()
        {
            _stats.Update(Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if(_stats.TryPickup(otherCollider.gameObject))
                { return; }
        }
        #endregion monobehaviour

    }
}
