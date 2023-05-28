// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using System;
using Silk.NET.Core.Attributes;

#pragma warning disable 1591

namespace Silk.NET.SPIRV
{
    [NativeName("Name", "SpvFPOperationMode_")]
    public enum FPOperationMode : int
    {
        [NativeName("Name", "SpvFPOperationModeIEEE")]
        Ieee = 0x0,
        [NativeName("Name", "SpvFPOperationModeALT")]
        Alt = 0x1,
        [NativeName("Name", "SpvFPOperationModeMax")]
        Max = 0x7FFFFFFF,
    }
}
