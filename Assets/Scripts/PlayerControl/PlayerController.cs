using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    // per second
    [SerializeField] 
    float _speed;

    PlayerMovement _playerMovement;

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
        _playerMovement = new PlayerMovement(rb, transform, _speed);

    }

    void UpdateComponents()
    {
        _playerMovement.Update();
    }

    void FixUpdateComponents()
    {
        _playerMovement.FixedUpdate();
    }
    #endregion
}
