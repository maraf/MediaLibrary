using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Commands
{
    public class EditMovieCommand : Command<IKey>
    {
        private readonly Library library;
        private readonly INavigator navigator;

        public EditMovieCommand(Library library, INavigator navigator)
        {
            Ensure.NotNull(library, "library");
            Ensure.NotNull(navigator, "navigator");
            this.library = library;
            this.navigator = navigator;
        }

        public override bool CanExecute(IKey key) => key != null && !key.IsEmpty;

        public override void Execute(IKey key)
        {
            navigator.EditMovieAsync(library, key);
        }

        public new void RaiseCanExecuteChanged()
        {
            base.RaiseCanExecuteChanged();
        }
    }
}
