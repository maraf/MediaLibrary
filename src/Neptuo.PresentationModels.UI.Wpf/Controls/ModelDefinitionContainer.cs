using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI.Controls
{
    public class ModelDefinitionContainer : IModelDefinitionContainer
    {
        private IModelDefinition definition;

        public IModelDefinition Definition
        {
            get { return definition; }
            set
            {
                if (definition != value)
                {
                    definition = value;
                    Changed?.Invoke();
                }
            }
        }

        public event Action Changed;
    }
}
