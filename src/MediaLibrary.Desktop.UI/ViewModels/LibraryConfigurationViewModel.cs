using MediaLibrary.ViewModels.Commands;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaLibrary.ViewModels
{
    public class LibraryConfigurationViewModel : ObservableObject
    {
        public IReadOnlyCollection<FieldViewModel> Fields { get; private set; }

        public ICommand Save { get; }
        public ICommand Close { get; }

        public LibraryConfigurationViewModel(Library library, INavigatorContext navigator)
        {
            Ensure.NotNull(library, "library");
            Fields = new List<FieldViewModel>(library.ConfigurationDefinition.Fields.Select(f => new FieldViewModel(f, RaisePropertyChanged)));

            foreach (FieldViewModel fieldViewModel in Fields)
            {
                if (library.Configuration.TryGetValue(fieldViewModel.Definition.Identifier, out object value))
                    fieldViewModel.Value = value;
            }

            Save = new SaveLibraryConfigurationCommand(this, library, navigator);
            Close = new CloseCommand(navigator);
        }
    }
}
