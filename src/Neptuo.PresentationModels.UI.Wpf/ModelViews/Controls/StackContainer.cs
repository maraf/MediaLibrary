using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews.Controls
{
    public class StackContainer : StackPanel, IModelDefinitionContainer
    {
        public IModelDefinition ModelDefinition { get; private set; }

        public StackContainer(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            ModelDefinition = modelDefinition;
        }
    }
}
