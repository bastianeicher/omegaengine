/*
 * Copyright 2006-2014 Bastian Eicher
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;
using Resources = OmegaEngine.Properties.Resources;

namespace OmegaEngine.Graphics.VertexDecl
{
    /// <summary>
    /// A custom vertex format that stores position, normals, shadow information, texture blending weights and texture coordinates.
    /// Using this format hints the engine that tangents (and maybe normals) still need to be calculated.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PositionNormalMultiTextured
    {
        #region Constants
        /// <summary>
        /// The length of this vertex structure in bytes.
        /// </summary>
        public const int StrideSize = 30 * 4;
        #endregion

        #region Variables
        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable FieldCanBeMadeReadOnly.Global
        /// <summary>The position of the vertex in entity-space</summary>
        public Vector3 Position;

        /// <summary>The normal of the vertex in entity-space</summary>
        public Vector3 Normal;

        /// <summary>The U-component of the texture coordinates</summary>
        public float Tu;

        /// <summary>The V-component of the texture coordinates</summary>
        public float Tv;

        /// <summary>The angles at which the global light source occlusion begins and ends</summary>
        public Vector4 OcclusionIntervals;

        /// <summary>Texture blending weights</summary>
        public Vector4 TexWeights1, TexWeights2, TexWeights3, TexWeights4;

        /// <summary>A color by which the texture will be multiplied</summary>
        public Color4 Color;

        // ReSharper restore FieldCanBeMadeReadOnly.Global
        // ReSharper restore MemberCanBePrivate.Global
        #endregion

        #region Properties
        /// <summary>The X-component of the position of the vertex in entity-space</summary>
        public float X { get { return Position.X; } set { Position.X = value; } }

        /// <summary>The Y-component of the position of the vertex in entity-space</summary>
        public float Y { get { return Position.Y; } set { Position.Y = value; } }

        /// <summary>The Z-component of the position of the vertex in entity-space</summary>
        public float Z { get { return Position.Z; } set { Position.Z = value; } }

        /// <summary>The X-component of the normal of the vertex in entity-space</summary>
        public float Nx { get { return Normal.X; } set { Normal.X = value; } }

        /// <summary>The X-component of the normal of the vertex in entity-space</summary>
        public float Ny { get { return Normal.Y; } set { Normal.Y = value; } }

        /// <summary>The X-component of the normal of the vertex in entity-space</summary>
        public float Nz { get { return Normal.Z; } set { Normal.Z = value; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new multi-textured vertex
        /// </summary>
        /// <param name="position">The position of the vertex in entity-space</param>
        /// <param name="normal">The normal vector</param>
        /// <param name="tu">The U-component of the texture coordinates</param>
        /// <param name="tv">The V-component of the texture coordinates</param>
        /// <param name="occlusionIntervals">The angles at which the global light source occlusion begins and ends</param>
        /// <param name="texWeights">A 16-element array of texture blending weight</param>
        /// <param name="color">A color by which the texture will be multiplied</param>
        public PositionNormalMultiTextured(Vector3 position, Vector3 normal, float tu, float tv, Vector4 occlusionIntervals, float[] texWeights, Color4 color)
        {
            #region Sanity checks
            if (texWeights == null) throw new ArgumentNullException(nameof(texWeights));
            if (texWeights.Length != 16)
                throw new ArgumentException(string.Format(Resources.WrongTexArrayLength, "16"), nameof(texWeights));
            #endregion

            Position = position;
            Normal = normal;
            Tu = tu;
            Tv = tv;
            OcclusionIntervals = occlusionIntervals;
            TexWeights1 = new Vector4(texWeights[0], texWeights[1], texWeights[2], texWeights[3]);
            TexWeights2 = new Vector4(texWeights[4], texWeights[5], texWeights[6], texWeights[7]);
            TexWeights3 = new Vector4(texWeights[8], texWeights[9], texWeights[10], texWeights[11]);
            TexWeights4 = new Vector4(texWeights[12], texWeights[13], texWeights[14], texWeights[15]);
            Color = color;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return "PositionNormalMultiTextured(position=" + Position + ", " +
                   "normal=" + Normal +
                   "tu=" + Tu + ", " + "tv=" + Tv + ", " +
                   "texWeights=" + TexWeights1 + TexWeights2 + TexWeights3 + TexWeights4 + ")";
        }
        #endregion

        //--------------------//

        #region Vertex declaration
        /// <summary>
        /// Returns an array describing the usage of the vertex fields
        /// </summary>
        public static VertexElement[] GetVertexElements()
        {
            return new[]
            {
                // Position
                new VertexElement(0, 0, DeclarationType.Float3, DeclarationMethod.Default,
                    DeclarationUsage.Position, 0),
                // Normal
                new VertexElement(0, sizeof(float) * 3, DeclarationType.Float3, DeclarationMethod.Default,
                    DeclarationUsage.Normal, 0),
                // Tu, Tv
                new VertexElement(0, sizeof(float) * 6, DeclarationType.Float2, DeclarationMethod.Default,
                    DeclarationUsage.TextureCoordinate, 0),
                // OcclusionIntervals
                new VertexElement(0, sizeof(float) * 10, DeclarationType.Float4, DeclarationMethod.Default,
                    DeclarationUsage.TextureCoordinate, 1),
                // TexWeights1
                new VertexElement(0, sizeof(float) * 14, DeclarationType.Float4, DeclarationMethod.Default,
                    DeclarationUsage.TextureCoordinate, 2),
                // TexWeights2
                new VertexElement(0, sizeof(float) * 18, DeclarationType.Float4, DeclarationMethod.Default,
                    DeclarationUsage.TextureCoordinate, 3),
                // TexWeights3
                new VertexElement(0, sizeof(float) * 22, DeclarationType.Float4, DeclarationMethod.Default,
                    DeclarationUsage.TextureCoordinate, 4),
                // TexWeights4
                new VertexElement(0, sizeof(float) * 26, DeclarationType.Float4, DeclarationMethod.Default,
                    DeclarationUsage.TextureCoordinate, 5),
                // Color
                new VertexElement(0, sizeof(float) * 30, DeclarationType.Float4, DeclarationMethod.Default,
                    DeclarationUsage.Color, 0),
                // End
                VertexElement.VertexDeclarationEnd
            };
        }
        #endregion
    }
}
