using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class Rabbit : MonoBehaviour 
{
    [SerializeField] 
    int _lives;

    public int Lives
    {
        get { return _lives; }
        private set { _lives = value; }
    }

    /// <summary>
    /// returns true if rabbit died due to inflicted damage
    /// </summary>
    public bool LoseLife()
    {
        if(Lives == 1)
        {
            Log("rabbit died");
        }

        Lives--;
        return Lives <= 0;
    }
}
