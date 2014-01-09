/*
 * Copyright 2006-2013 Bastian Eicher
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
using System.IO;
using AlphaFramework.World;
using Common.Storage;
using ICSharpCode.SharpZipLib.Zip;
using TerrainSample.World.Config;

namespace TerrainSample.World
{
    /// <summary>
    /// Represents a game session (i.e. a game actually being played).
    /// It is equivalent to the content of a savegame.
    /// </summary>
    public sealed class Session : SessionBase<Universe>
    {
        #region Constants
        /// <summary>
        /// The file extensions when this class is stored as a file.
        /// </summary>
        public const string FileExt = "." + GeneralSettings.AppNameShort + "Save";

        /// <summary>
        /// Used for encrypting serialized versions of this class.
        /// </summary>
        /// <remarks>This provides only very basic protection against savegame tampering.</remarks>
        private const string EncryptionKey = "Session";
        #endregion

        #region Constructor
        /// <summary>
        /// Base-constructor for XML serialization. Do not call manually!
        /// </summary>
        public Session()
        {}

        /// <summary>
        /// Creates a new game session based upon a given <see cref="Universe"/>.
        /// </summary>
        /// <param name="baseUniverse">The universe to base the new game session on.</param>
        public Session(Universe baseUniverse) : base(baseUniverse)
        {}
        #endregion

        //--------------------//

        #region Storage
        /// <summary>
        /// Loads a <see cref="Session"/> from a encrypted XML file (savegame).
        /// </summary>
        /// <param name="path">The file to load from.</param>
        /// <returns>The loaded <see cref="Session"/>.</returns>
        /// <exception cref="IOException">Thrown if a problem occurred while reading the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown if read access to the file is not permitted.</exception>
        /// <exception cref="InvalidOperationException">Thrown if a problem occurred while deserializing the XML data.</exception>
        public static Session Load(string path)
        {
            // Load the file
            Session session;
            try
            {
                session = XmlStorage.LoadXmlZip<Session>(path, EncryptionKey);
            }
                #region Error handling
            catch (ZipException ex)
            {
                throw new IOException(ex.Message, ex);
            }
            #endregion

            // Restore the orginal map filename
            session.Universe.SourceFile = session.MapSourceFile;

            return session;
        }

        /// <summary>
        /// Saves this <see cref="Session"/> in an encrypted XML file (savegame).
        /// </summary>
        /// <param name="path">The file to save in.</param>
        /// <exception cref="IOException">Thrown if a problem occurred while writing the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown if write access to the file is not permitted.</exception>
        public void Save(string path)
        {
            this.SaveXmlZip(path, EncryptionKey);
        }
        #endregion
    }
}
