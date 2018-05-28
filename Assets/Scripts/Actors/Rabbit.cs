using UnityEngine;

namespace Actors
{
    public class Rabbit : MonoBehaviour 
    {
        [SerializeField] 
        int _startingLives;

        public int Lives => _livesComponent.Lives;

        LivesComponent _livesComponent;
        Animator _anim;

        void Awake()
        {
            _livesComponent = new LivesComponent(_startingLives);
        }

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
