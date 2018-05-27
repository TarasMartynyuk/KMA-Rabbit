using UnityEngine;
using Utils;

/// <summary>
/// moves one gameobject as other gamobject moves,
/// applying the coefficient to it's displacement
/// </summary>
public class GameObjectMovementDependency 
{
    /// <summary>
    /// with respect to camera
    /// </summary>
    readonly float _ratio;
    readonly GameObject _dependentObject;
    readonly GameObject _freeObject;

    Vector3 _pastFramePos;

    public GameObjectMovementDependency(GameObject freeObject, GameObject dependentObject, float ratio)
    {
        _freeObject = freeObject;
        _dependentObject = dependentObject;
        _ratio = ratio;

        _pastFramePos = freeObject.transform.position;
    }

    public void Update()
    {
        var currPos = _freeObject.transform.position;
        var displacement = currPos - _pastFramePos;

        var dependentDisplacement = displacement *_ratio;
        _dependentObject.transform.Translate(dependentDisplacement);

        _pastFramePos = currPos;
    }
}
