using UnityEngine;

namespace Utils
{
    public static class VectorUtils
    {
        public static Vector3 AddZCoord(Vector2 vec, float z)
        {
            return new Vector3(vec.x, vec.y, z);
        }

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


        public static Vector3 ScalarDivision(float scalar, Vector3 vec)
        {
            return new Vector3(
                scalar / vec.x,
                scalar / vec.y,
                scalar / vec.z);
        }

        public static Vector3 ElementwiseDivision(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.x / right.x,
                left.y / right.y,
                left.z / right.z);
        }

        public static Vector3 Direction(Vector3 point, Vector3 target)
        {
            return (target - point).normalized;
        }

        public static bool ApproximatelyEqual(Vector3 vec, Vector3 otherVec, float tolerance)
        {
            return Vector3.Distance(vec, otherVec) < tolerance;
        }
    }
}
