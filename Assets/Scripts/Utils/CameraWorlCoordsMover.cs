using UnityEngine;
using UnityEngine.Assertions;

namespace Utils
{
    class CameraWorlCoordsMover
    {
        readonly Camera _camera;

        public CameraWorlCoordsMover(Camera camera)
        {
            _camera = camera;
        }

        public Vector2 GetScreenDimsInWorldCoords()
        {
            var botLeft = ScreenBotLeft(_camera);
            var topRight = ScreenTopRight(_camera);

            var dims = topRight - botLeft;
            Assert.IsTrue(dims.x > 0f && dims.y > 0f);

            // why this line below returns the vector which is half of the dims?
            //var k = new Vector3(Screen.width, Screen.height, 10);

            // return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
            return dims;
        }

        static Vector2 ScreenTopRight(Camera camera)
        {
            return camera.ViewportToWorldPoint(new Vector3(1f, 1f, 10f));
        }

        static Vector2 ScreenBotLeft(Camera camera)
        {
            return camera.ViewportToWorldPoint(new Vector3(0f, 0f, 10f));
        }
    }
}
