using UnityEngine;
using Utils;

public class ParalaxBackground : MonoBehaviour 
{
    [SerializeField]
    Camera _camera;
    [SerializeField]
    GameObject _background;
    [SerializeField]
    GameObject _backgroundCopy;

    // half of screen width in world coords
    float _cameraYOffset;

    void Awake()
    {
        _cameraYOffset = CameraUtils.GetScreenDimsInWorldCoords(_camera).x / 2;
    }

    float ScreenRightBoundInWorldCoords()
    {
        return _camera.transform.position.x + _cameraYOffset;
    }

    float ScreenLeftBoundInWorldCoords()
    {
        return _camera.transform.position.x - _cameraYOffset;
    }
}
