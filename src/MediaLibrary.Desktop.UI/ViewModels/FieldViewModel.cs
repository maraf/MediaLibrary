using Neptuo;
using Neptuo.Observables;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels
{
    public class FieldViewModel
    {
        private readonly Action<string> propertyChanged;

        public IFieldDefinition Definition { get; private set; }

        private object value;
        public object Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    propertyChanged(Definition.Identifier);
                }
            }
        }

        public FieldViewModel(IFieldDefinition definition, Action<string> propertyChanged)
        {
            Ensure.NotNull(definition, "definition");
            Ensure.NotNull(propertyChanged, "propertyChanged");
            Definition = definition;
            this.propertyChanged = propertyChanged;
        }
    }
}
