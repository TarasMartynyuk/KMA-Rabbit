using System;
using System.Collections;
using Actors;
using GameFlow;
using PlayerControl;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;
using Utils;

namespace InanimateObjects.Collectables
{
    /// <summary>
    /// handles picking up collectables 
    /// and manages the rabbits state relevant to collectables
    /// </summary>
    public class RabbitStats : IRabbitStats
    {
        public bool Enlarged { get; private set; }
        public int FruitsCollected { get; private set; }
        public int GemsCollected { get; private set; }

        const float EnlargmentFactor = 1.6f;
        const double TimeEnlarged = 4f;

        readonly GameObject _rabbit;
        readonly PlayerMovement _playerMovement;
        readonly Respawner _respawner;
        readonly Animator _anim;
        readonly LivesComponent _rabbitLives;

        readonly int _dieAnimHash;
        readonly float _rabbitColliderVertExtent;

        double _enlargementTimer;

        /// <summary>
        /// rabbit ref needed to handle damage on bomb pickup
        /// </summary>
        /// <param name="rabbit"></param>
        public RabbitStats(GameObject rabbit, PlayerMovement playerMovement, 
            Respawner respawner, Animator anim)
        {
            _rabbit = rabbit;
            _playerMovement = playerMovement;
            _respawner = respawner;
            _anim = anim;

            _dieAnimHash = Animator.StringToHash("Die");

            var coll = _rabbit.GetComponent<Collider2D>();

            if (coll == null)
                { throw new MissingComponentException($"{nameof(rabbit)} must have a Collider2D attached"); }

            _rabbitColliderVertExtent = coll.bounds.extents.y;
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
                        if(_rabbit.GetComponent<Rabbit>().Lives.LoseLife())
                        { CoroutineStarter.Instance.StartCoroutine(
                            PlayDeathAnimAndRespawnRabbitCoroutine()); }
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
            Assert.AreApproximatelyEqual(Math.Abs(_rabbit.transform.localScale.x), 1f, 0.1f);
            Assert.AreApproximatelyEqual(
                _rabbit.GetComponent<Collider2D>().bounds.extents.y,
                _rabbitColliderVertExtent, 0.1f);

            _rabbit.transform.Translate(Vector3.up * _rabbitColliderVertExtent);

            _rabbit.transform.localScale = _rabbit.transform.localScale * EnlargmentFactor;
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

        IEnumerator PlayDeathAnimAndRespawnRabbitCoroutine()
        {
            _anim.Play(_dieAnimHash);
            // wait for changes in animator made during this frame
            yield return new WaitForEndOfFrame();

            Assert.AreEqual(_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name, "Die");
            float animLength = _anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            yield return new WaitForSeconds(animLength);

            _respawner.RespawnRabbit();
        }
    }
}
