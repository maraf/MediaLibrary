using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MediaLibrary.ViewModels.Commands
{
    public class DeleteMovieCommand : AsyncCommand<IKey>
    {
        private readonly MovieCollection collection;
        private readonly INavigator navigator;

        public DeleteMovieCommand(MovieCollection collection, INavigator navigator)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(navigator, "navigator");
            this.collection = collection;
            this.navigator = navigator;
        }

        protected override bool CanExecuteOverride(IKey key) => key != null && !key.IsEmpty;

        protected override async Task ExecuteAsync(IKey key, CancellationToken cancellationToken)
        {
            Movie model = collection.FindByKey(key);
            if (model != null && await navigator.ConfirmAsync($"Delete movie '{model.Name}'?"))
                collection.Remove(model);
        }
    }
}
