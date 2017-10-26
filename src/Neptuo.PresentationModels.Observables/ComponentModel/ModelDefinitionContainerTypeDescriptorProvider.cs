using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Observables.ComponentModel
{
    /// <summary>
    /// An implementation of <see cref="TypeDescriptionProvider"/> for instances implementing <see cref="IModelDefinitionContainer"/>.
    /// </summary>
    public class ModelDefinitionContainerTypeDescriptorProvider : TypeDescriptionProvider
    {
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            if (instance is IModelDefinitionContainer container && container.ModelDefinition != null)
                return new ModelDefinitionCustomTypeDescriptor(container.ModelDefinition);

            return base.GetTypeDescriptor(objectType, instance);
        }
    }
}
