using Neptuo.PresentationModels.Observables.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Observables
{
    /// <summary>
    /// A observable implementation of <see cref="IModelValueProvider"/> and <see cref="ICustomTypeDescriptor"/> based on single <see cref="IModelDefinition"/>.
    /// </summary>
    [TypeDescriptionProvider(typeof(ModelDefinitionContainerTypeDescriptorProvider))]
    public class ObservableModel : ObservableModelValueProvider, IModelDefinitionContainer
    {
        /// <summary>
        /// Gets a definition of model.
        /// </summary>
        protected IModelDefinition ModelDefinition { get; private set; }

        IModelDefinition IModelDefinitionContainer.Definition => ModelDefinition;

        /// <summary>
        /// Creates a new instance for <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">A definition of model.</param>
        public ObservableModel(IModelDefinition modelDefinition)
            : this(modelDefinition, new DictionaryModelValueProvider())
        { }

        /// <summary>
        /// Creates a new instance for <paramref name="modelDefinition"/> with custom innner value storage <paramref name="valueProvider"/>.
        /// </summary>
        /// <param name="modelDefinition">A definition of model.</param>
        /// <param name="valueProvider">A custom inner value storage.</param>
        public ObservableModel(IModelDefinition modelDefinition, IModelValueProvider valueProvider)
            : base(valueProvider)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            ModelDefinition = modelDefinition;
        }

        public override bool TrySetValue(string identifier, object value)
        {
            if (ModelDefinition.Fields.Any(f => f.Identifier == identifier))
                return base.TrySetValue(identifier, value);

            return false;
        }
    }
}
