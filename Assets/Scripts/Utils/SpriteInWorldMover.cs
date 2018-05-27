using UnityEngine;
using UnityEngine.Assertions;

namespace Utils
{
    public class SpriteInWorldMover
    {
        public float LeftBounds => _spriteRenderer.bounds.min.x;

        public float RightBounds => _spriteRenderer.bounds.max.x;

        public float TopBounds =>  _spriteRenderer.bounds.min.y;

        public float BotBounds =>  _spriteRenderer.bounds.max.y;

        readonly SpriteRenderer _spriteRenderer;

        /// <summary>
        /// this is the gameobject to which the renderer is attached, or the parent of such gameobject
        /// this gameobject must appear at the center of sprite
        /// it will be moved based on the offsets calculated using sprite
        /// </summary>
        readonly GameObject _spriteParent;
        readonly float _horizExtent;

        public SpriteInWorldMover(SpriteRenderer spriteRenderer, GameObject spriteParent)
        {
            _spriteRenderer = spriteRenderer;
            _spriteParent = spriteParent;
            _horizExtent = (RightBounds - LeftBounds) / 2;

            Assert.IsTrue(_horizExtent > 0);
        }

        /// <summary>
        /// moves the sprite so that it's top left corner has the <paramref name="coords"/> coordinates
        /// </summary>
        public void MoveTopLeftCorner(Vector2 coords)
        {
            Assert.AreEqual(LeftBounds, coords.x);
            Assert.AreEqual(TopBounds, coords.y);
        }
    }
}
