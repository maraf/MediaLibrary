using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Neptuo;

namespace MediaLibrary.ViewModels.Commands
{
    public class SaveCommand : AsyncCommand
    {
        private readonly Library library;
        private readonly XmlStore store;

        public SaveCommand(Library library, XmlStore store)
        {
            Ensure.NotNull(library, "library");
            Ensure.NotNull(store, "store");
            this.library = library;
            this.store = store;
        }

        protected override bool CanExecuteOverride()
        {
            return true;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return store.SaveAsync(library);
        }
    }
}
