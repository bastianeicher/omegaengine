﻿/*
 * Copyright 2006-2013 Bastian Eicher
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Diagnostics.CodeAnalysis;
using TemplateWorld.Terrains;

namespace AlphaEditor.World.Commands
{
    /// <summary>
    /// Loads new texture-map data into a <see cref="ITerrain"/>.
    /// </summary>
    public class ImportTextureMap : ImportMap
    {
        #region Constructor
        /// <summary>
        /// Creates a new command for loading texture-map data into a <see cref="ITerrain"/>.
        /// </summary>
        /// <param name="terrain">The <see cref="ITerrain"/> to load new texture-map data into.</param>
        /// <param name="fileName">The file to load the texture-map data from.</param>
        /// <param name="refreshHandler">Called when the <see cref="ITerrain"/> needs to be reset.</param>
        public ImportTextureMap(ITerrain terrain, string fileName, Action refreshHandler)
            : base(terrain, fileName, refreshHandler)
        {}
        #endregion

        //--------------------//

        #region Terrain access
        /// <summary>
        /// Points to <see cref="ITerrain.TextureMap"/>
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "This property provides direct access to the underlying array without any cloning involved")]
        protected override byte[,] MapData { get { return _terrain.TextureMap; } set { _terrain.TextureMap = value; } }

        /// <summary>
        /// Loads the texture-map data from a file into the <see cref="ITerrain"/>
        /// </summary>
        protected override void LoadMap()
        {
            _terrain.LoadTextureMap(_fileName);
        }
        #endregion
    }
}
