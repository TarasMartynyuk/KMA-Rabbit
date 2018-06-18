using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour 
{
	void Start () 
	{
	    //transform.position = new Vector3(-5f, 5f, 0f);
	    var rb = gameObject.AddComponent<Rigidbody2D>();
	    rb.bodyType = RigidbodyType2D.Kinematic;

	    rb.velocity = new Vector2(1f, -1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
