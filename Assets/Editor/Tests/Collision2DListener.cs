using System;
using UnityEngine;

class Collision2DListener : MonoBehaviour
{
    public event Action EnterredColission;

    void OnCollisionEnter2D(Collision2D col)
    {
        EnterredColission?.Invoke();
    }
}