using UnityEngine;
using UnityEngine.Assertions;
using Utils;

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
        readonly Animator _anim;

        /// <summary>
        /// after _jumpTime seconds has elapsed since the jump start,
        /// user's input (if he holds the jump button) is not count to prolong the jump
        /// </summary>
        readonly float _jumpTime;
        readonly float _jumpYVelocity;
        readonly int _jumpingParamID;

        const float GroundCheckRadius = 0.15f;

        bool _jumping;
        float _jumpTimeCounter;
        bool _onGround;

        public PlayerJump(Rigidbody2D rb, Transform groundCheck, 
            LayerMask whatIsGround, float jumpYVelocity, 
            float jumpTime, Animator anim)
        {
            _groundCheck = groundCheck;
            _whatIsGround = whatIsGround;
            _jumpYVelocity = jumpYVelocity;
            _rb = rb;
            _jumpTime = jumpTime;

            _jumpingParamID = Animator.StringToHash("Jumping");
            AssertAnimator.HasParameter(_jumpingParamID, anim);
            Assert.IsFalse(anim.GetBool(_jumpingParamID));

            _anim = anim;

            ResetJumpCounter();
        }

        public void Update()
        {
            // jumping start
            if(_onGround && Input.GetButtonDown("Jump"))
            {
                StartJumping();
            }
            else if(Input.GetButton("Jump") && _jumping)
            {
                Assert.IsTrue(_jumpTimeCounter > 0);

                _jumpTimeCounter -= Time.deltaTime;
                if(_jumpTimeCounter <= 0)
                    { _jumping = false; }
            } 
            else if(Input.GetButtonUp("Jump"))
            {
                _jumping = false;
            }
        }

        public void FixedUpdate()
        {
            _onGround = CheckIfOnGround();

            _anim.SetBool(_jumpingParamID, ! _onGround);

            if (_jumping)
            {
                _rb.velocity = new Vector2 (_rb.velocity.x, _jumpYVelocity);
            }
        }

        void ResetJumpCounter()
        {
            _jumpTimeCounter = _jumpTime;
        }

        void StartJumping()
        {
            _jumping = true;
            ResetJumpCounter();
        }

        bool CheckIfOnGround()
        {
            var coll = Physics2D.OverlapCircle(_groundCheck.position, GroundCheckRadius, _whatIsGround);

            Assert.IsTrue(coll != null && coll.attachedRigidbody != _rb);

            return coll != null;
        }
    }
}
