using System;
using Background.MonoBehaviours;
using UnityEngine;
using UnityEngine.Assertions;

namespace Background
{
    /// <summary>
    /// more convenient api for moving sprite in world coords
    /// sprite can be a single sprite gameobject, or a prefab with CompositeSprite component
    /// </summary>
    public class SpriteMover
    {
        #region bounds props
        // actually this props exist just for readability
        public Vector2 BotLeft => _spriteRenderer.bounds.min;
        public Vector2 TopRight => _spriteRenderer.bounds.max;
        public Vector2 TopLeft => new Vector2(BotLeft.x, TopRight.y);
        public Vector2 BotRight => new Vector2(TopRight.x, BotLeft.y);

        public float LeftBound => BotLeft.x;
        public float TopBound =>  TopRight.y;
        public float RightBound => TopRight.x;
        public float BotBound =>  BotLeft.y;

        public float HorizExtent => _spriteRenderer.bounds.extents.x;
        public float VertExtent => _spriteRenderer.bounds.extents.y;

        public Vector2 Dimentions => _spriteRenderer.bounds.size;
        public Vector2 Center => _spriteRenderer.bounds.center;
        #endregion

        readonly SpriteRenderer _spriteRenderer;

        /// <summary>
        /// this is the gameobject to which the renderer is attached, or the parent of such gameobject
        /// this gameobject must appear at the center of sprite
        /// it will be moved based on the offsets calculated using sprite
        /// </summary>
        readonly GameObject _spriteParent;

        #region ctors
        public SpriteMover(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
            _spriteParent = _spriteRenderer.gameObject;
        }

        public SpriteMover(CompositeSprite compositeSprite)
        {
            if(compositeSprite.transform.position != compositeSprite.BoundingSprite.bounds.center)
            {
                throw new ArgumentException($"{nameof(compositeSprite)} " + 
                                            $"must be positioned in the center of {nameof(compositeSprite.BoundingSprite)}");
            }
            _spriteRenderer = compositeSprite.BoundingSprite;
            _spriteParent = compositeSprite.gameObject;
        }
        #endregion

        /// <summary>
        /// moves the sprite so that it's top left corner has the <paramref name="coords"/> coordinates
        /// </summary>
        public void MoveTopLeftCorner(Vector2 coords)
        {
            float spriteAlignedX = coords.x + HorizExtent;
            float spriteAlignedY = coords.y - VertExtent;

            _spriteParent.transform.position = new Vector3(
                spriteAlignedX, spriteAlignedY, _spriteParent.transform.position.z);

            Assert.AreApproximatelyEqual(LeftBound, coords.x, 0.05f);
            Assert.AreApproximatelyEqual(TopBound, coords.y, 0.05f);
        }

        /// <summary>
        /// moves the sprite so that it's top left corner has the <paramref name="coords"/> coordinates
        /// </summary>
        public void MoveTopRightCorner(Vector2 coords)
        {
            var spriteAlignedPos = (Vector3) coords - _spriteRenderer.bounds.extents;
            spriteAlignedPos.z = _spriteParent.transform.position.z;

            _spriteParent.transform.position = spriteAlignedPos;

            Assert.AreApproximatelyEqual(RightBound, coords.x, 0.05f);
            Assert.AreApproximatelyEqual(TopBound, coords.y, 0.05f);
        }

        /// <summary>
        /// moves the sprite so that it's center has the <paramref name="coords"/> coordinates
        /// </summary>
        public void MoveCenter(Vector2 coords)
        {
            _spriteParent.transform.position = coords;
        }

        public bool Contains(Vector3 position)
        {
            position.z = 0;
            return _spriteRenderer.bounds.Contains(position);
        }
    }
}