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

using Nitrogen.Core;
using Nitrogen.HaloReach.ContentData;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.HaloReach
{
    /// <summary>
    /// Represents a map info blob for Halo: Reach maps.
    /// </summary>
    public class HaloReachMapInfoBlob
        : MapInfoBlob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloReachMapInfoBlob"/> with default values.
        /// </summary>
        public HaloReachMapInfoBlob()
            : base(new HaloReachLevel()) { }

        /// <summary>
        /// Gets or sets the level (map) information.
        /// </summary>
        public new HaloReachLevel Level
        {
            get { return base.Level as HaloReachLevel; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                base.Level = value;
            }
        }
    }
}
