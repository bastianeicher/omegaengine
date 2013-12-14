﻿/*
 * Copyright 2006-2013 Bastian Eicher
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using Common.Undo;
using TemplateWorld.Positionables;
using TemplateWorld.Templates;

namespace AlphaEditor.World.Commands
{
    /// <summary>
    /// Changes the <see cref="ITemplateName.TemplateName"/> property of one or more <see cref="EntityBase{TSelf,TCoordinates,TTemplate}"/>s.
    /// </summary>
    public class ChangeEntityTemplates : SimpleCommand
    {
        #region Variables
        private readonly List<ITemplateName> _entities;
        private readonly string[] _oldTemplates;
        private readonly string _newTemplates;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new command for changing the <see cref="EntityTemplateBase{TSelf}"/> of one or more <see cref="EntityBase{TSelf,TCoordinates,TTemplate}"/>s.
        /// </summary>
        /// <param name="entities">The <see cref="EntityBase{TSelf,TCoordinates,TTemplate}"/>s to modify.</param>
        /// <param name="template">The name of the new <see cref="EntityTemplateBase{TSelf}"/> to set.</param>
        public ChangeEntityTemplates(IEnumerable<ITemplateName> entities, string template)
        {
            #region Sanity checks
            if (entities == null) throw new ArgumentNullException("entities");
            if (template == null) throw new ArgumentNullException("template");
            #endregion

            // Create local defensive copy of entities
            _entities = new List<ITemplateName>(entities);

            // Backup the old class names
            _oldTemplates = new string[_entities.Count];
            for (int i = 0; i < _entities.Count; i++)
                _oldTemplates[i] = _entities[i].TemplateName;

            _newTemplates = template;
        }
        #endregion

        //--------------------//

        #region Undo / Redo
        /// <inheritdoc />
        protected override void OnExecute()
        {
            foreach (var entity in _entities)
                entity.TemplateName = _newTemplates;
        }

        /// <inheritdoc />
        protected override void OnUndo()
        {
            for (int i = 0; i < _entities.Count; i++)
                _entities[i].TemplateName = _oldTemplates[i];
        }
        #endregion
    }
}
