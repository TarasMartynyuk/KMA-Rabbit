using UnityEngine;

namespace Utils
{
    public static class VectorUtils
    {
        public static Vector3 ElementviseMult(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.x * right.x,
                left.y * right.y,
                left.z * right.z);
        }

        public static Vector2 ElementwiseAdd(Vector2 left, float right)
        {
            return new Vector2(
                left.x + right,
                left.y + right);
        }
    }
}
