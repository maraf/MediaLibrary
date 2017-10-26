using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Activators;
using Neptuo.Observables.Commands;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Commands
{
    public class SaveMovieCommand : Command
    {
        private readonly MovieEditViewModel viewModel;
        private readonly IFactory<Movie> movieFactory;
        private readonly INavigatorContext navigator;
        private Movie model;

        public SaveMovieCommand(MovieEditViewModel viewModel, IFactory<Movie> movieFactory, INavigatorContext navigator, Movie model = null)
        {
            Ensure.NotNull(viewModel, "viewModel");
            Ensure.NotNull(movieFactory, "movieFactory");
            Ensure.NotNull(navigator, "navigator");
            this.viewModel = viewModel;
            this.movieFactory = movieFactory;
            this.navigator = navigator;
            this.model = model;

            viewModel.PropertyChanged += OnViewModelChanged;
        }

        private void OnViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.Name))
                RaiseCanExecuteChanged();
        }

        public override bool CanExecute()
        {
            return !String.IsNullOrEmpty(viewModel.Name) && !String.IsNullOrWhiteSpace(viewModel.Name);
        }

        public override void Execute()
        {
            if (model == null)
                model = movieFactory.Create();

            foreach (FieldViewModel fieldViewModel in viewModel.Fields)
                model.TrySetValue(fieldViewModel.Definition.Identifier, fieldViewModel.Value);

            navigator.Close();
        }
    }
}
