using UnityEngine;

namespace PlayerControl
{
    public class PlayerController : MonoBehaviour 
    {
        public PlayerMovement PlayerMovement { get; private set; }

        // per second
        [SerializeField] 
        float _speed;

        [SerializeField]
        Transform _groundCheck;
        [SerializeField]
        LayerMask _whatIsGround;
        [SerializeField]
        float _jumpTime;
        [SerializeField]
        float _jumpForceMagnitude;

        PlayerJump _playerJump;

        #region Unity methods
        void Start () 
        {
            InitComponents();
        }
	
        void Update () 
        {
            UpdateComponents();
        }

        void FixedUpdate()
        {
            FixUpdateComponents();
        }
        #endregion

        #region helpers
        void InitComponents()
        {
            var rb = GetComponent<Rigidbody2D>();
            var anim = GetComponent<Animator>();

            PlayerMovement = new PlayerMovement(rb, transform, _speed, anim);
            _playerJump = new PlayerJump(
                rb, _groundCheck, _whatIsGround, 
                _jumpForceMagnitude, _jumpTime, anim);
        }

        void UpdateComponents()
        {
            PlayerMovement.Update();
            _playerJump.Update();
        }

        void FixUpdateComponents()
        {
            PlayerMovement.FixedUpdate();
            _playerJump.FixedUpdate();
        }
        #endregion
    }
}
