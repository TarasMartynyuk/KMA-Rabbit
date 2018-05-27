using Assets.Scripts.Background_Camera.MonoBehaviours;

class StaticHorizontalTilingBackground : HorizontalTilingBackgroundEditorInputProvider
{
    void Update()
    {
        _tilingBackground.Update();
    }
}