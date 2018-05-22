﻿using UnityEngine;

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
    // delegated to serializefield fields
    //LayerMask whatIsGround;
    //Transform groundCheck;
    readonly float _speed;

    readonly Rigidbody2D _rb;
    readonly Transform _transform;

    //const float groundRadius = 0.2f;
    //const float JumpForceMag = 10f;
    //const float JumpTime = 0.1f;

    bool _facingRight = true;
    //bool grounded;
    //float jumpTimeCounter;

    public PlayerMovement(Rigidbody2D rb, Transform transform, float speed)
    {
        _rb = rb;
        _transform = transform;
        _speed = speed;
    }

    public void Update()
    {
        //JumpFunction();
    }

    //void JumpFunction()
    //{
    //    //starting jump
    //    if(Input.GetButtonDown("Jump"))
    //    {
    //        if(grounded)
    //        {
    //            _rb.velocity = new Vector2(_rb.velocity.x, JumpForceMag / 2);
    //        }
    //    }

    //    //long jump while time counter >0
    //    if(Input.GetButton("Jump"))
    //    {
    //        if(jumpTimeCounter > 0)
    //        {
    //            _rb.velocity = new Vector2(_rb.velocity.x, JumpForceMag);
    //            jumpTimeCounter -= Time.deltaTime;
    //        }
    //    }

    //    //ending jump
    //    if(Input.GetButtonUp("Jump"))
    //    {
    //        jumpTimeCounter = 0;
    //    }

    //    //reseting timer
    //    if(grounded)
    //    {
    //        jumpTimeCounter = JumpTime;
    //    }
    //}

    // Update is called once per frame
    public void FixedUpdate()
    {
        //grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        float move = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(move * _speed, _rb.velocity.y);

        if(_facingRight && move < 0 ||
           ! _facingRight && move > 0)
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