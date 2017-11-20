using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Services
{
    /// <summary>
    /// A service providing state changes.
    /// </summary>
    public interface IChangeTracker
    {
        /// <summary>
        /// Returns <c>true</c> if contains any change; <c>false</c> otherwise.
        /// </summary>
        bool Has { get; }

        /// <summary>
        /// Updates values in <paramref name="model"/> from <paramref name="newState"/>.
        /// </summary>
        void UpdateModel(IModelDefinition definition, IModelValueProvider model, IModelValueGetter newState);

        /// <summary>
        /// Cleas all changes.
        /// </summary>
        void Clear();

        /// <summary>
        /// Raised when a change is added.
        /// </summary>
        event Action Added;

        /// <summary>
        /// Raised when all changes are cleared.
        /// </summary>
        event Action Cleared;

        /// <summary>
        /// Removes <paramref name="model"/> from <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">A collection of movies.</param>
        /// <param name="model">A movie to remove.</param>
        void Remove(MovieCollection collection, Movie model);
    }
}
