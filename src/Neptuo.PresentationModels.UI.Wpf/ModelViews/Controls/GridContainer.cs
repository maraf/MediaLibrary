using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews.Controls
{
    public class GridContainer : Grid, IModelDefinitionContainer
    {
        public IModelDefinition Definition { get; private set; }

        public GridContainer(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Definition = modelDefinition;
        }
    }
}
