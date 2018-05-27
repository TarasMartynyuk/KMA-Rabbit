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

    CameraWorldCoordsWrapper _cameraWorldCoordsWrapper;
    SpriteInWorldMover _backgroundMover;
    SpriteInWorldMover _backgroundCopyMover;

    void Awake()
    {
        _cameraWorldCoordsWrapper = new CameraWorldCoordsWrapper(_camera);

    }

    void Start()
    {
        _backgroundMover = new SpriteInWorldMover(_background.BoundingSprite, _background.gameObject);
        _backgroundCopyMover = new SpriteInWorldMover(_backgroundCopy.BoundingSprite, _backgroundCopy.gameObject);

        Debug.Log($"sprite min: {_background.BoundingSprite.bounds.min}");
        Debug.Log($"sprite max: {_background.BoundingSprite.bounds.max}");
        Debug.Log($"sprite dims: {_background.BoundingDimentions}");



        // place the one copy of bg at the center of the camera, the other tiled to the left
    }


    //bool CamereCenterInsideBackground(SpriteWorldMover background)
    //{

    //}


    /// <summary>
    /// tiles one sprite to the right of another:
    /// places the <paramref name="movingSpriteIn"/> so that it's top left corner 
    /// is at the <paramref name="staticSpriteIn"/>'s top right corner, 
    /// making a seemless connection
    /// </summary>
    static void TileSpriteToTheRight(SpriteInWorldMover staticSpriteIn, SpriteInWorldMover movingSpriteIn)
    {

    }

    /// <summary>
    /// tiles one sprite to the left of another:
    /// places the <paramref name="movingSpriteIn"/> so that it's top right corner 
    /// is at the <paramref name="staticSpriteIn"/>'s top left corner, 
    /// making a seemless connection
    /// </summary>
    static void TileSpriteToTheLeft(SpriteInWorldMover staticSpriteIn, SpriteInWorldMover movingSpriteIn)
    {

    }

    void AssertBackgroundsDimsAreEqual()
    {
        //Assert.AreEqual(_background.Boun)
    }
}
