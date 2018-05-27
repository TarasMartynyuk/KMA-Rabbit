using UnityEngine;

class StaticHorizontalTilingBackground : MonoBehaviour
{
    [SerializeField]
    Camera _camera;
    [SerializeField]
    CompositeBackground _background;
    [SerializeField]
    CompositeBackground _backgroundCopy;

    HorizontalTilingBackground _tilingBackground;

    void Awake()
    {
        _tilingBackground = new HorizontalTilingBackground(
            _camera, _background, _backgroundCopy);
    }

    void Update()
    {
        _tilingBackground.Update();
    }
}