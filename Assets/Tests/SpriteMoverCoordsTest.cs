using UnityEngine;
using Utils;
using UnityEngine.Assertions;

public class SpriteMoverCoordsTest : MonoBehaviour 
{
    [SerializeField]
    SpriteRenderer _spriteRenderer;
    [SerializeField]
    GameObject _goToMove;
    [SerializeField]
    Vector2 _spriteWorldDims;

    const float Tolerance = 0.1f;
    SpriteInWorldMover _initMover;
    float _horizExtent;

    void Awake()
    {
        _initMover = new SpriteInWorldMover(_spriteRenderer, _goToMove);
        _horizExtent = _spriteWorldDims.x / 2;

        Debug.Log($"bounds: {_spriteRenderer.bounds}");
        Debug.Log($"sprite bounds: {_spriteRenderer.sprite.bounds}");
        Debug.Log($"sprite rect: {_spriteRenderer.sprite.rect}");

        LeftBound_IsGameObjectMinusExtent();
        RightBound_IsGameObjectPlusExtent();
    }

    void LeftBound_IsGameObjectMinusExtent()
    {
        float expected = _goToMove.transform.position.x - _horizExtent;
        Assert.AreApproximatelyEqual(expected, _initMover.LeftBounds, Tolerance);

        Debug.Log("passed: LeftBound_IsGameObjectMinusExtent");
    }

    void RightBound_IsGameObjectPlusExtent()
    {
        float expected = _goToMove.transform.position.x - _horizExtent;
        Assert.AreApproximatelyEqual(expected, _initMover.LeftBounds, Tolerance);

        Debug.Log("passed: RightBound_IsGameObjectPlusExtent");
    }
}
