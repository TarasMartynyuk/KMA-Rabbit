using GameFlow;
using InanimateObjects.Collectables;
using PlayerControl;
using UnityEngine;
using UnityEngine.Assertions;

namespace Actors
{
    public class Rabbit 
    {
        public LivesComponent Lives {get; }

        RabbitStats _stats;

        /// <summary>
        /// for initializing from editor
        /// </summary>
        public Rabbit(GameObject rabbitGo, int startingLives, PlayerMovement movement, 
            Respawner respawner, Animator anim)
            : this(new LivesComponent(startingLives), 
                new RabbitStats(rabbitGo, movement, respawner, anim))
        {}

        /// <summary>
        /// mock constructor for tests
        /// </summary>
        public Rabbit(LivesComponent lives, RabbitStats stats)
        {
            Lives = lives;
            _stats = stats;
        }

        public void Update(double deltaTime)
        {
            _stats.Update(deltaTime);
        }

        public void ManageTriggerEnter2D(Collider2D otherCollider)
        {
            if(_stats.TryPickup(otherCollider.gameObject))
                { return; }
        }
    }
}
