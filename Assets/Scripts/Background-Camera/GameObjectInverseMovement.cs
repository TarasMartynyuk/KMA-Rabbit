using UnityEngine;

public class GameObjectInverseMovement 
{
    /// <summary>
    /// with respect to camera
    /// </summary>
    readonly float _speed;
    readonly GameObject _camera;

    public GameObjectInverseMovement(float speed, GameObject camera)
    {
        _speed = speed;
        _camera = camera;
    }

    public void Update()
    {

    }
}
