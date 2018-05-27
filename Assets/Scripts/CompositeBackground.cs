using UnityEngine;

/// <summary>
/// script for the parent of multiple sprite gameobjects, aligned to create a background
/// </summary>
public class CompositeBackground : MonoBehaviour 
{
    public Vector2 BoundingDimentions { get; private set; }

    public SpriteRenderer BoundingSprite { get; private set; }

    [SerializeField]
    SpriteRenderer _boundingSprite;

    void Awake()
    {
        BoundingSprite = _boundingSprite;

        BoundingDimentions = new Vector2(
            _boundingSprite.sprite.rect.width,
            _boundingSprite.sprite.rect.height);
    }
}
