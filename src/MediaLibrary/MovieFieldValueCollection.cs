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
        private readonly IEnumerable<IFieldDefinition> fieldDefinitions;
        private readonly Action<string> propertyChanged;

        public MovieFieldValueCollection(IEnumerable<IFieldDefinition> fieldDefinitions, Action<string> propertyChanged)
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

        public T FindValue<T>(string identifier)
        {
            if (TryGetValue(identifier, out object rawValue) && rawValue is T value)
                return value;

            return default(T);
        }

        public bool TrySetValue(string identifier, object value)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");
            IFieldDefinition fieldDefinition = fieldDefinitions.FirstOrDefault(f => f.Identifier == identifier);

            if (fieldDefinition != null)
            {
                bool isChanged = true;
                if (storage.TryGetValue(identifier, out object oldValue))
                    isChanged = !Object.Equals(value, oldValue);

                storage[identifier] = value;

                if (isChanged)
                    propertyChanged(identifier);

                return true;
            }

            return false;
        }

        public void Dispose()
        { }
    }
}
