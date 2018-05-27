using System;
using UnityEngine;
using Utils;
using UnityEngine.Assertions;

public class ParalaxBackground : MonoBehaviour 
{
    [SerializeField]
    Camera _camera;
    [SerializeField]
    CompositeBackground _background;
    [SerializeField]
    CompositeBackground _backgroundCopy;

    Vector3 CameraPosition => _camera.transform.position;

    CameraWorldCoordsWrapper _cameraWorldCoordsWrapper;
    SpriteInWorldMover _backgroundMover;
    SpriteInWorldMover _backgroundCopyMover;

    void Start()
    {
        AssertBackgroundsDimsAreEqual();
        _cameraWorldCoordsWrapper = new CameraWorldCoordsWrapper(_camera);

        _backgroundMover = new SpriteInWorldMover(_background.BoundingSprite, _background.gameObject);
        _backgroundCopyMover = new SpriteInWorldMover(_backgroundCopy.BoundingSprite, _backgroundCopy.gameObject);


        Debug.Log($"TopLeft {_backgroundMover.TopLeft}");
        Debug.Log($"TopRight {_backgroundMover.TopRight}");
        Debug.Log($"BotRight {_backgroundMover.BotRight}");
        Debug.Log($"BotLeft {_backgroundMover.BotLeft}");

        // place the one copy of bg at the center of the camera, the other tiled to the left
        _backgroundMover.MoveCenter(CameraPosition);
        TileSpriteToTheLeft(_backgroundCopyMover, _backgroundMover);
    }


    //bool CamereCenterInsideBackground(SpriteWorldMover background)
    //{

    //}


    /// <summary>
    /// tiles one sprite to the right of another:
    /// places the <paramref name="movingSprite"/> so that it's top left corner 
    /// is at the <paramref name="staticSprite"/>'s top right corner, 
    /// making a seemless connection
    /// </summary>
    static void TileSpriteToTheRight(SpriteInWorldMover movingSprite, SpriteInWorldMover staticSprite)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// tiles one sprite to the left of another:
    /// places the <paramref name="movingSprite"/> so that it's top right corner 
    /// is at the <paramref name="staticSprite"/>'s top left corner, 
    /// making a seemless connection
    /// </summary>
    static void TileSpriteToTheLeft(SpriteInWorldMover movingSprite, SpriteInWorldMover staticSprite)
    {
        movingSprite.MoveTopRightCorner(staticSprite.TopLeft);
    }

    void AssertBackgroundsDimsAreEqual()
    {
        Assert.AreEqual(_background.BoundingSprite.bounds.extents, _backgroundCopy.BoundingSprite.bounds.extents);
    }
}
