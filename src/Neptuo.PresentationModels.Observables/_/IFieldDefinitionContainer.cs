using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// A component holding instance of <see cref="IFieldDefinition"/>.
    /// </summary>
    public interface IFieldDefinitionContainer
    {
        /// <summary>
        /// Gets an definition of field.
        /// </summary>
        IFieldDefinition FieldDefinition { get; }
    }
}
