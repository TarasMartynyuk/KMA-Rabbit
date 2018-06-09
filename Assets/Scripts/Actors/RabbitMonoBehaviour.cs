using GameFlow;
using PlayerControl;
using UnityEngine;
using UnityEngine.Assertions;

namespace Actors
{
    class RabbitMonoBehaviour : MonoBehaviour
    {
        public Rabbit Rabbit { get; private set; }

        [SerializeField] 
        int _startingLives;
        [SerializeField] 
        Respawner _respawner;

        void Start()
        {
            var playerController = GetComponent<PlayerController>();

            if(playerController == null)
            { throw new MissingComponentException(
                "the Rabbit monobehaviour requires that it's parent gameobject has a PlayerController component");  }

            var anim = GetComponent<Animator>();
            Assert.IsNotNull(anim);

            Rabbit = new Rabbit(gameObject, _startingLives, 
                playerController.PlayerMovement, _respawner, anim);
        }
    }
}
