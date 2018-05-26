using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens to the unity collision message on the gameobject it is attached to,
/// and fires a C# event
/// </summary>
/// <remarks>
/// this way, any other C# class can subscribe and handle collisions of this gameobject
/// the cached members of the class are hardcoded for my particular case
/// </remarks>
public class CollisionListener : MonoBehaviour 
{
    public event Action<Collision> OnEnteredCollision;


    void OnCollisionEnter(Collision collision)
    {
        OnEnteredCollision?.Invoke(collision);
    }
}
