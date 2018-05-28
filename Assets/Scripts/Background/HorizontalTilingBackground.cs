using System;
using UnityEngine;

namespace Background
{
    public class HorizontalTilingBackground 
    {
        const float BgChangeTolerance = 0f;

        readonly CameraWorldWrapper _cameraWorldWrapper;
        readonly SpriteMover _bgMover;
        readonly SpriteMover _copyBgMover;

        public HorizontalTilingBackground(Camera camera, SpriteMover spriteMover, SpriteMover spriteCopyMover)
        {
            if(! SpriteMoverDimsEqual(spriteMover, spriteCopyMover))
                { throw new ArgumentException("backgrounds must have equal sizes"); }

            _cameraWorldWrapper = new CameraWorldWrapper(camera);

            if(! BackgroundLargerThanScreen(spriteMover, _cameraWorldWrapper)) 
                { throw new ArgumentException("background must be larger than screen (in world units)"); }

            _bgMover = spriteMover;
            _copyBgMover = spriteCopyMover;

            // place the one copy of bg at the center of the camera, the other tiled to the left
            _bgMover.MoveCenter(_cameraWorldWrapper.Position);
            TileSpriteToTheLeft(_bgMover, _copyBgMover);
        }

        public void Update()
        {
            TileAuxBackgroundIfCameraOutOfBounds();
        }

        void TileAuxBackgroundIfCameraOutOfBounds()
        {
            // get currently shown background
            var currentlyShownBg = _bgMover.Contains(_cameraWorldWrapper.Position) ?
                _bgMover : _copyBgMover;

            var otherBg = GetOtherBackground(currentlyShownBg);

            //Debug.Log($"BG LB: {currentlyShownBg.LeftBound}, CAM LB: {_cameraWorldWrapper.LeftBound}," + 
            //          $" OUT? : {CameraOutOfLeftBound(currentlyShownBg)}");

            if (CameraOutOfRightBound(currentlyShownBg) && otherBg.Center.x < currentlyShownBg.Center.x)
            {
                Debug.Log("tiled to right");
                TileSpriteToTheRight(currentlyShownBg, otherBg);
            }
            else if (CameraOutOfLeftBound(currentlyShownBg) && otherBg.Center.x > currentlyShownBg.Center.x)
            {
                Debug.Log("tiled to left");
                TileSpriteToTheLeft(currentlyShownBg, otherBg);
            }
        }

        /// <summary>
        /// tiles one sprite to the right of another:
        /// places the <paramref name="movingSprite"/> so that it's top left corner 
        /// is at the <paramref name="staticSprite"/>'s top right corner, 
        /// making a seemless connection
        /// </summary>
        static void TileSpriteToTheRight(SpriteMover staticSprite, SpriteMover movingSprite)
        {
            movingSprite.MoveTopLeftCorner(staticSprite.TopRight);
        }

        /// <summary>
        /// tiles one sprite to the left of another:
        /// places the <paramref name="movingSprite"/> so that it's top right corner 
        /// is at the <paramref name="staticSprite"/>'s top left corner, 
        /// making a seemless connection
        /// </summary>
        static void TileSpriteToTheLeft(SpriteMover staticSprite, SpriteMover movingSprite)
        {
            movingSprite.MoveTopRightCorner(staticSprite.TopLeft);
        }

        bool CameraOutOfRightBound(SpriteMover sprite)
        {
            return _cameraWorldWrapper.RightBound + BgChangeTolerance >= sprite.RightBound;
        }

        bool CameraOutOfLeftBound(SpriteMover sprite)
        {
            return _cameraWorldWrapper.LeftBound - BgChangeTolerance <= sprite.LeftBound;
        }

        SpriteMover GetOtherBackground(SpriteMover thisSprite)
        {
            return thisSprite == _bgMover ?
                _copyBgMover : _bgMover;
        }

        #region validation
        static bool SpriteMoverDimsEqual(SpriteMover mover, SpriteMover otherMover)
        {
            return Math.Abs(
                (mover.Dimentions - otherMover.Dimentions).magnitude) <0.02;
        }

        static bool BackgroundLargerThanScreen(SpriteMover mover, CameraWorldWrapper camera)
        {
            float cameraArea =  camera.GetScreenDimsInWorldCoords().magnitude;
            var backgroundDims = mover.Dimentions;

            return backgroundDims.magnitude > cameraArea;
        }
        #endregion
    }
}
