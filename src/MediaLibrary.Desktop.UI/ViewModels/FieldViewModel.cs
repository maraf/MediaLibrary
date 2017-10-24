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
    public class FieldViewModel : ObservableObject
    {
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
                    RaisePropertyChanged();
                }
            }
        }

        public FieldViewModel(IFieldDefinition definition)
        {
            Ensure.NotNull(definition, "definition");
            Definition = definition;
        }
    }
}
