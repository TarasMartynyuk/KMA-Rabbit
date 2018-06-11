using System;
using UnityEngine;

namespace PlayModeTests.Utils
{
    class Collision2DListener : MonoBehaviour
    {
        public event Action<Collision2D> EnterredColission;
        public bool HasCollided { get; private set; }

        void OnCollisionEnter2D(Collision2D col)
        {
            EnterredColission?.Invoke(col);
            HasCollided = true;
        }
    }
}