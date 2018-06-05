using InanimateObjects.Collectables;
using UnityEngine;

namespace Actors
{
    public class Rabbit : MonoBehaviour 
    {
        [SerializeField] 
        int _startingLives;

        public int Lives => _livesComponent.Lives;

        LivesComponent _livesComponent;
        RabbitStats _stats;
        Animator _anim;

        #region monobehaviour
        void Awake()
        {
            _livesComponent = new LivesComponent(_startingLives);
            _stats = new RabbitStats(this);
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

        #region delegating livescomponent
        public bool LoseLife()
        {
            //bool died = _livesComponent.LoseLife();
            //if (died &&)
            //{
            //    // if grounded play death anim
            //}
            return _livesComponent.LoseLife();
        }

        public void ResetLives()
        {
            _livesComponent.ResetLives();
        }
        #endregion

    }
}
