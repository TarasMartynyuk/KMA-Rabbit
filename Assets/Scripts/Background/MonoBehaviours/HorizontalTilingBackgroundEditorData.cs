using UnityEngine;
using UnityEngine.Assertions;

namespace Background.MonoBehaviours
{
    public abstract class HorizontalTilingBackgroundEditorData : MonoBehaviour
    {
        [SerializeField]
        protected Camera _camera;
        // the backgrounds must be either a single sprite gameobjects,
        // or a gameobject that serves as agroup for sprites, and has a CompositeSprite component
        [SerializeField]
        protected GameObject _background;
        [SerializeField]
        protected GameObject _backgroundCopy;

        protected HorizontalTilingBackground _tilingBackground;

        protected virtual void Start()
        {
            var spriteMover = WrapInSpriteMover(_background);
            Assert.IsNotNull(spriteMover, $"{nameof(_background)} has neither a CompositeSpriteComponent, nor a SpriteRenderer component");

            var spriteCopyMover = WrapInSpriteMover(_backgroundCopy);
            Assert.IsNotNull(spriteCopyMover, $"{nameof(_backgroundCopy)} has neither a CompositeSpriteComponent, nor a SpriteRenderer component");

            _tilingBackground = new HorizontalTilingBackground(
                _camera, spriteMover, spriteCopyMover);
        }

        /// <summary>
        /// wraps the gameobject in sprite mover,
        /// if it is either a single sprite or prefab with composite sprite component
        /// returns null if it is neither
        /// </summary>
        /// <returns></returns>
        static SpriteMover WrapInSpriteMover(GameObject background)
        {
            var compositeSprite = background.GetComponent<CompositeSprite>();

            if(compositeSprite != null) 
                { return new SpriteMover(compositeSprite); }

            var singleSprite =  background.GetComponent<SpriteRenderer>();
            return singleSprite == null ?
                null : new SpriteMover(singleSprite);
        }
    }
}
