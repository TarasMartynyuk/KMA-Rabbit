using UnityEngine;
using static UnityEngine.Debug;

namespace PlayerControl
{
    /// <summary>
    /// pushes rigidbody upwards when user gives us jump input,
    /// prolonging the jump if the user holds the jump button
    /// </summary>
    public class PlayerJump
    {
        readonly Transform _groundCheck;
        readonly LayerMask _whatIsGround;
        readonly Rigidbody2D _rb;
        /// <summary>
        /// after _jumpTime seconds has elapsed since the jump start,
        /// user's input (if he holds the jump button) is not count to prolong the jump
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
