using System;
using UnityEngine;
using Utils;
using UnityEngine.Assertions;

public class HorizontalParalaxBackground : MonoBehaviour 
{
    [SerializeField]
    Camera _camera;
    [SerializeField]
    CompositeBackground _background;
    [SerializeField]
    CompositeBackground _backgroundCopy;

    Vector3 CameraPosition => _camera.transform.position;

    const float BgChangeTolerance = 0f;

    CameraWorldCoordsWrapper _cameraWorldWrapper;
    SpriteInWorldMover _bgMover;
    SpriteInWorldMover _copyBgMover;

    void Start()
    {
        AssertBackgroundsDimsAreEqual();
        _cameraWorldWrapper = new CameraWorldCoordsWrapper(_camera);

        AssertBackgroundLargerThanScreen();



        _bgMover = new SpriteInWorldMover(_background.BoundingSprite, _background.gameObject);
        _copyBgMover = new SpriteInWorldMover(_backgroundCopy.BoundingSprite, _backgroundCopy.gameObject);


        Debug.Log($"TopLeft {_bgMover.TopLeft}");
        Debug.Log($"TopRight {_bgMover.TopRight}");
        Debug.Log($"BotRight {_bgMover.BotRight}");
        Debug.Log($"BotLeft {_bgMover.BotLeft}");


        Debug.Log($"camera dims {_cameraWorldWrapper.GetScreenDimsInWorldCoords()}");

        Debug.Log($"camera right bound {_cameraWorldWrapper.RightBound + BgChangeTolerance}");
        Debug.Log($"current bg right bound {_bgMover.RightBound}");


        // place the one copy of bg at the center of the camera, the other tiled to the left
        _bgMover.MoveCenter(CameraPosition);
        TileSpriteToTheLeft(_bgMover, _copyBgMover);
    }

    void Update()
    {
        TileAuxBackgroundIfCameraOutOfBounds();
    }

    void TileAuxBackgroundIfCameraOutOfBounds()
    {
        // get currently shown background
        var currentlyShownBg = _bgMover.Contains(CameraPosition) ?
            _bgMover : _copyBgMover;

        var otherBg = GetOtherBackground(currentlyShownBg);


        Debug.Log($"BG LB: {currentlyShownBg.LeftBound}, CAM LB: {_cameraWorldWrapper.LeftBound}," + 
                  $" OUT? : {CameraOutOfLeftBound(currentlyShownBg)}");

        if (CameraOutOfRightBound(currentlyShownBg) && otherBg.Center.x < currentlyShownBg.Center.x)
        {
            Debug.Log("tiled to right");
            TileSpriteToTheRight(currentlyShownBg, otherBg);
        }
        else if (CameraOutOfLeftBound(currentlyShownBg) && otherBg.Center.x > currentlyShownBg.Center.x)
        {
            TileSpriteToTheLeft(currentlyShownBg, otherBg);
        }
    }

    /// <summary>
    /// tiles one sprite to the right of another:
    /// places the <paramref name="movingSprite"/> so that it's top left corner 
    /// is at the <paramref name="staticSprite"/>'s top right corner, 
    /// making a seemless connection
    /// </summary>
    static void TileSpriteToTheRight(SpriteInWorldMover staticSprite, SpriteInWorldMover movingSprite)
    {
        movingSprite.MoveTopLeftCorner(staticSprite.TopRight);
    }

    /// <summary>
    /// tiles one sprite to the left of another:
    /// places the <paramref name="movingSprite"/> so that it's top right corner 
    /// is at the <paramref name="staticSprite"/>'s top left corner, 
    /// making a seemless connection
    /// </summary>
    static void TileSpriteToTheLeft(SpriteInWorldMover staticSprite, SpriteInWorldMover movingSprite)
    {
        movingSprite.MoveTopRightCorner(staticSprite.TopLeft);
    }

    bool CameraOutOfRightBound(SpriteInWorldMover sprite)
    {
        return _cameraWorldWrapper.RightBound + BgChangeTolerance >= sprite.RightBound;
    }

    bool CameraOutOfLeftBound(SpriteInWorldMover sprite)
    {
        return _cameraWorldWrapper.LeftBound - BgChangeTolerance <= sprite.LeftBound;
    }

    SpriteInWorldMover GetOtherBackground(SpriteInWorldMover thisSprite)
    {
        return thisSprite == _bgMover ?
            _copyBgMover : _bgMover;
    }

    #region assert
    void AssertBackgroundsDimsAreEqual()
    {
        Assert.AreEqual(_background.BoundingSprite.bounds.size, _backgroundCopy.BoundingSprite.bounds.size);
    }

    void AssertBackgroundLargerThanScreen()
    {
        var cameraArea =  _cameraWorldWrapper.GetScreenDimsInWorldCoords().magnitude;
        var backgroundDims = _background.BoundingSprite.bounds.size;
        backgroundDims.z = 0;

        Assert.IsTrue(backgroundDims.magnitude > cameraArea);
    }
    #endregion
}
