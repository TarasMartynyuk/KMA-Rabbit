using UnityEngine;
using UnityEngine.Assertions;
using Utils;

namespace PlayerControl
{
    ///<summary>
    /// handles user right and left movement with turning
    /// </summary>
    /// <remarks>
    /// Resembles the unity's Monobehaviours, but is purely C# 
    /// in order not to constrain you to add it to the gameobject on the scene.
    /// Instead, you should make this class a (in-code) component (member) of your Monobehaviour
    /// and delegate the monobehaviour properties and serialized fields to this class
    /// 
    /// this way that monobehaviour can encapsulate any amount of such C# classes,
    /// and only 1 monobehaviour needs to be attached to the gameobject
    /// 
    /// P.S i just like doing things in code more than doing them in editor
    /// this allowes us to use proper class initialization and readonly accessor
    /// </remarks>
    public class PlayerMovement
    {
        readonly float _speed;

        readonly Rigidbody2D _rb;
        readonly Transform _transform;
        readonly Animator _anim;

        readonly int _runningParamId;
        bool _facingRight = true;
        float _move;

        public PlayerMovement(Rigidbody2D rb, Transform transform, 
            float speed, Animator anim)
        {
            _rb = rb;
            _transform = transform;
            _speed = speed;

            _runningParamId = Animator.StringToHash("Running");

            AssertAnimator.HasParameter(_runningParamId, anim);
            Assert.IsFalse(anim.GetBool(_runningParamId));
            _anim = anim;
        }

        public void Update()
        {
            _move = Input.GetAxis("Horizontal");

            if(Mathf.Abs(_move) > 0.0f) { Debug.Log(_move); }

            _anim.SetBool(_runningParamId, Mathf.Abs(_move) > 0.0f);
        }


        // Update is called once per frame
        public void FixedUpdate()
        {
            _rb.velocity = new Vector2(_move * _speed, _rb.velocity.y);

            if (_facingRight && _move < 0 ||
                ! _facingRight && _move > 0)
            { Turn(); }
        }

        void Turn()
        {
            _facingRight = ! _facingRight;

            var oldScale = _transform.localScale;
            oldScale.x *= -1;

            _transform.localScale = oldScale;
        }
    }
}