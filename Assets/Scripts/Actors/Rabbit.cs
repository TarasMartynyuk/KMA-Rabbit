using GameFlow;
using InanimateObjects.Collectables;
using PlayerControl;
using UnityEngine;
using UnityEngine.Assertions;

namespace Actors
{
    public class Rabbit : MonoBehaviour 
    {
        public LivesComponent Lives {get; private set;}

        [SerializeField] 
        int _startingLives;
        [SerializeField] 
        Respawner _respawner;

        RabbitStats _stats;

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

            var anim = GetComponent<Animator>();
            Assert.IsNotNull(anim);

            _stats = new RabbitStats(this, playerController.PlayerMovement, _respawner, anim);
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
