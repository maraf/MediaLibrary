using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels;

namespace MediaLibrary
{
    public class MovieFieldValueCollection : IModelValueProvider
    {
        private readonly Dictionary<string, object> storage = new Dictionary<string, object>();
        private readonly IReadOnlyCollection<IFieldDefinition> fieldDefinitions;
        private readonly Action<string> propertyChanged;

        public MovieFieldValueCollection(IReadOnlyCollection<IFieldDefinition> fieldDefinitions, Action<string> propertyChanged)
        {
            Ensure.NotNull(fieldDefinitions, "fieldDefinitions");
            Ensure.NotNull(propertyChanged, "propertyChanged");
            this.fieldDefinitions = fieldDefinitions;
            this.propertyChanged = propertyChanged;
        }

        public bool TryGetValue(string identifier, out object value)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");
            IFieldDefinition fieldDefinition = fieldDefinitions.FirstOrDefault(f => f.Identifier == identifier);

            if (fieldDefinition != null && storage.TryGetValue(identifier, out object rawValue))
            {
                value = rawValue;
                return true;
            }

            value = null;
            return false;
        }

        public bool TrySetValue(string identifier, object value)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");
            IFieldDefinition fieldDefinition = fieldDefinitions.FirstOrDefault(f => f.Identifier == identifier);

            if (fieldDefinition != null)
            {
                storage[identifier] = value;
                return true;
            }

            return false;
        }

        public void Dispose()
        { }
    }
}
