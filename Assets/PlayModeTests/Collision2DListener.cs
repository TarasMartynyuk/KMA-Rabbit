using System;
using UnityEngine;

class Collision2DListener : MonoBehaviour
{
    public event Action<Collision2D> EnterredColission;

    void OnCollisionEnter2D(Collision2D col)
    {
        EnterredColission?.Invoke(col);
    }
}