using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Commands
{
    public class SaveLibraryConfigurationCommand : CloseCommand
    {
        private readonly LibraryConfigurationViewModel viewModel;
        private readonly Library library;

        public SaveLibraryConfigurationCommand(LibraryConfigurationViewModel viewModel, Library library, INavigatorContext navigator)
            : base(navigator)
        {
            Ensure.NotNull(viewModel, "viewModel");
            Ensure.NotNull(library, "library");
            this.viewModel = viewModel;
            this.library = library;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            foreach (FieldViewModel fieldViewModel in viewModel.Fields)
                library.Configuration.TrySetValue(fieldViewModel.Definition.Identifier, fieldViewModel.Value);

            base.Execute();
        }
    }
}
