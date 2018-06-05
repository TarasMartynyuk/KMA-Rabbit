using System;
using Actors;
using PlayerControl;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace InanimateObjects.Collectables
{
    /// <summary>
    /// handles picking up collectables 
    /// and manages the rabbits state relevant to collectables
    /// </summary>
    class RabbitStats
    {
        public bool Enlarged { get; private set; }
        public int FruitsCollected { get; private set; }
        public int GemsCollected { get; private set; }

        const float EnlargmentFactor = 1.6f;
        const double TimeEnlarged = 4f;

        double _enlargementTimer;
        readonly Rabbit _rabbit;
        readonly PlayerMovement _playerMovement;

        /// <summary>
        /// rabbit ref needed to handle damage on bomb pickup
        /// </summary>
        /// <param name="rabbit"></param>
        public RabbitStats(Rabbit rabbit, PlayerMovement playerMovement)
        {
            _rabbit = rabbit;
            _playerMovement = playerMovement;
        }

        /// <summary>
        /// if <paramref name="gameObject"/> is a collectable,
        /// does the pickup logic and returns true,
        /// else returns false
        /// </summary>
        public bool TryPickup(GameObject gameObject)
        {
            var maybeCollectable = gameObject.GetComponent<Collectable>();

            if(maybeCollectable == null) 
                { return false; }

            switch(maybeCollectable.Type)
            {
                case CollectableType.Bomb:
                    if(Enlarged) 
                        { StopRabbitsEnlargement(); }
                    else
                    {
                        if(_rabbit.Lives.LoseLife())
                        {

                        }
                    }
                    break;

                case CollectableType.Mushroom:

                    if(! Enlarged)
                    { EnlargeRabbit(); }
                    else 
                    { _enlargementTimer += TimeEnlarged; }
                    break;

                case CollectableType.Fruit:

                    FruitsCollected++;
                    break;

                case CollectableType.Gem:

                    GemsCollected++;
                    break;

                default:
                    throw new Exception("unknown Collectable type");
            }

            Object.Destroy(gameObject);
            return true;
        }

        public void Update(double deltaTime)
        {
            if(Enlarged)
            {
                _enlargementTimer -= deltaTime;
                if(_enlargementTimer <= 0)
                    { StopRabbitsEnlargement(); }
            }
        }

        void EnlargeRabbit()
        {
            Assert.IsFalse(Enlarged);
            _rabbit.gameObject.transform.localScale = Vector3.one * EnlargmentFactor;
            _enlargementTimer = TimeEnlarged;
            Enlarged = true;
        }

        void StopRabbitsEnlargement()
        {
            Assert.IsTrue(Enlarged);
            Assert.AreApproximatelyEqual(Math.Abs(_rabbit.transform.localScale.x), EnlargmentFactor, .1f);

            _rabbit.transform.localScale = new Vector3(
                _playerMovement.FacingRight? 1f : -1f,
                1f,
                1f);
            Enlarged = false;
        }
    }
}
