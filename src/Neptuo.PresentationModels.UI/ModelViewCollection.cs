﻿using Neptuo.Activators;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Implementation of <see cref="IModelViewProvider{T}"/> based on model identifiers.
    /// For each model definition, there can be registered single model view.
    /// For generic pursoses, there can be registered search delegate for finding model view.
    /// </summary>
    public class ModelViewCollection<T> : IModelViewProvider<T>
    {
        private readonly Dictionary<string, IFactory<IModelView<T>>> storage = new Dictionary<string, IFactory<IModelView<T>>>();
        private readonly OutFuncCollection<IModelDefinition, IFactory<IModelView<T>>, bool> onSearchView = new OutFuncCollection<IModelDefinition, IFactory<IModelView<T>>, bool>();

        /// <summary>
        /// Pairs model definition identified by <paramref name="modelIdentifier"/> with view provided by <paramref name="modelViewFactory"/>.
        /// </summary>
        /// <param name="modelIdentifier">Model identifier.</param>
        /// <param name="modelViewFactory">Activator for creating model view instances.</param>
        /// <returns>Self (for fluency).</returns>
        public ModelViewCollection<T> Add(string modelIdentifier, IFactory<IModelView<T>> modelViewFactory)
        {
            Ensure.NotNullOrEmpty(modelIdentifier, "modelIdentifier");
            Ensure.NotNull(modelViewFactory, "modelViewFactory");
            storage[modelIdentifier] = modelViewFactory;
            return this;
        }

        /// <summary>
        /// Registers generic search handler for unregistered model identifiers.
        /// </summary>
        /// <param name="searchHandler">Search delegate for providing model view activator.</param>
        /// <returns>Self (for fluency).</returns>
        public ModelViewCollection<T> AddSearchHandler(OutFunc<IModelDefinition, IFactory<IModelView<T>>, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchView.Add(searchHandler);
            return this;
        }

        public bool TryGet(IModelDefinition modelDefinition, out IModelView<T> modelView)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            IFactory<IModelView<T>> modelViewActivator;
            if (storage.TryGetValue(modelDefinition.Identifier, out modelViewActivator))
            {
                modelView = modelViewActivator.Create();
                return true;
            }

            if (onSearchView.TryExecute(modelDefinition, out modelViewActivator))
            {
                modelView = modelViewActivator.Create();
                return true;
            }

            modelView = null;
            return false;
        }
    }
}
