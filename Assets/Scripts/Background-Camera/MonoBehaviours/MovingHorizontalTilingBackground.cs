using UnityEngine;

namespace MonoBehaviours
{
    public class MovingHorizontalTilingBackground : HorizontalTilingBackgroundEditorData
    {
        GameObjectMovementDependency _backgroundMoveDependency;
        GameObjectMovementDependency _copyBackgroundMoveDependency;

        protected override void Start()
        {
            base.Start();
            _backgroundMoveDependency = new GameObjectMovementDependency(
                _camera.gameObject, _background, 0.5f);

            _copyBackgroundMoveDependency = new GameObjectMovementDependency(
                _camera.gameObject, _backgroundCopy, 0.5f);
        }

        void Update()
        {
            _backgroundMoveDependency.Update();
            _copyBackgroundMoveDependency.Update();
            _tilingBackground.Update();
        }
    }
}
