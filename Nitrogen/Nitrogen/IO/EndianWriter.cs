﻿/*
 *   Nitrogen - Halo Content API
 *   Copyright (c) 2013 Matt Saville and Aaron Dierking
 * 
 *   This file is part of Nitrogen.
 *
 *   Nitrogen is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   Nitrogen is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with Nitrogen.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace Nitrogen.Core.IO
{
    /// <summary>
    /// Writes primitive types (and <see cref="IList"/> objects containing primitive types) in binary
    /// to a stream in a certain byte order, and supports writing <see cref="DateTime"/> objects and
    /// strings.
    /// </summary>
    public class EndianWriter
        : BinaryWriter
    {
        public EndianWriter(EndianStream stream, bool leaveOpen = false)
            : base(stream, leaveOpen)
        {
        }
    }
}
