﻿using MediaLibrary.ViewModels;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Observables.Commands;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaLibrary.Views
{
    public partial class MovieEditWindow : ModelWindow
    {
        private readonly IChangeTracker changeTracker;
        private readonly Library library;
        private readonly IModelDefinition modelDefinition;
        private Movie model;

        public MovieEditWindow(INavigator navigator, IChangeTracker changeTracker, Library library, Movie model)
            : base(navigator, library)
        {
            Ensure.NotNull(changeTracker, "changeTracker");
            Ensure.NotNull(library, "library");

            InitializeComponent();

            kebClose.Command = new DelegateCommand(Close);

            this.changeTracker = changeTracker;
            this.library = library;
            this.modelDefinition = library.MovieDefinition;
            this.model = model;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            ModelPresenter.Definition = modelDefinition;
            if (model != null)
            {
                CopyModelValueProvider copy = new CopyModelValueProvider(modelDefinition, true);
                copy.Update(ModelPresenter, model);
            }
            else
            {
                ModelPresenter.TrySetValue(nameof(Movie.Added), DateTime.Now);
            }
        }

        private void OnSaveClick()
        {
            if (!ModelPresenter.TryGetValue(nameof(Movie.Name), out object rawName) || String.IsNullOrEmpty(rawName as string))
                return;

            if (model == null)
                model = library.Create();

            changeTracker.UpdateModel(modelDefinition, model, ModelPresenter);

            Close();
        }

        private void OnCloseClick() => Close();
    }
}
