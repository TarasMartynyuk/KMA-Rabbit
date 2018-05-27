using UnityEngine;

/// <summary>
/// script for the parent of multiple sprite gameobjects, grouped and aligned to create a composite sprite
/// </summary>
public class CompositeSprite : MonoBehaviour 
{
    /// <summary>
    /// there must be a sprite that contains all others 
    /// and serves as a bounding box
    /// </summary>
    public SpriteRenderer BoundingSprite { get; private set; }

    [SerializeField]
    SpriteRenderer _boundingSprite;

    void Awake()
    {
        BoundingSprite = _boundingSprite;
    }
}
