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
    public static unsafe partial class Simd64
    {
        /// <summary>
        /// Performs hardware-accelerated Equal on 64-bit vectors.
        /// </summary>
        [MethodImpl(Scalar.MaxOpt)]
        public static Vector64<T> Equal<T>(Vector64<T> left, Vector64<T> right) where T : unmanaged
        {
            return Byte(left, right);            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> Byte(Vector64<T> left, Vector64<T> right)
            {
                if (typeof(T) == typeof(byte))
                {
#if AdvSIMD
                    if (AdvSimd.IsSupported)
                    {
                        return AdvSimd.CompareEqual(left.AsByte(), right.AsByte()).As<byte, T>();
                    }
#endif
                }
        
                return SByte(left, right);
            }            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> SByte(Vector64<T> left, Vector64<T> right)
            {
                if (typeof(T) == typeof(sbyte))
                {
#if AdvSIMD
                    if (AdvSimd.IsSupported)
                    {
                        return AdvSimd.CompareEqual(left.AsSByte(), right.AsSByte()).As<sbyte, T>();
                    }
#endif
                }
        
                return UInt16(left, right);
            }            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> UInt16(Vector64<T> left, Vector64<T> right)
            {
                if (typeof(T) == typeof(ushort))
                {
#if AdvSIMD
                    if (AdvSimd.IsSupported)
                    {
                        return AdvSimd.CompareEqual(left.AsUInt16(), right.AsUInt16()).As<ushort, T>();
                    }
#endif
                }
        
                return Int16(left, right);
            }            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> Int16(Vector64<T> left, Vector64<T> right)
            {
                if (typeof(T) == typeof(short))
                {
#if AdvSIMD
                    if (AdvSimd.IsSupported)
                    {
                        return AdvSimd.CompareEqual(left.AsInt16(), right.AsInt16()).As<short, T>();
                    }
#endif
                }
        
                return UInt32(left, right);
            }            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> UInt32(Vector64<T> left, Vector64<T> right)
            {
                if (typeof(T) == typeof(uint))
                {
#if AdvSIMD
                    if (AdvSimd.IsSupported)
                    {
                        return AdvSimd.CompareEqual(left.AsUInt32(), right.AsUInt32()).As<uint, T>();
                    }
#endif
                }
        
                return Int32(left, right);
            }            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> Int32(Vector64<T> left, Vector64<T> right)
            {
                if (typeof(T) == typeof(int))
                {
#if AdvSIMD
                    if (AdvSimd.IsSupported)
                    {
                        return AdvSimd.CompareEqual(left.AsInt32(), right.AsInt32()).As<int, T>();
                    }
#endif
                }
        
                return Single(left, right);
            }            
             
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> Single(Vector64<T> left, Vector64<T> right)
            {
                if (typeof(T) == typeof(float))
                {
#if AdvSIMD
                    if (AdvSimd.IsSupported)
                    {
                        return AdvSimd.CompareEqual(left.AsSingle(), right.AsSingle()).As<float, T>();
                    }
#endif
                }
        
                return Other8byte(left, right);
            }
            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> Other8byte(Vector64<T> left, Vector64<T> right)
            {
                if (sizeof(T) == 8)
                {
                    var res = Scalar.Equal(Unsafe.As<Vector64<T>, T>(ref left), Unsafe.As<Vector64<T>, T>(ref right));
                    return res ? Simd64<T>.AllBitsSet : Simd64<T>.Zero;
                }
                return Other(left, right);
            }
            
            [MethodImpl(Scalar.MaxOpt)]
            static Vector64<T> Other(Vector64<T> left, Vector64<T> right)
            {
                var vec = Vector64<T>.Zero;
                for (int i = 0; i < Vector64<T>.Count; i++)
                {
                    vec = vec.WithElement(i, Scalar.Equal(left.GetElement(i), right.GetElement(i)) ? Scalar<T>.AllBitsSet : Scalar<T>.Zero);
                }
                return vec;
            }
        }
    }
}
#endif