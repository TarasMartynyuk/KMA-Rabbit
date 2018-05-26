using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class NewBehaviourScript : MonoBehaviour 
{
    [SerializeField] 
    int _lives;

    public int Lives
    {
        get { return _lives; }
        private set { _lives = value; }
    }

    public void LoseLife()
    {
        if(Lives == 1) 
            { Log("rabbit died"); }

        Lives--;
    }
}
