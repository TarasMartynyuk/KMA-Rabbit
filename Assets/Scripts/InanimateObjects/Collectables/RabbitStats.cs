using System;
using Actors;
using UnityEngine;
using UnityEngine.Assertions;

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
        public int DiamondsCollected { get; private set; }

        const float EnlargmentScale = 2f;
        const double TimeEnlarged = 4f;

        double _enlargementTimer;
        readonly Rabbit _rabbit;

        /// <summary>
        /// rabbit ref needed to handle damage on bomb pickup
        /// </summary>
        /// <param name="rabbit"></param>
        public RabbitStats(Rabbit rabbit)
        {
            _rabbit = rabbit;
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
                        { _rabbit.LoseLife(); }
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

                case CollectableType.Diamond:

                    DiamondsCollected++;
                    break;

                default:
                    throw new Exception("unknown Collectable type");
            }
            return true;
        }

        public void Update(double deltaTime)
        {
            _enlargementTimer -= deltaTime;
            if(_enlargementTimer <= 0)
                { StopRabbitsEnlargement(); }
        }

        void EnlargeRabbit()
        {
            Assert.IsFalse(Enlarged);
            _rabbit.gameObject.transform.localScale = Vector3.one * EnlargmentScale;
            _enlargementTimer = TimeEnlarged;
            Enlarged = true;
        }

        void StopRabbitsEnlargement()
        {
            Assert.IsTrue(Enlarged);
            _rabbit.gameObject.transform.localScale = Vector3.one;
            Enlarged = false;
        }
    }
}
