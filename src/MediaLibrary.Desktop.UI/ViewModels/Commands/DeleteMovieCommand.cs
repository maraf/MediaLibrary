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
        private readonly IChangeTracker changeTracker;

        public DeleteMovieCommand(MovieCollection collection, INavigator navigator, IChangeTracker changeTracker)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(navigator, "navigator");
            Ensure.NotNull(changeTracker, "changeTracker");
            this.collection = collection;
            this.navigator = navigator;
            this.changeTracker = changeTracker;
        }

        protected override bool CanExecuteOverride(IKey key) => key != null && !key.IsEmpty;

        protected override async Task ExecuteAsync(IKey key, CancellationToken cancellationToken)
        {
            Movie model = collection.FindByKey(key);
            if (model != null && await navigator.ConfirmAsync($"Delete movie '{model.Name}'?"))
            {
                changeTracker.Remove(collection, model);
            }
        }

        public new void RaiseCanExecuteChanged()
        {
            base.RaiseCanExecuteChanged();
        }
    }
}
