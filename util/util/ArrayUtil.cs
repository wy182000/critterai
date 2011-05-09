﻿using System;
using System.Collections.Generic;
using System.Text;

namespace org.critterai
{
    public static class ArrayUtil
    {
        /// <summary>
        /// Determines whether or not the elements of the provided vectors 
        /// are equal within the specified tolerance of each other.
        /// </summary>
        /// <param name="u">A vector array in the form (x, y, z). (Length: 3)
        /// </param>
        /// <param name="v">A vector array in the form (x, y, z). (Length: 3)
        /// </param>
        /// <param name="tolerance">The tolerance for the test.  </param>
        /// <returns>TRUE if the associated elements are  within the 
        /// specified tolerance of each other</returns>
        public static bool SloppyEquals(float[] a, float[] b, float tolerance)
        {
            tolerance = Math.Max(0, tolerance);
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < b[i] - tolerance || a[i] > b[i] + tolerance)
                    return false;
            }
            return true;
        }

        public static ushort ToUInt16(byte[] source, int index)
        {
            return (ushort)((ushort)(source[index] << 8) + source[index + 1]);
        }

        public static uint ToUInt32(byte[] source, int index)
        {
            return (((uint)source[index] << 24) 
                + ((uint)source[index + 1] << 16)
                + ((uint)source[index + 2] << 8)
                + (uint)source[index + 3]);
        }

        public static byte[] ToByteArray(uint[] source)
        {
            int targetLength = sizeof(uint) * source.Length;
            byte[] tmp = new byte[targetLength];
            Buffer.BlockCopy(source, 0, tmp, 0, targetLength);
            return tmp;
        }

        public static byte[] ToByteArray(ushort[] source)
        {
            int targetLength = sizeof(ushort) * source.Length;
            byte[] tmp = new byte[targetLength];
            Buffer.BlockCopy(source, 0, tmp, 0, targetLength);
            return tmp;
        }

        public static uint[] ToUIntArray(byte[] source)
        {
            uint[] tmp = new uint[source.Length / sizeof(uint)];
            Buffer.BlockCopy(source, 0, tmp, 0, source.Length);
            return tmp;
        }

        public static uint[] ToUIntArray(int[] source)
        {
            uint[] tmp = new uint[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                tmp[i] = (uint)source[i];
            }
            return tmp;
        }

        public static int[] ToIntArray(uint[] source)
        {
            int[] tmp = new int[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                tmp[i] = (int)source[i];
            }
            return tmp;
        }

        public static ushort[] ToUShortArray(byte[] source)
        {
            ushort[] tmp = new ushort[source.Length / sizeof(ushort)];
            Buffer.BlockCopy(source, 0, tmp, 0, source.Length);
            return tmp;
        }
    }
}
