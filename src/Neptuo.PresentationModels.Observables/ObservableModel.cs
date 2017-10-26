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
    public class ObservableModel : ObservableModelValueProvider, ICustomTypeDescriptor
    {
        protected IModelDefinition ModelDefinition { get; private set; }

        /// <summary>
        /// Creates a new instance for <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">A definition of model.</param>
        public ObservableModel(IModelDefinition modelDefinition)
            : this(modelDefinition, new DictionaryModelValueProvider())
        {
        }

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

        #region ICustomTypeDescriptor

        private PropertyDescriptorCollection properties;

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return AttributeCollection.Empty;
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return null;
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return null;
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return null;
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return null;
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return null;
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return null;
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return null;
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return null;
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            if (properties == null)
                properties = new PropertyDescriptorCollection(ModelDefinition.Fields.Select(f => new FieldDefinitionPropertyDescriptor(f)).ToArray());

            return properties;
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return null;
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return null;
        }

        #endregion
    }
}
