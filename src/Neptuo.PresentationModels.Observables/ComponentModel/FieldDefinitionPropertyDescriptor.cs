using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Observables.ComponentModel
{
    /// <summary>
    /// An implementation of <see cref="PropertyDescriptor"/> based on <see cref="IFieldDefinition"/>.
    /// </summary>
    public class FieldDefinitionPropertyDescriptor : PropertyDescriptor
    {
        private readonly IFieldDefinition fieldDefinition;

        /// <summary>
        /// Creates a new instance for <paramref name="fieldDefinition"/>.
        /// </summary>
        /// <param name="fieldDefinition">A field definition.</param>
        public FieldDefinitionPropertyDescriptor(IFieldDefinition fieldDefinition)
            : base(fieldDefinition.Identifier, Array.Empty<Attribute>())
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            this.fieldDefinition = fieldDefinition;
        }

        public override Type ComponentType => null;

        public override Type PropertyType => fieldDefinition.FieldType;

        public override bool IsReadOnly => fieldDefinition.Metadata.Get("IsReadOnly", false);

        public override bool CanResetValue(object component) => true;

        public override void ResetValue(object component) => SetValue(component, null);

        public override bool ShouldSerializeValue(object component) => GetValue(component) != null;

        public override object GetValue(object component)
        {
            if (component is IModelValueGetter getter && getter.TryGetValue(fieldDefinition.Identifier, out object value))
                return value;

            return null;
        }

        public override void SetValue(object component, object value)
        {
            if (component is IModelValueSetter setter)
                setter.TrySetValue(fieldDefinition.Identifier, value);
        }
    }
}
