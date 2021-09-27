using System;
using UnityEngine;

namespace Misner.Test.Math
{
    /// <summary>
    /// Double Vector 3
    /// 
    /// A double based Vector3 class, designed to closely mimic Unity's Vector3 class.
    /// 
    /// TODO, remaining methods to implement and build tests for:
    /// static DoubleVector3 SmoothDamp(DoubleVector3 current, DoubleVector3 target, ref DoubleVector3 currentVelocity, float smoothTime, float maxSpeed)
    /// static DoubleVector3 SmoothDamp(DoubleVector3 current, DoubleVector3 target, ref DoubleVector3 currentVelocity, float smoothTime)
    /// static DoubleVector3 SmoothDamp(DoubleVector3 current, DoubleVector3 target, ref DoubleVector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
    /// </summary>
    [System.Serializable]
    public struct DoubleVector3 : IEquatable<DoubleVector3>
    {
        #region Constants
        public const double kEpsilon = 1E-10;
        public const double kEpsilonNormalSqrt = 1E-30;
        #endregion


        #region Public Variables
        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public double x;
        
        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public double y;
        
        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public double z;
        #endregion


        #region Private Static Variables
        private static readonly DoubleVector3 zeroVector = new DoubleVector3(0, 0, 0);
        private static readonly DoubleVector3 oneVector = new DoubleVector3(1, 1, 1);
        private static readonly DoubleVector3 upVector = new DoubleVector3(0, 1, 0);
        private static readonly DoubleVector3 downVector = new DoubleVector3(0, -1, 0);
        private static readonly DoubleVector3 leftVector = new DoubleVector3(-1, 0, 0);
        private static readonly DoubleVector3 rightVector = new DoubleVector3(1, 0, 0);
        private static readonly DoubleVector3 forwardVector = new DoubleVector3(0, 0, 1);
        private static readonly DoubleVector3 backVector = new DoubleVector3(0, 0, -1);
        private static readonly DoubleVector3 positiveInfinityVector = new DoubleVector3(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        private static readonly DoubleVector3 negativeInfinityVector = new DoubleVector3(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
        private const double RadiansToDegrees = 180.0 / System.Math.PI;
        #endregion


        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid DoubleVector3 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid DoubleVector3 index!");
                }
            }
        }


        #region Getter Properties
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1, if possible (Read Only).</para>
        /// </summary>
        public DoubleVector3 normalized => Normalize(this);

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public double magnitude => System.Math.Sqrt(x * x + y * y + z * z);

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public double sqrMagnitude => x * x + y * y + z * z;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(0, 0, 0).</para>
        /// </summary>
        public static DoubleVector3 zero => zeroVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(1, 1, 1).</para>
        /// </summary>
        public static DoubleVector3 one => oneVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(0, 0, 1).</para>
        /// </summary>
        public static DoubleVector3 forward => forwardVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(0, 0, -1).</para>
        /// </summary>
        public static DoubleVector3 back => backVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(0, 1, 0).</para>
        /// </summary>
        public static DoubleVector3 up => upVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(0, -1, 0).</para>
        /// </summary>
        public static DoubleVector3 down => downVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(-1, 0, 0).</para>
        /// </summary>
        public static DoubleVector3 left => leftVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(1, 0, 0).</para>
        /// </summary>
        public static DoubleVector3 right => rightVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity).</para>
        /// </summary>
        public static DoubleVector3 positiveInfinity => positiveInfinityVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity).</para>
        /// </summary>
        public static DoubleVector3 negativeInfinity => negativeInfinityVector;
#pragma warning restore IDE1006 // Naming Styles
        #endregion


        /// <summary>
        ///   <para>Creates a new vector with given x, y, z components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public DoubleVector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y components and sets z to zero.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public DoubleVector3(double x, double y)
        {
            this.x = x;
            this.y = y;
            z = 0.0;
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static DoubleVector3 Lerp(DoubleVector3 a, DoubleVector3 b, double t)
        {
            t = Clamp01_Internal(t);
            return new DoubleVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static DoubleVector3 LerpUnclamped(DoubleVector3 a, DoubleVector3 b, double t)
        {
            return new DoubleVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Calculate a position between the points specified by current and target, moving no farther than the distance specified by maxDistanceDelta.</para>
        /// </summary>
        /// <param name="current">The position to move from.</param>
        /// <param name="target">The position to move towards.</param>
        /// <param name="maxDistanceDelta">Distance to move current per call.</param>
        /// <returns>
        ///   <para>The new position.</para>
        /// </returns>
        public static DoubleVector3 MoveTowards(DoubleVector3 current, DoubleVector3 target, double maxDistanceDelta)
        {
            DoubleVector3 a = target - current;
            double magnitude = a.magnitude;
            if (!(magnitude <= maxDistanceDelta) && !(magnitude < 1.401298E-45))
            {
                return current + a / magnitude * maxDistanceDelta;
            }
            return target;
        }

        /// <summary>
        ///   <para>Set x, y and z components of an existing DoubleVector3.</para>
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="newZ"></param>
        public void Set(double newX, double newY, double newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static DoubleVector3 Scale(DoubleVector3 a, DoubleVector3 b)
        {
            return new DoubleVector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(DoubleVector3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }

        /// <summary>
        ///   <para>Cross Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static DoubleVector3 Cross(DoubleVector3 lhs, DoubleVector3 rhs)
        {
            return new DoubleVector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public override int GetHashCode()
        {
#pragma warning disable RECS0025 // Non-readonly field referenced in 'GetHashCode()'
            return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
#pragma warning restore RECS0025 // Non-readonly field referenced in 'GetHashCode()'
        }

        /// <summary>
        ///   <para>Component-wise equality of two vectors.</para>
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            if (!(obj is DoubleVector3))
            {
                return false;
            }
            return Equals((DoubleVector3)obj);
        }

        /// <summary>
        ///   <para>Component-wise equality of two vectors.</para>
        /// </summary>
        /// <param name="other">The <see cref="Misner.Test.Math.DoubleVector3"/> to compare with the current <see cref="T:Misner.Test.Math.DoubleVector3"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Misner.Test.Math.DoubleVector3"/> is equal to the current
        /// <see cref="T:Misner.Test.Math.DoubleVector3"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(DoubleVector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        /// <summary>
        ///   <para>Reflects a vector off the plane defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static DoubleVector3 Reflect(DoubleVector3 inDirection, DoubleVector3 inNormal)
        {
            return -2.0 * Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1, if possible.</para>
        /// </summary>
        /// <param name="value"></param>
        public static DoubleVector3 Normalize(DoubleVector3 value)
        {
            double num = Magnitude(value);
            if (num > kEpsilon)
            {
                return value / num;
            }
            return zero;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1, if possible.</para>
        /// </summary>
        public void Normalize()
        {
            double num = Magnitude(this);
            if (num > kEpsilon)
            {
                this /= num;
            }
            else
            {
                this = zero;
            }
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static double Dot(DoubleVector3 lhs, DoubleVector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        /// <summary>
        ///   <para>Projects a vector onto another vector.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        public static DoubleVector3 Project(DoubleVector3 vector, DoubleVector3 onNormal)
        {
            double num = Dot(onNormal, onNormal);
            if (num < Mathf.Epsilon)
            {
                return zero;
            }
            return onNormal * Dot(vector, onNormal) / num;
        }

        /// <summary>
        ///   <para>Projects a vector onto a plane defined by a normal orthogonal to the plane.</para>
        /// </summary>
        /// <param name="planeNormal">The direction from the vector towards the plane.</param>
        /// <param name="vector">The location of the vector above the plane.</param>
        /// <returns>
        ///   <para>The location of the vector on the plane.</para>
        /// </returns>
        public static DoubleVector3 ProjectOnPlane(DoubleVector3 vector, DoubleVector3 planeNormal)
        {
            return vector - Project(vector, planeNormal);
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured.</param>
        /// <param name="to">The vector to which the angular difference is measured.</param>
        /// <returns>
        ///   <para>The angle in degrees between the two vectors.</para>
        /// </returns>
        public static double Angle(DoubleVector3 from, DoubleVector3 to)
        {
            double num = System.Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
            if (num < kEpsilon)
            {
                return 0.0;
            }
            double f = Clamp_Internal(Dot(from, to) / num, -1.0, 1.0);
            return System.Math.Acos(f) * RadiansToDegrees;
        }

        /// <summary>
        ///   <para>Returns the signed angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured.</param>
        /// <param name="to">The vector to which the angular difference is measured.</param>
        /// <param name="axis">A vector around which the other vectors are rotated.</param>
        public static double SignedAngle(DoubleVector3 from, DoubleVector3 to, DoubleVector3 axis)
        {
            double num = Angle(from, to);
            double num2 = System.Math.Sign(Dot(axis, Cross(from, to)));
            return num * num2;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static double Distance(DoubleVector3 a, DoubleVector3 b)
        {
            DoubleVector3 vector = new DoubleVector3(a.x - b.x, a.y - b.y, a.z - b.z);
            return System.Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static DoubleVector3 ClampMagnitude(DoubleVector3 vector, double maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength)
            {
                return vector.normalized * maxLength;
            }
            return vector;
        }

        /// <summary>
        ///   <para>Returns the length of the vector provided (Read Only).</para>
        /// </summary>
        public static double Magnitude(DoubleVector3 vector)
        {
            return System.Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }

        /// <summary>
        ///   <para>Returns the squared length of the vector provided (Read Only).</para>
        /// </summary>
        public static double SqrMagnitude(DoubleVector3 vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static DoubleVector3 Min(DoubleVector3 lhs, DoubleVector3 rhs)
        {
            return new DoubleVector3(System.Math.Min(lhs.x, rhs.x), System.Math.Min(lhs.y, rhs.y), System.Math.Min(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static DoubleVector3 Max(DoubleVector3 lhs, DoubleVector3 rhs)
        {
            return new DoubleVector3(System.Math.Max(lhs.x, rhs.x), System.Math.Max(lhs.y, rhs.y), System.Math.Max(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Component-wise addition of two vectors.</para>
        /// </summary>
        /// <param name="a">The first <see cref="Misner.Test.Math.DoubleVector3"/> to add.</param>
        /// <param name="b">The second <see cref="Misner.Test.Math.DoubleVector3"/> to add.</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector3"/> that is the sum of the values of <c>a</c> and <c>b</c>.</returns>
        public static DoubleVector3 operator +(DoubleVector3 a, DoubleVector3 b)
        {
            return new DoubleVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        /// <summary>
        ///   <para>Component-wise subtraction of two vectors.</para>
        /// </summary>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector3"/> to subtract from (the minuend).</param>
        /// <param name="b">The <see cref="Misner.Test.Math.DoubleVector3"/> to subtract (the subtrahend).</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector3"/> that is the <c>a</c> minus <c>b</c>.</returns>
        public static DoubleVector3 operator -(DoubleVector3 a, DoubleVector3 b)
        {
            return new DoubleVector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        /// <summary>
        ///   <para>Component-wise negation of a vector.</para>
        /// </summary>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector3"/> provided, negated.</returns>
        public static DoubleVector3 operator -(DoubleVector3 a)
        {
            return new DoubleVector3(0 - a.x, 0 - a.y, 0 - a.z);
        }

        /// <summary>
        ///   <para>Scalar multiplication of a vector by a individual value.</para>
        /// </summary>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector3"/> to multiply.</param>
        /// <param name="d">The <see cref="double"/> to multiply.</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector3"/> that is the <c>a</c> * <c>d</c>.</returns>
        public static DoubleVector3 operator *(DoubleVector3 a, double d)
        {
            return new DoubleVector3(a.x * d, a.y * d, a.z * d);
        }

        /// <summary>
        ///   <para>Scalar multiplication of a vector by a individual value.</para>
        /// </summary>
        /// <param name="d">The <see cref="double"/> to multiply.</param>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector3"/> to multiply.</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector3"/> that is the <c>d</c> * <c>a</c>.</returns>
        public static DoubleVector3 operator *(double d, DoubleVector3 a)
        {
            return new DoubleVector3(a.x * d, a.y * d, a.z * d);
        }

        /// <summary>
        ///   <para>Scalar division of a vector by a individual value.</para>
        /// </summary>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector3"/> to divide (the divident).</param>
        /// <param name="d">The <see cref="double"/> to divide (the divisor).</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector3"/> that is the <c>a</c> / <c>d</c>.</returns>
        public static DoubleVector3 operator /(DoubleVector3 a, double d)
        {
            return new DoubleVector3(a.x / d, a.y / d, a.z / d);
        }

        /// <summary>
        ///   <para>Spatial equality of two vectors within an epsilon tolerance.</para>
        /// </summary>
        /// <param name="lhs">The first <see cref="Misner.Test.Math.DoubleVector3"/> to compare.</param>
        /// <param name="rhs">The second <see cref="Misner.Test.Math.DoubleVector3"/> to compare.</param>
        /// <returns><c>true</c> if <c>lhs</c> and <c>rhs</c> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(DoubleVector3 lhs, DoubleVector3 rhs)
        {
            return SqrMagnitude(lhs - rhs) < kEpsilonNormalSqrt;
        }

        /// <summary>
        ///   <para>Non-equality of two vectors within a spatial epsilon tolerance.</para>
        /// </summary>
        /// <param name="lhs">The first <see cref="Misner.Test.Math.DoubleVector3"/> to compare.</param>
        /// <param name="rhs">The second <see cref="Misner.Test.Math.DoubleVector3"/> to compare.</param>
        /// <returns><c>true</c> if <c>lhs</c> and <c>rhs</c> are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(DoubleVector3 lhs, DoubleVector3 rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1})", x, y, z);
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2})", x.ToString(format), y.ToString(format), z.ToString(format));
        }

        /// <summary>
        ///   <para>Value clamp implementation based on UnityEngine.Mathf.Clamp().</para>
        /// </summary>
        /// <returns>The internal.</returns>
        /// <param name="value">Value.</param>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Max.</param>
        private static double Clamp_Internal(double value, double min, double max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        /// <summary>
        ///   <para>Clamps value between 0 and 1 and returns value.</para>
        /// </summary>
        /// <param name="value"></param>
        private static double Clamp01_Internal(double value)
        {
            if (value < 0.0)
            {
                return 0.0;
            }
            if (value > 1.0)
            {
                return 1.0;
            }
            return value;
        }

    }
}
