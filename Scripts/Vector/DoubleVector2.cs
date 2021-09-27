using System;

namespace Misner.Test.Math
{
    /// <summary>
    /// Double Vector 2
    /// 
    /// A double based Vector2 class, designed to closely mimic Unity's Vector2 class.
    /// 
    /// TODO, remaining methods to implement and build tests for:
    /// static DoubleVector2 Lerp(DoubleVector2 a, DoubleVector2 b, float t)
    /// static DoubleVector2 LerpUnclamped(DoubleVector2 a, DoubleVector2 b, float t)
    /// static DoubleVector2 MoveTowards(DoubleVector2 current, DoubleVector2 target, float maxDistanceDelta)
    /// static DoubleVector2 SmoothDamp(DoubleVector2 current, DoubleVector2 target, ref DoubleVector2 currentVelocity, float smoothTime, float maxSpeed)
    /// static DoubleVector2 SmoothDamp(DoubleVector2 current, DoubleVector2 target, ref DoubleVector2 currentVelocity, float smoothTime)
    /// static DoubleVector2 SmoothDamp(DoubleVector2 current, DoubleVector2 target, ref DoubleVector2 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
    /// static implicit operator DoubleVector2(Vector3 v)
    /// static implicit operator Vector3(DoubleVector2 v)
    /// </summary>
    [System.Serializable]
    public struct DoubleVector2 : IEquatable<DoubleVector2>
    {
        #region Variables
        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public double x;

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public double y;
        #endregion


        #region Constants
        private static readonly DoubleVector2 zeroVector = new DoubleVector2(0, 0);
        private static readonly DoubleVector2 oneVector = new DoubleVector2(1, 1);
        private static readonly DoubleVector2 upVector = new DoubleVector2(0, 1);
        private static readonly DoubleVector2 downVector = new DoubleVector2(0, -1);
        private static readonly DoubleVector2 leftVector = new DoubleVector2(-1, 0);
        private static readonly DoubleVector2 rightVector = new DoubleVector2(1, 0);
        private static readonly DoubleVector2 positiveInfinityVector = new DoubleVector2(double.PositiveInfinity, double.PositiveInfinity);
        private static readonly DoubleVector2 negativeInfinityVector = new DoubleVector2(double.NegativeInfinity, double.NegativeInfinity);
        public const double kEpsilon = 1E-10;
        public const double kEpsilonNormalSqrt = 1E-30;
        private const double RadiansToDegrees = 180.0 / System.Math.PI;
        #endregion


        /// <summary>
        /// Array style access to vector compenents.
        /// 
        /// For a DoubleVector2 name 'v':
        /// v[0] == v.x
        /// v[1] == v.y
        /// </summary>
        /// <param name="index">Index.</param>
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
                    default:
                        throw new IndexOutOfRangeException("Invalid DoubleVector2 index!");
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
                    default:
                        throw new IndexOutOfRangeException("Invalid DoubleVector2 index!");
                }
            }
        }

        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1, if possible (Read Only).</para>
        /// </summary>
        public DoubleVector2 normalized
        {
            get
            {
                DoubleVector2 result = new DoubleVector2(x, y);
                result.Normalize();
                return result;
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public double magnitude => System.Math.Sqrt(x * x + y * y);

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public double sqrMagnitude => x * x + y * y;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector2(0, 0).</para>
        /// </summary>
        public static DoubleVector2 zero => zeroVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector2(1, 1).</para>
        /// </summary>
        public static DoubleVector2 one => oneVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector2(0, 1).</para>
        /// </summary>
        public static DoubleVector2 up => upVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector2(0, -1).</para>
        /// </summary>
        public static DoubleVector2 down => downVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector2(-1, 0).</para>
        /// </summary>
        public static DoubleVector2 left => leftVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector2(1, 0).</para>
        /// </summary>
        public static DoubleVector2 right => rightVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector2(double.PositiveInfinity, double.PositiveInfinity).</para>
        /// </summary>
        public static DoubleVector2 positiveInfinity => positiveInfinityVector;

        /// <summary>
        ///   <para>Shorthand for writing DoubleVector2(double.NegativeInfinity, double.NegativeInfinity).</para>
        /// </summary>
        public static DoubleVector2 negativeInfinity => negativeInfinityVector;

        /// <summary>
        ///   <para>Constructs a new vector with given x, y components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public DoubleVector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        ///   <para>Set x and y components of an existing DoubleVector2.</para>
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        public void Set(double newX, double newY)
        {
            x = newX;
            y = newY;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static DoubleVector2 Scale(DoubleVector2 a, DoubleVector2 b)
        {
            return new DoubleVector2(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(DoubleVector2 scale)
        {
            x *= scale.x;
            y *= scale.y;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1, if possible.</para>
        /// </summary>
        public void Normalize()
        {
            double magnitude = this.magnitude;
            if (magnitude > kEpsilon)
            {
                this /= magnitude;
            }
            else
            {
                this = zero;
            }
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1})", x, y);
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("({0}, {1})", x.ToString(format), y.ToString(format));
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:Misner.Test.Math.DoubleVector2"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
#pragma warning disable RECS0025 // Non-readonly field referenced in 'GetHashCode()'
            return x.GetHashCode() ^ y.GetHashCode() << 2;
#pragma warning restore RECS0025 // Non-readonly field referenced in 'GetHashCode()'
        }

        /// <summary>
        ///   <para>Component-wise equality of two vectors.</para>
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            if (!(obj is DoubleVector2))
            {
                return false;
            }
            return Equals((DoubleVector2)obj);
        }

        /// <summary>
        ///   <para>Component-wise equality of two vectors.</para>
        /// </summary>
        /// <param name="other">The <see cref="Misner.Test.Math.DoubleVector2"/> to compare with the current <see cref="T:Misner.Test.Math.DoubleVector2"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Misner.Test.Math.DoubleVector2"/> is equal to the current
        /// <see cref="T:Misner.Test.Math.DoubleVector2"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(DoubleVector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        /// <summary>
        ///   <para>Reflects a vector off the vector defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static DoubleVector2 Reflect(DoubleVector2 inDirection, DoubleVector2 inNormal)
        {
            return -2.0 * Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para>Returns the 2D vector perpendicular to this 2D vector. The result is always rotated 90-degrees in a counter-clockwise direction for a 2D coordinate system where the positive Y axis goes up.</para>
        /// </summary>
        /// <param name="inDirection">The input direction.</param>
        /// <returns>
        ///   <para>The perpendicular direction.</para>
        /// </returns>
        public static DoubleVector2 Perpendicular(DoubleVector2 inDirection)
        {
            return new DoubleVector2(0 - inDirection.y, inDirection.x);
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static double Dot(DoubleVector2 lhs, DoubleVector2 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        /// <summary>
        ///   <para>Returns the unsigned angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured.</param>
        /// <param name="to">The vector to which the angular difference is measured.</param>
        public static double Angle(DoubleVector2 from, DoubleVector2 to)
        {
            double num = System.Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
            if (num < kEpsilonNormalSqrt)
            {
                return 0;
            }
            double f = Clamp_Internal(Dot(from, to) / num, -1, 1);
            return System.Math.Acos(f) * RadiansToDegrees;
        }

        /// <summary>
        ///   <para>Returns the signed angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured.</param>
        /// <param name="to">The vector to which the angular difference is measured.</param>
        public static double SignedAngle(DoubleVector2 from, DoubleVector2 to)
        {
            double num = Angle(from, to);
            double num2 = System.Math.Sign(from.x * to.y - from.y * to.x);
            return num * num2;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static double Distance(DoubleVector2 a, DoubleVector2 b)
        {
            return (a - b).magnitude;
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static DoubleVector2 ClampMagnitude(DoubleVector2 vector, double maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength)
            {
                return vector.normalized * maxLength;
            }
            return vector;
        }

        /// <summary>
        ///   <para>Returns the squared length of the vector provided (Read Only).</para>
        /// </summary>
        public static double SqrMagnitude(DoubleVector2 a)
        {
            return a.x * a.x + a.y * a.y;
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public double SqrMagnitude()
        {
            return x * x + y * y;
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static DoubleVector2 Min(DoubleVector2 lhs, DoubleVector2 rhs)
        {
            return new DoubleVector2(System.Math.Min(lhs.x, rhs.x), System.Math.Min(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static DoubleVector2 Max(DoubleVector2 lhs, DoubleVector2 rhs)
        {
            return new DoubleVector2(System.Math.Max(lhs.x, rhs.x), System.Math.Max(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Component-wise addition of two vectors.</para>
        /// </summary>
        /// <param name="a">The first <see cref="Misner.Test.Math.DoubleVector2"/> to add.</param>
        /// <param name="b">The second <see cref="Misner.Test.Math.DoubleVector2"/> to add.</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector2"/> that is the sum of the values of <c>a</c> and <c>b</c>.</returns>
        public static DoubleVector2 operator +(DoubleVector2 a, DoubleVector2 b)
        {
            return new DoubleVector2(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        ///   <para>Component-wise subtraction of two vectors.</para>
        /// </summary>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector2"/> to subtract from (the minuend).</param>
        /// <param name="b">The <see cref="Misner.Test.Math.DoubleVector2"/> to subtract (the subtrahend).</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector2"/> that is the <c>a</c> minus <c>b</c>.</returns>
        public static DoubleVector2 operator -(DoubleVector2 a, DoubleVector2 b)
        {
            return new DoubleVector2(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        ///   <para>Component-wise multiplication of two vectors.</para>
        /// </summary>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector2"/> to multiply.</param>
        /// <param name="b">The <see cref="Misner.Test.Math.DoubleVector2"/> to multiply.</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector2"/> that is the <c>a</c> * <c>b</c>.</returns>
        public static DoubleVector2 operator *(DoubleVector2 a, DoubleVector2 b)
        {
            return new DoubleVector2(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        ///   <para>Component-wise division of two vectors.</para>
        /// </summary>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector2"/> to divide (the divident).</param>
        /// <param name="b">The <see cref="Misner.Test.Math.DoubleVector2"/> to divide (the divisor).</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector2"/> that is the <c>a</c> / <c>b</c>.</returns>
        public static DoubleVector2 operator /(DoubleVector2 a, DoubleVector2 b)
        {
            return new DoubleVector2(a.x / b.x, a.y / b.y);
        }

        /// <summary>
        ///   <para>Component-wise negation of a vector.</para>
        /// </summary>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector2"/> provided, negated.</returns>
        public static DoubleVector2 operator -(DoubleVector2 a)
        {
            return new DoubleVector2(0 - a.x, 0 - a.y);
        }

        /// <summary>
        ///   <para>Scalar multiplication of a vector by a individual value.</para>
        /// </summary>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector2"/> to multiply.</param>
        /// <param name="d">The <see cref="double"/> to multiply.</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector2"/> that is the <c>a</c> * <c>d</c>.</returns>
        public static DoubleVector2 operator *(DoubleVector2 a, double d)
        {
            return new DoubleVector2(a.x * d, a.y * d);
        }

        /// <summary>
        ///   <para>Scalar multiplication of a vector by a individual value.</para>
        /// </summary>
        /// <param name="d">The <see cref="double"/> to multiply.</param>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector2"/> to multiply.</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector2"/> that is the <c>d</c> * <c>a</c>.</returns>
        public static DoubleVector2 operator *(double d, DoubleVector2 a)
        {
            return new DoubleVector2(a.x * d, a.y * d);
        }

        /// <summary>
        ///   <para>Scalar division of a vector by a individual value.</para>
        /// </summary>
        /// <param name="a">The <see cref="Misner.Test.Math.DoubleVector2"/> to divide (the divident).</param>
        /// <param name="d">The <see cref="double"/> to divide (the divisor).</param>
        /// <returns>The <see cref="T:Misner.Test.Math.DoubleVector2"/> that is the <c>a</c> / <c>d</c>.</returns>
        public static DoubleVector2 operator /(DoubleVector2 a, double d)
        {
            return new DoubleVector2(a.x / d, a.y / d);
        }

        /// <summary>
        ///   <para>Spatial equality of two vectors within an epsilon tolerance.</para>
        /// </summary>
        /// <param name="lhs">The first <see cref="Misner.Test.Math.DoubleVector2"/> to compare.</param>
        /// <param name="rhs">The second <see cref="Misner.Test.Math.DoubleVector2"/> to compare.</param>
        /// <returns><c>true</c> if <c>lhs</c> and <c>rhs</c> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(DoubleVector2 lhs, DoubleVector2 rhs)
        {
            return (lhs - rhs).sqrMagnitude < kEpsilonNormalSqrt;
        }

        /// <summary>
        ///   <para>Non-equality of two vectors within a spatial epsilon tolerance.</para>
        /// </summary>
        /// <param name="lhs">The first <see cref="Misner.Test.Math.DoubleVector2"/> to compare.</param>
        /// <param name="rhs">The second <see cref="Misner.Test.Math.DoubleVector2"/> to compare.</param>
        /// <returns><c>true</c> if <c>lhs</c> and <c>rhs</c> are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(DoubleVector2 lhs, DoubleVector2 rhs)
        {
            return !(lhs == rhs);
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

    }
}
