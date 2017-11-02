using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables.Collections;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Commands
{
    public class AddRelatedMovieCommand : Command
    {
        private readonly INavigator navigator;
        private readonly Library library;
        private readonly ObservableCollection<Movie> items;

        public AddRelatedMovieCommand(INavigator navigator, Library library, ObservableCollection<Movie> items)
        {
            Ensure.NotNull(navigator, "navigator");
            Ensure.NotNull(library, "library");
            Ensure.NotNull(items, "items");
            this.navigator = navigator;
            this.library = library;
            this.items = items;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async void Execute()
        {
            IEnumerable<IKey> keys = await navigator.SelectMoviesAsync(library);
            if (keys != null)
            {
                foreach (IKey key in keys)
                    AddKey(key);
            }
        }

        public bool AddKey(IKey key)
        {
            Ensure.Condition.NotEmptyKey(key);

            Movie model = library.Movies.FindByKey(key);
            if (model == null)
                return false;

            items.Add(model);
            return true;
        }
    }
}
