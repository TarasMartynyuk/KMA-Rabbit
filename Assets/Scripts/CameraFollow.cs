using System;
using UnityEngine;
using Utils;
//using static System.Diagnostics.Debug;
using static UnityEngine.Debug;

public class CameraFollow : MonoBehaviour 
{
    /// <summary>
    /// from the left
    /// </summary>
	[SerializeField]
	float _xOffset;
    /// <summary>
    /// from the bottom
    /// </summary>
	[SerializeField]
	float _yOffset;
	[SerializeField]
	Camera _mainCamera;

	Vector2 _screenDims;
	Vector3 _worldPointOffset;

	void Start () 
	{
	    AssertEditorVarsValid();
		
		_screenDims = new CameraWorlCoordsMover(_mainCamera).GetScreenDimsInWorldCoords();
        _worldPointOffset = GetCenterOffsetInWorldCoords();
	}
	
	void Update () 
	{
		// gos position's z is 0, but camera's must be -10 (or just negative)
		var goPlaneCameraPosition = transform.position + _worldPointOffset;
		goPlaneCameraPosition.z = _mainCamera.transform.position.z;
		_mainCamera.transform.position = goPlaneCameraPosition;
	}

	Vector3 GetCenterOffsetInWorldCoords()
	{
        float centerOffsetX = _xOffset - 0.5f;
	    float centerOffsetY = _yOffset - 0.5f;

        return - ElementviseMult(new Vector2(centerOffsetX, centerOffsetY), _screenDims);
	}

    void AssertEditorVarsValid()
    {
        Assert(_xOffset >= 0 && _xOffset <= 1, "X Offset must be in range [0, 1] - it is in viewport coords");
        Assert(_yOffset >= 0 && _yOffset <= 1, "Y Offset must be in range [0, 1] - it is in viewport coords");
    }

    static Vector3 ElementviseMult(Vector3 left, Vector3 right)
    {
        return new Vector3(
            left.x * right.x,
            left.y * right.y,
            left.z * right.z);
    }
}
