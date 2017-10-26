﻿using MediaLibrary.ViewModels.Commands;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Activators;
using Neptuo.Observables;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaLibrary.ViewModels
{
    public class MovieEditViewModel : ObservableObject
    {
        private readonly Movie movie;

        public MovieCollection Collection { get; }
        public bool IsNewRecord { get; }
        public IReadOnlyCollection<FieldViewModel> Fields { get; private set; }
        public ICommand Save { get; }
        public ICommand Close { get; }

        public string Name
        {
            get
            {
                FieldViewModel fieldDefinition = Fields.First(f => f.Definition.Identifier == nameof(Movie.Name));
                return fieldDefinition.Value as string;
            }
            set
            {
                FieldViewModel fieldDefinition = Fields.First(f => f.Definition.Identifier == nameof(Movie.Name));
                fieldDefinition.Value = value;
            }
        }

        public MovieEditViewModel(Library library, INavigatorContext navigator)
        {
            Ensure.NotNull(library, "library");

            Collection = library.Movies;
            Fields = new List<FieldViewModel>(library.MovieDefinition.Fields.Select(f => new FieldViewModel(f, RaisePropertyChanged)));
            IsNewRecord = true;

            FieldViewModel addedField = Fields.FirstOrDefault(f => f.Definition.Identifier == nameof(Movie.Added));
            if (addedField != null)
                addedField.Value = DateTime.Now;

            Save = new SaveMovieCommand(this, library, navigator, null);
            Close = new CloseCommand(navigator);
        }

        public MovieEditViewModel(Library library, INavigatorContext navigator, Movie movie)
        {
            Ensure.NotNull(library, "library");
            Ensure.NotNull(movie, "movie");
            this.movie = movie;

            Collection = library.Movies;
            Fields = new List<FieldViewModel>(library.MovieDefinition.Fields.Select(f => new FieldViewModel(f, RaisePropertyChanged)));

            foreach (FieldViewModel fieldViewModel in Fields)
            {
                if (movie.TryGetValue(fieldViewModel.Definition.Identifier, out object value))
                    fieldViewModel.Value = value;
            }

            Save = new SaveMovieCommand(this, library, navigator, movie);
            Close = new CloseCommand(navigator);
        }
    }
}
