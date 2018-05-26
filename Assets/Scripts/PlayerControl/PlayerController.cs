using UnityEngine;

namespace PlayerControl
{
    public class PlayerController : MonoBehaviour 
    {
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


        PlayerMovement _playerMovement;
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

            _playerMovement = new PlayerMovement(rb, transform, _speed, anim);
            _playerJump = new PlayerJump(
                rb, _groundCheck, _whatIsGround, 
                _jumpForceMagnitude, _jumpTime, anim);
        }

        void UpdateComponents()
        {
            //_playerMovement.Update();
            _playerJump.Update();
        }

        void FixUpdateComponents()
        {
            //_playerMovement.FixedUpdate();
            _playerJump.FixedUpdate();
        }
        #endregion
    }
}
