using Neptuo;
using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels
{
    public class SortViewModel
    {
        public IFieldDefinition FieldDefinition { get; private set; }
        public string Label { get; private set; }

        public SortViewModel(IFieldDefinition fieldDefinition)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            FieldDefinition = fieldDefinition;
            Label = fieldDefinition.Metadata.Get("Label", fieldDefinition.Identifier);
        }
    }
}
