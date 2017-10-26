using Neptuo;
using Neptuo.Collections.Specialized;
using Neptuo.Observables;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels
{
    public class SortViewModel : ObservableObject
    {
        public IFieldDefinition FieldDefinition { get; private set; }
        public string Label { get; private set; }
        public ListSortDirection DefaultDirection { get; private set; }

        private bool isActivate;
        public bool IsActive
        {
            get { return isActivate; }
            set
            {
                if (isActivate != value)
                {
                    isActivate = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ListSortDirection direction;
        public ListSortDirection Direction
        {
            get { return direction; }
            set
            {
                if (direction != value)
                {
                    direction = value;
                    RaisePropertyChanged();
                }
            }
        }

        public SortViewModel(IFieldDefinition fieldDefinition)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            FieldDefinition = fieldDefinition;
            Label = fieldDefinition.Metadata.Get("Label", fieldDefinition.Identifier);
            DefaultDirection = fieldDefinition.Metadata.Get("DefaultSortDirection", ListSortDirection.Ascending);
        }
    }
}
