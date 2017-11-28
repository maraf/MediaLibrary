using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI.Controls
{
    public class FieldDefinitionContainer : IFieldDefinitionContainer
    {
        private IFieldDefinition definition;

        public IFieldDefinition Definition
        {
            get { return definition; }
            set
            {
                if(definition != value)
                {
                    definition = value;
                    Changed?.Invoke();
                }
            }
        }

        public event Action Changed;
    }
}
