using UnityEngine;
using static UnityEngine.Debug;

namespace PlayerControl
{
    public class PlayerJump
    {
        readonly Transform _groundCheck;
        readonly LayerMask _whatIsGround;
        readonly Rigidbody2D _rb;
        /// <summary>
        /// how long do we respond to the users jump input
        /// </summary>
        readonly float _jumpTime;
        readonly float _jumpYVelocity;

        const float GroundCheckRadius = 0.1f;

        bool _jumping;
        float _jumpTimeCounter;
        bool _grounded;

        public PlayerJump(Rigidbody2D rb, Transform groundCheck, 
            LayerMask whatIsGround, float jumpYVelocity, 
            float jumpTime)
        {
            _groundCheck = groundCheck;
            _whatIsGround = whatIsGround;
            _jumpYVelocity = jumpYVelocity;
            _rb = rb;
            _jumpTime = jumpTime;

            ResetJumpCounter();
        }

        public void Update()
        {
            // jumping start
            if(_grounded && Input.GetButtonDown("Jump"))
            {
                _jumping = true;
                ResetJumpCounter();
                Log("started jumping");

            }
            else if(Input.GetButton("Jump") && _jumping)
            {
                _jumpTimeCounter -= Time.deltaTime;
                Log($"counter: {_jumpTimeCounter}");
                if(_jumpTimeCounter <= 0)
                    { _jumping = false;  Log("finished jumping due to timer"); }

            } 
            else if(Input.GetButtonUp("Jump"))
            {
                Log("finished jumping");
                _jumping = false;

            }
        }
 
        public void FixedUpdate()
        {
            _grounded = Physics2D.OverlapCircle(_groundCheck.position, GroundCheckRadius, _whatIsGround);

            if(_jumping)
            {
                _rb.velocity = new Vector2 (_rb.velocity.x, _jumpYVelocity);
                //_jumping = false;
            }
 
            //if(Input.GetButton("Jump") && _jumpTimeCounter > 0)
            //{
            //    _rb.velocity = new Vector2 (_rb.velocity.x, _jumpYVelocity);
            //    _jumpTimeCounter -= Time.deltaTime;
            //}
        }

        void ResetJumpCounter()
        {
            _jumpTimeCounter = _jumpTime;
        }
    }
}
