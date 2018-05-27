using UnityEngine;

namespace Utils
{
    public class SpriteInWorldMover
    {
        public float LeftBounds => _spriteRenderer.bounds.min.x;

        public float RightBounds => _spriteRenderer.bounds.max.x;

        readonly SpriteRenderer _spriteRenderer;
        /// <summary>
        /// this is the gameobject to which the renderer is attached, or the parent of such gameobject
        /// this gameobject must appear at the center of sprite
        /// it will be moved based on the offsets calculated using sprite
        /// </summary>
        readonly GameObject _spriteParent;

        public SpriteInWorldMover(SpriteRenderer spriteRenderer, GameObject spriteParent)
        {
            _spriteRenderer = spriteRenderer;
            _spriteParent = spriteParent;

        }

        /// <summary>
        /// moves the sprite so that it's top left corner has the <paramref name="coords"/> coordinates
        /// </summary>
        public void MoveTopLeftCorner(Vector2 coords)
        {

        }
    }
}
