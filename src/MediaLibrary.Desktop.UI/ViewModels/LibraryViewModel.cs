using MediaLibrary.ViewModels.Commands;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Collections.Specialized;
using Neptuo.Models.Keys;
using Neptuo.Observables;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaLibrary.ViewModels
{
    public class LibraryViewModel : ObservableObject
    {
        private readonly Library library;

        public string Name => library.Configuration.Name;
        public IEnumerable<Movie> Movies => library.Movies;
        public IEnumerable<SortViewModel> Sorts { get; }

        public LibraryViewModel(Library library)
        {
            Ensure.NotNull(library, "library");
            this.library = library;

            Sorts = new List<SortViewModel>(library.MovieDefinition.Fields.Where(f => f.Metadata.Get("IsSortable", true)).Select(f => new SortViewModel(f)));
            SortViewModel firstSort = Sorts.FirstOrDefault();
            if (firstSort != null)
                firstSort.IsActive = true;

            library.Configuration.PropertyChanged += OnConfigurationPropertyChanged;
        }

        private void OnConfigurationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(library.Configuration.Name))
                RaisePropertyChanged(nameof(Name));
        }
    }
}
