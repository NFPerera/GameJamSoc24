using UnityEngine;

namespace _Main.Scripts.DevelopmentUtilities.Extensions
{
    public static class VectorExtentions
    {
        #region Vector2

        /// <summary> Distance to line that passses through pointA and pointB. </summary>
        public static float Distance(this Vector2 p_v, Vector2 p_a, Vector2 p_b) =>
            Mathf.Abs((p_b.y - p_a.y) * p_v.x - (p_b.x - p_a.x) * p_v.y + p_b.x * p_a.y - p_b.y * p_a.x) /
            Mathf.Sqrt(Mathf.Pow(p_b.y - p_a.y, 2) + Mathf.Pow(p_b.x - p_a.x, 2));

        /// <summary> Square Distance to line that passses through pointA and pointB. </summary>
        public static float SqrDistance(this Vector2 p_v, Vector2 p_a, Vector2 p_b) =>
            Mathf.Pow((p_b.y - p_a.y) * p_v.x - (p_b.x - p_a.x) * p_v.y + p_b.x * p_a.y - p_b.y * p_a.x, 2) /
            (Mathf.Pow(p_b.y - p_a.y, 2) + Mathf.Pow(p_b.x - p_a.x, 2));

        public static Vector3 XY0(this Vector2 p_v) => new(p_v.x, p_v.y, 0);
        public static Vector3 X0Z(this Vector2 p_v) => new(p_v.x, 0, p_v.y);

        #endregion

        #region Vector3

        /// <summary> Vector2 (x, y) </summary>
        public static Vector2 XY(this Vector3 p_v) => new Vector2(p_v.x, p_v.y);

        /// <summary> Vector2 (x, z) </summary>
        public static Vector2 XZ(this Vector3 p_v) => new Vector2(p_v.x, p_v.z);

        /// <summary> Vector2 (y, z) </summary>
        public static Vector2 YZ(this Vector3 p_v) => new Vector2(p_v.y, p_v.z);

        /// <summary> Vector3 (0, y, z) </summary>
        public static Vector3 Oyz(this Vector3 p_v) => new Vector3(0, p_v.y, p_v.z);

        /// <summary> Vector3 (x, 0, z) </summary>
        public static Vector3 Xoz(this Vector3 p_v) => new Vector3(p_v.x, 0, p_v.z);

        /// <summary> Vector3 (x, y, 0) </summary>
        public static Vector3 XY0(this Vector3 p_v) => new Vector3(p_v.x, p_v.y, 0);

        /// <summary> Vector3 (x, 0, 0) </summary>
        public static Vector3 Xoo(this Vector3 p_v) => new Vector3(p_v.x, 0, 0);

        /// <summary> Vector3 (x, y, 0) </summary>
        public static Vector3 Oyo(this Vector3 p_v) => new Vector3(0, p_v.y, 0);

        /// <summary> Vector3 (0, 0, z) </summary>
        public static Vector3 Ooz(this Vector3 p_v) => new Vector3(0, 0, p_v.z);

        /// <summary> Vector3 (value, y, z) </summary>
        public static Vector3 XYZ(this Vector3 p_v, float p_value) => new Vector3(p_value, p_v.y, p_v.z);

        /// <summary> Vector3 (x, value, z) </summary>
        public static Vector3 XyZ(this Vector3 p_v, float p_value) => new Vector3(p_v.x, p_value, p_v.z);

        /// <summary> Vector3 (x, y, value) </summary>
        public static Vector3 XYz(this Vector3 p_v, float p_value) => new Vector3(p_v.x, p_v.y, p_value);

        /// <summary> Vector3 (valueX, valueY, z) </summary>
        public static Vector3 XYZ(this Vector3 p_v, float p_valueX, float p_valueY) => new Vector3(p_valueX, p_valueY, p_v.z);

        public static Vector3 GetCrossProductWithFixedPlane(this Vector3 p_v, Vector3 p_fixedVector)
        {
            var l_perpendicularVector = Vector3.Cross(p_v, p_fixedVector);
            return l_perpendicularVector;
        }

        public static Vector3 GetDeviatedVectorWithFixedPlane(this Vector3 p_v, Vector3 p_fixedVector, float p_multiplier)
        {
            return ((GetCrossProductWithFixedPlane(p_v, p_fixedVector) * p_multiplier + p_v)).normalized;
        }

        /// <summary> Vector4 (x, y, z, 0) </summary>
        public static Vector4 Xyzo(this Vector3 p_v) => new Vector4(p_v.x, p_v.y, p_v.z, 0);

        /// <summary> Vector4 (x, y, z, 1) </summary>
        public static Vector4 XYZ1(this Vector3 p_v) => new Vector4(p_v.x, p_v.y, p_v.z, 1);

        /// <summary> Vector4 (x, y, z, value) </summary>
        public static Vector4 XYZw(this Vector3 p_v, float p_value) => new Vector4(p_v.x, p_v.y, p_v.z, p_value);

        public static Vector3 ClampMagnitude(this Vector3 p_v, float p_min, float p_max)
        {
            double l_sqrMag = p_v.sqrMagnitude;
            double l_sqrMin = p_min * p_min;
            double l_sqrMax = p_max * p_max;

            if (l_sqrMag < l_sqrMin) return p_v.normalized * p_min;
            else if (l_sqrMag > l_sqrMax) return p_v.normalized * p_max;
            else return p_v;
        }

        #endregion

        #region Vector4

        /// <summary> Vector3 (x, y, z) </summary>
        public static Vector3 XYZ(this Vector4 p_v) => new Vector3(p_v.x, p_v.y, p_v.z);

        #endregion
    }
}