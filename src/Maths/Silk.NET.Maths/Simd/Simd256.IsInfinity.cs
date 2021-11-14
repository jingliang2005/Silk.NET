// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#if INTRINSICS
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
#if SSE
using System.Runtime.Intrinsics.X86;
#endif
#if AdvSIMD
using System.Runtime.Intrinsics.Arm;
#endif

namespace Silk.NET.Maths
{
    public static unsafe partial class Simd256
    {
        /// <summary>
        /// Performs hardware-accelerated IsInfinity on 256-bit vectors.
        /// </summary>
        [MethodImpl(Scalar.MaxOpt)]
        public static Vector256<T> IsInfinity<T>(Vector256<T> vector) where T : unmanaged
        {
            return Single(vector);
            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector256<T> Single(Vector256<T> vector)
            {
                if (typeof(T) == typeof(float))
                {
                    return Equal(And(vector.AsInt32(), SingleUnsignedMask), SingleFiniteThreshold).As<int, T>();
                }
                
                return Double(vector);
            }
            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector256<T> Double(Vector256<T> vector)
            {
                if (typeof(T) == typeof(double))
                {
                    return Equal(And(vector.AsInt64(), DoubleUnsignedMask), DoubleFiniteThreshold).As<long, T>();
                }
                
                return Integer(vector);
            }
            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector256<T> Integer(Vector256<T> vector)
            {
                if (typeof(T) == typeof(byte)
                    || typeof(T) == typeof(sbyte)
                    || typeof(T) == typeof(ushort)
                    || typeof(T) == typeof(short)
                    || typeof(T) == typeof(uint)
                    || typeof(T) == typeof(int)
                    || typeof(T) == typeof(ulong)
                    || typeof(T) == typeof(long))
                {
                    return Simd256<T>.Zero;
                } 
                
                return Other(vector);
            }
            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector256<T> Other(Vector256<T> vector)
            {
                var vec = Vector256<T>.Zero;
                for (int i = 0; i < Vector256<T>.Count; i++)
                {
                    vec.WithElement(i, Scalar.IsInfinity(vector.GetElement(i)) ? Scalar<T>.AllBitsSet : Scalar<T>.Zero);
                }
                return vec;
            }
        }
    }
}
#endif
