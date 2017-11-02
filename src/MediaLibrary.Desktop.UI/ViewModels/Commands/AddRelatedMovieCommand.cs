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
        private readonly MovieCollection models;
        private readonly ObservableCollection<Movie> items;

        public AddRelatedMovieCommand(INavigator navigator, MovieCollection models, ObservableCollection<Movie> items)
        {
            Ensure.NotNull(navigator, "navigator");
            Ensure.NotNull(models, "models");
            Ensure.NotNull(items, "items");
            this.navigator = navigator;
            this.models = models;
            this.items = items;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async void Execute()
        {
            IEnumerable<IKey> keys = await navigator.SelectMoviesAsync();
        }

        public bool AddKey(IKey key)
        {
            Ensure.Condition.NotEmptyKey(key);

            Movie model = models.FindByKey(key);
            if (model == null)
                return false;

            items.Add(model);
            return true;
        }
    }
}
