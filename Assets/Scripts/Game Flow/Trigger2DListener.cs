using System;
using UnityEngine;

namespace Game_Flow
{
    /// <summary>
    /// Listens to the unity collision message on the gameobject it is attached to,
    /// and fires a C# event
    /// </summary>
    /// <remarks>
    /// this way, any other C# class can subscribe and handle collisions of this gameobject
    /// </remarks>
    public class Trigger2DListener : MonoBehaviour 
    {
        public event Action<Collider2D> EnterredTrigger;

        void OnTriggerEnter2D(Collider2D other)
        {
            EnterredTrigger?.Invoke(other);
        }
    }
}
