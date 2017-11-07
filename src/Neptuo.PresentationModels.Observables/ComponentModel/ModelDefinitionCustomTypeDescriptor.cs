using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Observables.ComponentModel
{
    /// <summary>
    /// An implentation of <see cref="ICustomTypeDescriptor"/> based on single <see cref="IModelDefinition"/>.
    /// </summary>
    public class ModelDefinitionCustomTypeDescriptor : ICustomTypeDescriptor
    {
        private readonly IModelDefinition modelDefinition;
        private PropertyDescriptorCollection properties;

        /// <summary>
        /// Creates a new instance for <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">A definition of model.</param>
        public ModelDefinitionCustomTypeDescriptor(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            this.modelDefinition = modelDefinition;
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes() => null;

        string ICustomTypeDescriptor.GetClassName() => null;

        string ICustomTypeDescriptor.GetComponentName() => null;

        TypeConverter ICustomTypeDescriptor.GetConverter() => new TypeConverter();

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => null;

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => null;

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => null;

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => null;

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) => null;

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) => null;

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => null;

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            if (properties == null)
                properties = new PropertyDescriptorCollection(modelDefinition.Fields.Select(f => new FieldDefinitionPropertyDescriptor(f)).ToArray());

            return properties;
        }
    }
}
