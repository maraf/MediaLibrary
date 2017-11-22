using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// A component holding instance of <see cref="IModelDefinition"/>.
    /// </summary>
    public interface IModelDefinitionContainer
    {
        /// <summary>
        /// Gets an definition of model.
        /// </summary>
        IModelDefinition Definition { get; }
    }
}
