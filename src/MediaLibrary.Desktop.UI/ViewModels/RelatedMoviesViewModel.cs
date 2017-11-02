using MediaLibrary.ViewModels.Commands;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables;
using Neptuo.Observables.Collections;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels
{
    public class RelatedMoviesViewModel : ObservableObject
    {
        public ObservableCollection<Movie> Items { get; private set; }
        public Command<Movie> Remove { get; set; }
        public AddRelatedMovieCommand Add { get; set; }

        public RelatedMoviesViewModel(INavigator navigator, MovieCollection models)
        {
            Ensure.NotNull(navigator, "navigator");
            Items = new ObservableCollection<Movie>();
            Remove = new DelegateCommand<Movie>(m => Items.Remove(m));
            Add = new AddRelatedMovieCommand(navigator, models, Items);
        }
    }
}
