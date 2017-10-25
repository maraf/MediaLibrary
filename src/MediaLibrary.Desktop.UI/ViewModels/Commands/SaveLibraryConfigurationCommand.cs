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
        private readonly LibraryConfiguration model;

        public SaveLibraryConfigurationCommand(LibraryConfigurationViewModel viewModel, LibraryConfiguration model, INavigatorContext navigator)
            : base(navigator)
        {
            Ensure.NotNull(viewModel, "viewModel");
            Ensure.NotNull(model, "model");
            this.viewModel = viewModel;
            this.model = model;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            model.Name = viewModel.Name;
            model.FilePath = viewModel.FilePath;
            model.OnlineDatabaseName = viewModel.OnlineDatabaseName;
            model.OnlineDatabaseUrlFormat = viewModel.OnlineDatabaseUrlFormat;

            base.Execute();
        }
    }
}
