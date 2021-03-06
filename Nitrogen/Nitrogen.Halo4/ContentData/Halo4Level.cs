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

using Nitrogen.Core.ContentData;
using Nitrogen.Core.ContentData.Localization;
using Nitrogen.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Halo4.ContentData
{
    /// <summary>
    /// Provides an overview of a level (map).
    /// </summary>
    /// <remarks>Represents the 'levl' chunk in a map info BLF file.</remarks>
    public class Halo4Level
        : Level
    {
        public const int InsertionPointCount = 12;
        public const int MPObjectTableSize = 256;
        private const int LanguageCount = 17;

        private string mapImageFileName, mapFileName;
        private int unk0, unk1;
        private byte unk2, unk3, unk4, unk5;
        private int unk6, unk7;
        private int[] mpObjectTable;
        private List<InsertionPoint> insertionPoints;
        private string defaultMapVariantAuthor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4Level"/> class with default values.
        /// </summary>
        public Halo4Level()
            : base(version: 9)
        {
            Name = new LocalizedName(GameData.Languages, "");
            Description = new LocalizedDescription(GameData.Languages, "");

            this.mapImageFileName = "";
            this.mapFileName = "";
            this.mpObjectTable = new int[MPObjectTableSize / 4];
            this.defaultMapVariantAuthor = "Nitrogen";

            this.insertionPoints = new List<InsertionPoint>();
            for (int i = 0; i < InsertionPointCount; i++)
            {
                this.insertionPoints.Add(new InsertionPoint());
            }
        }

        /// <summary>
        /// Gets or sets the default map variant author.
        /// </summary>
        public string DefaultMapVariantAuthor
        {
            get { return this.defaultMapVariantAuthor; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.defaultMapVariantAuthor = value;
            }
        }

        /// <summary>
        /// Gets a collection of all insertion points in this level.
        /// </summary>
        /// <seealso cref="InsertionPointCount"/>
        public IReadOnlyList<InsertionPoint> InsertionPoints
        {
            get { return this.insertionPoints; }
        }

        /// <summary>
        /// Gets or sets the map file name (without the extension).
        /// </summary>
        public string MapFileName
        {
            get { return this.mapFileName; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.mapFileName = value;
            }
        }

        /// <summary>
        /// Gets or sets the map image file name (without the extension).
        /// </summary>
        public string MapImageFileName
        {
            get { return this.mapImageFileName; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.mapImageFileName = value;
            }
        }

        #region Level Members

        protected override void SerializeEndianStreamData(EndianStream s)
        {
            base.SerializeEndianStreamData(s);

            s.Stream(ref this.mapImageFileName, Encoding.ASCII, 256);
            s.Stream(ref this.mapFileName, Encoding.ASCII, 256);
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);
            s.Stream(ref this.unk2);
            s.Stream(ref this.unk3);
            s.Stream(ref this.unk4);
            s.Stream(ref this.unk5);
            s.Stream(ref this.unk6);
            s.Stream(ref this.unk7);
            s.Stream(this.mpObjectTable, 0, this.mpObjectTable.Length);
            s.Serialize(this.insertionPoints, 0, this.insertionPoints.Count);
            s.Stream(ref this.defaultMapVariantAuthor, Encoding.ASCII, 16);
        }

        #endregion

        /// <summary>
        /// Represents an insertion point in a level.
        /// </summary>
        public class InsertionPoint
            : ISerializable<EndianStream>
        {
            private bool isUsed;
            private bool isVisible;
            private byte unk0;
            private byte unk1;
            private string zoneName;
            private int unk2;
            private LocalizedName name;
            private LocalizedDescription description;

            /// <summary>
            /// Initializes a new instance of the <see cref="InsertionPoint"/> class with default values.
            /// </summary>
            public InsertionPoint()
            {
                this.name = new LocalizedName(GameData.Languages, "");
                this.description = new LocalizedDescription(GameData.Languages, "");
                this.zoneName = "";
            }

            /// <summary>
            /// Gets or sets the description of this insertion point.
            /// </summary>
            public LocalizedDescription Description
            {
                get { return this.description; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    this.description = value;
                }
            }

            /// <summary>
            /// Gets or sets whether this insertion point is enabled.
            /// </summary>
            public bool Enabled
            {
                get { return this.isUsed; }
                set { this.isUsed = value; }
            }

            /// <summary>
            /// Gets or sets the name of this insertion point.
            /// </summary>
            public LocalizedName Name
            {
                get { return this.name; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    this.name = value;
                }
            }

            /// <summary>
            /// Gets or sets whether this insertion point is visible.
            /// </summary>
            public bool Visible
            {
                get { return this.isVisible; }
                set { this.isVisible = value; }
            }

            /// <summary>
            /// Gets or sets the zone name of this insertion point.
            /// </summary>
            public string ZoneName
            {
                get { return this.zoneName; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    Contract.Requires<ArgumentOutOfRangeException>(Encoding.ASCII.GetByteCount(value) < 128);

                    this.zoneName = value;
                }
            }

            #region IEndianStreamSerializable Members

            public void Serialize(EndianStream s)
            {
                s.Stream(ref this.isUsed);
                s.Stream(ref this.isVisible);
                s.Stream(ref this.unk0);
                s.Stream(ref this.unk1);
                s.Stream(ref this.zoneName, Encoding.ASCII, 128);
                s.Stream(ref this.unk2);
                this.name.Serialize(s);
                this.description.Serialize(s);
            }

            #endregion
        }
    }
}
