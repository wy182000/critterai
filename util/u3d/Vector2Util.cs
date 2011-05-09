﻿/*
 * Copyright (c) 2010 Stephen A. Pratt
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using UnityEngine;

namespace org.critterai
{
    /// <summary>
    /// Provides various 2D vector utility methods which are not provided
    /// by the Unity3D vector class.
    /// </summary>
    /// <remarks>
    /// <p>Static methods are thread safe.</p>
    /// </remarks>
    public static class Vector2Util
    {
        private const float EPSILON_STD = MathUtil.Epsilon;

        /// <summary>
        /// Returns the
        /// <a href="http://en.wikipedia.org/wiki/Dot_product" target="_blank">
        /// dot product</a> of the provided vectors.
        /// </summary>
        /// <param name="ux">The x-value of the vector (ux, uy).</param>
        /// <param name="uy">The y-value of the vector (ux, uy).</param>
        /// <param name="vx">The x-value of the vector (vx, vy).</param>
        /// <param name="vy">The y-value of the vector (vx, vy).</param>
        /// <returns>The dot product of the provided vectors.</returns>
        public static float Dot(float ux, float uy, float vx, float vy)
        {
            return (ux * vx) + (uy * vy);
        }

        /// <summary>
        /// Derives the normalized direction vector from point A (ax, ay) 
        /// to point B (bx, by).
        /// <p>WARNING: This is a costly method.</p>
        /// </summary>
        /// <param name="ax">The x-value for the starting point A (ax, ay).
        /// </param>
        /// <param name="ay">The y-value for the starting point A (ax, ay).
        /// </param>
        /// <param name="bx">The x-value for the end point B (bx, by).
        /// </param>
        /// <param name="by">The y-value for the end point B (bx, by).
        /// </param>
        /// <returns>The normalized direction vector for the vector pointing 
        /// from point A to B.</returns>
        public static Vector2 GetDirectionAB(float ax, float ay
                , float bx, float by)
        {
            // Subtract.
            Vector2 result = new Vector2(bx - ax, by - ay);
            
            // Normalize.
            float length = (float)
                Math.Sqrt((result.x * result.x) + (result.y * result.y));
            if (length <= EPSILON_STD) 
                length = 1;
            
            result.x /= length;
            result.y /= length;
            
            if (Math.Abs(result.x) < EPSILON_STD) 
                result.x = 0;
            if (Math.Abs(result.y) < EPSILON_STD) 
                result.y = 0;    
            
            return result;
        }

        /// <summary>
        /// Determines whether or not the elements of the provided vectors 
        /// are equal within the specified tolerance.
        /// </summary>
        /// <param name="ux">The x-value of the vector (ux, uy).</param>
        /// <param name="uy">The y-value of the vector (ux, uy).</param>
        /// <param name="vx">The x-value of the vector (vx, vy).</param>
        /// <param name="vy">The y-value of the vector (vx, vy).</param>
        /// <param name="tolerance">The tolerance for the test.</param>
        /// <returns>TRUE if each of the associated elements are 
        /// within the specified tolerance of each other.
        /// </returns>
        public static bool SloppyEquals(float ux, float uy
            , float vx, float vy
            , float tolerance)
        {
            tolerance = Math.Max(0, tolerance);
            return !(Math.Abs(ux - vx) > tolerance 
                || Math.Abs(uy - vy) > tolerance);
        }

        /// <summary>
        /// Determines whether or not the elements of the provided vectors 
        /// are equal within the specified tolerance.
        /// </summary>
        /// <param name="u">Vector v</param>
        /// <param name="v">Vector u</param>
        /// <param name="tolerance">The tolerance for the test. </param>
        /// <returns>TRUE if each of the associated elements are
        /// within the specified tolerance of each other.
        /// </returns>
        public static bool SloppyEquals(Vector2 u, Vector2 v, float tolerance)
        {
            return SloppyEquals(u.x, u.y, v.x, v.y, tolerance);
        }

        /// <summary>
        /// Determines whether or not the elements of the provided vectors 
        /// are equal within the specified tolerance.
        /// </summary>
        /// <param name="u">Vector v</param>
        /// <param name="vx">The x-value of the vector (vx, vy).</param>
        /// <param name="vy">The y-value of the vector (vx, vy).</param>
        /// <param name="tolerance">The tolerance for the test.</param>
        /// <returns>TRUE if each of the associated elements are 
        /// within the specified tolerance of each other.
        /// </returns>
        public static bool SloppyEquals(Vector2 u, float vx, float vy, float tolerance)
        {
            return SloppyEquals(u.x, u.y, vx, vy, tolerance);
        }

        /// <summary>
        /// Returns the square of the distance between the two provided 
        /// points.
        /// </summary>
        /// <param name="ax">The x-value of the point (ax, ay).</param>
        /// <param name="ay">The y-value of the point (ax, ay).</param>
        /// <param name="bx">The x-value of the point (bx, by).</param>
        /// <param name="by">The y-value of the point (bx, by).</param>
        /// <returns>The square of the distance between the two provided 
        /// points.</returns>
        public static float GetDistanceSq(float ax, float ay
            , float bx, float by)
        {
            float dx = ax - bx;
            float dy = ay - by;
            return (dx * dx + dy * dy);
        }

        /// <summary>
        /// Normalizes the provided vector such that its length is equal to 
        /// one.
        /// <p>WARNING: This is a costly method.</p>
        /// </summary>
        /// <param name="x">The x-value of the vector (x, y).</param>
        /// <param name="y">The y-value of the vector (x, y).</param>
        /// <returns>The normalized vector.</returns>
        public static Vector2 Normalize(float x, float y) 
        {
            float length = (float)Math.Sqrt(x * x + y * y);
            if (length <= EPSILON_STD) 
                length = 1;
            
            x /= length;
            y /= length;
            
            if (Math.Abs(x) < EPSILON_STD) 
                x = 0;
            if (Math.Abs(y) < EPSILON_STD) 
                y = 0;
            
            return new Vector2(x, y);
        }

        /// <summary>
        /// Scales the vector to the provided length.
        /// <p>WARNING: This is a costly method.</p>
        /// </summary>
        /// <param name="x">The x-value of the vector (x, y).</param>
        /// <param name="y">The y-value of the vector (x, y).</param>
        /// <param name="length">The length to scale the vector to.</param>
        /// <returns>The vector scaled to the provided length.</returns>
        public static Vector2 ScaleTo(float x, float y, float length) 
        {
            if (length == 0 || (x == 0 && y == 0))
                return Vector2.zero;
            float factor = (length / (float)(Math.Sqrt(x * x + y * y)));
            return new Vector2(x * factor, y * factor);
        }

        /// <summary>
        /// Scales the vector to the provided length.
        /// <p>WARNING: This is a costly method.</p>
        /// </summary>
        /// <param name="v">The vector to scale.</param>
        /// <param name="length">The length to scale the vector to.</param>
        /// <returns>A new vector scaled to the provided length.</returns>
        public static Vector2 ScaleTo(Vector2 v, float length) 
        {
            return ScaleTo(v.x, v.y, length);
        }

        /// <summary>
        /// Truncates the length of the vector to the provided value.
        /// </summary>
        /// <remarks>
        /// <p>If the vector's length is longer than the provided value the 
        ///  length of the vector is scaled back to the provided maximum 
        ///  length.</p>
        ///  <p>If the vector's length is shorter than the provided value, 
        ///  the  vector is not changed.</p>
        ///  <p>WARNING: This is a potentially costly method.</p>
        /// </remarks>
        /// <param name="x">The x-value of the vector (x, y).</param>
        /// <param name="y">The y-value of the vector (x, y).</param>
        /// <param name="maxLength">The maximum allowed length of the 
        /// resulting vector.</param>
        /// <returns>A vector with a length at or below the maximum
        /// length.</returns>
        public static Vector2 TruncateLength(float x, float y, float maxLength) 
        {
            if (maxLength == 0 || (x < float.Epsilon && y < float.Epsilon))
            { 
                return new Vector2(0, 0);
            }
            float csq = x * x + y * y;
            if (csq <= maxLength * maxLength)
                return new Vector2(x, y);
            float factor = (float)(maxLength / Math.Sqrt(csq));
            return new Vector2(x * factor, y * factor);
        }

        /// <summary>
        /// Truncates the length of the vector to the provided value.
        /// </summary>
        /// <remarks>
        /// <p>If the vector's length is longer than the provided value 
        /// the length of the vector is scaled back to the provided
        /// maximum length.</p>
        /// <p>If the vector's length is shorter than the provided value, 
        /// the vector is not changed.</p>
        /// <p>WARNING: This is a potentially costly method.</p>
        /// </remarks>
        /// <param name="v">The vector to scale.</param>
        /// <param name="maxLength">The maximum allowed length of the 
        /// resulting vector.</param>
        /// <returns>A vector with a length at or below the maximum
        /// length.</returns>
        public static Vector2 TruncateLength(Vector2 v, float maxLength) 
        {
            return TruncateLength(v.x, v.y, maxLength);
        }
    }
}
