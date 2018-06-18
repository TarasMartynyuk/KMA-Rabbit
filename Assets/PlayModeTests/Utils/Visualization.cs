using UnityEngine;
using UnityEngine.UI;
using static Utils.VectorUtils;

namespace PlayModeTests.Utils
{
        public static class Visualization
    {
        const int DefaultCameraSize = 5;
        static readonly Vector2 GameObjectTextSize = new Vector2(1f, 1f);

        /// <summary>
        /// camera is orthographic,
        /// default position is (0, 0, -10)
        /// </summary>
        public static Camera AddCameraToScene(Vector3 cameraPos)
        {
            var cameraGo = new GameObject("camera");
            var camera = cameraGo.AddComponent<Camera>();

            camera.orthographic = true;

            cameraGo.transform.position = cameraPos;
            return camera;
        }

        public static Camera AddCameraToScene()
        {
            return AddCameraToScene(new Vector3(0f, 0f, -10f));
        }

        public static void AddLabelToGameObject(GameObject gameObject, string labelName = null)
        {
            AddLabelToGameObject(gameObject, GameObjectTextSize, labelName);
        }

        public static void AddLabelToGameObject(GameObject gameObject,Vector2 labelSize, string labelName = null)
        {
            var canvasGo = new GameObject("canvas");
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;

            var canvasRectTransform = canvas.GetComponent<RectTransform>();
            canvasRectTransform.sizeDelta = labelSize;

            var textGo = AddLabel(labelName ?? gameObject.name);

            textGo.transform.SetParent(canvas.transform);
            var rectTransform = textGo.GetComponent<RectTransform>();

            Debug.Break();

            // span image
        }


        #region text creation
        static GameObject AddLabel(string name)
        {
            var textGo = new GameObject("text");

            var text = textGo.AddComponent<Text>();
            text.text = name;
            text.alignment = TextAnchor.MiddleCenter;

            SetRectSizeKeepingWorldSize(
                text, 
                new Vector3(200f, 200f, 0f), 
                AddZCoord(GameObjectTextSize, 1f));

            SetArialFont(text);
            SetFontSizeToMaxThatFitsText(text);

            return textGo;
        }

        static void SetRectSizeKeepingWorldSize(Text text, Vector2 rectSize, Vector2 worldSize)
        {
            text.rectTransform.sizeDelta = rectSize;
            text.rectTransform.localScale = ElementwiseDivision(
                AddZCoord(worldSize, 1f), 
                AddZCoord(rectSize, 1f));
        }

        static void SetArialFont(Text text)
        {
            var arial = (Font) Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            text.font = arial;
        }

        static void SetFontSizeToMaxThatFitsText(Text text)
        {
            text.resizeTextMinSize = 1;
            text.resizeTextMaxSize = 300;
            text.resizeTextForBestFit = true;
        }
        #endregion
    }
}
