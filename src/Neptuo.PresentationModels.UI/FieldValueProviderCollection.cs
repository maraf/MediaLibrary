using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// A collection of field value providers.
    /// </summary>
    public class FieldValueProviderCollection : IReadOnlyCollection<IFieldValueProvider>
    {
        private readonly Dictionary<string, IFieldValueProvider> storage = new Dictionary<string, IFieldValueProvider>();

        /// <summary>
        /// Adds <paramref name="provider"/> to be handling value of fields <paramref name="identifier"/>.
        /// </summary>
        /// <param name="identifier">A field identifier.</param>
        /// <param name="provider">A field value provider.</param>
        /// <returns>Self (for fluency).</returns>
        public FieldValueProviderCollection Add(string identifier, IFieldValueProvider provider)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");
            if (provider == null)
                storage.Remove(identifier);
            else
                storage[identifier] = provider;

            return this;
        }

        /// <summary>
        /// Tries to get a value provider for field <paramref name="identifier"/>.
        /// </summary>
        /// <param name="identifier">A field identifier.</param>
        /// <param name="provider">A field value provider or null.</param>
        /// <returns><c>true</c> <paramref name="provider"/> was found and is not <c>null</c>; <c>false</c> otherwise.</returns>
        public bool TryGet(string identifier, out IFieldValueProvider provider)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");
            return storage.TryGetValue(identifier, out provider) && provider != null;
        }

        #region IReadOnlyCollection<IFieldValueProvider>

        /// <summary>
        /// Gets a count of registered providers.
        /// </summary>
        public int Count => storage.Count;

        public IEnumerator<IFieldValueProvider> GetEnumerator()
        {
            return storage.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
