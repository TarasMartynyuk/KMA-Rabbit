using UnityEngine;
using UnityEngine.Assertions;

class StaticHorizontalTilingBackground : MonoBehaviour
{
    [SerializeField]
    Camera _camera;
    // the backgrounds must be either a single sprite gameobjects,
    // or a gameobject that serves as agroup for sprites, and has a CompositeSprite component
    [SerializeField]
    GameObject _background;
    [SerializeField]
    GameObject _backgroundCopy;

    HorizontalTilingBackground _tilingBackground;

    void Start()
    {
        var sprite = GetBoundingSpriteRenderer(_background);
        Assert.IsNotNull(sprite, $"{nameof(_background)} has neither a CompositeSpriteComponent, nor a SpriteRenderer component");

        var spriteCopy = GetBoundingSpriteRenderer(_backgroundCopy);
        Assert.IsNotNull(spriteCopy, $"{nameof(_backgroundCopy)} has neither a CompositeSpriteComponent, nor a SpriteRenderer component");

        _tilingBackground = new HorizontalTilingBackground(
            _camera, sprite, spriteCopy);
    }

    void Update()
    {
        _tilingBackground.Update();
    }

    /// <summary>
    /// returns the bounding sprite, if argument is a composite sprite GO,
    /// or the sprite rendere of an argument, if it is a single sprite
    /// returns null if it neither
    /// </summary>
    /// <returns></returns>
    static SpriteRenderer GetBoundingSpriteRenderer(GameObject background)
    {
        var compositeSprite = background.GetComponent<CompositeSprite>();

        if(compositeSprite != null) 
            { return compositeSprite.BoundingSprite; }

        return background.GetComponent<SpriteRenderer>();
    }
}