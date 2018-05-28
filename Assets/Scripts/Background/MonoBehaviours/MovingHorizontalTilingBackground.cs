using UnityEngine;

namespace Background.MonoBehaviours
{
    public class MovingHorizontalTilingBackground : HorizontalTilingBackgroundEditorData
    {
        [SerializeField]
        float _scrollingSpeed;

        GameObjectMovementDependency _backgroundMoveDependency;
        GameObjectMovementDependency _copyBackgroundMoveDependency;

        protected override void Start()
        {
            base.Start();
            _backgroundMoveDependency = new GameObjectMovementDependency(
                _camera.gameObject, _background, 1f / _scrollingSpeed);

            _copyBackgroundMoveDependency = new GameObjectMovementDependency(
                _camera.gameObject, _backgroundCopy, 1f / _scrollingSpeed);
        }

        void Update()
        {
            _tilingBackground.Update();
        }

        void LateUpdate()
        {
            _backgroundMoveDependency.LateUpdate();
            _copyBackgroundMoveDependency.LateUpdate();
        }
    }
}
