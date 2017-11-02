using MediaLibrary.ViewModels.Commands;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaLibrary.ViewModels
{
    public class MainViewModel : LibraryViewModel
    {
        public ICommand Create { get; }
        public Command<IKey> Edit { get; }
        public Command<IKey> Delete { get; }
        public ICommand Save { get; }
        public ICommand OpenConfiguration { get; }

        public MainViewModel(Library library, INavigator navigator, ILibraryStore store)
            : base(library)
        {
            Ensure.NotNull(navigator, "navigator");

            Create = new DelegateCommand(() => navigator.CreateMovieAsync(library));
            Edit = new EditMovieCommand(library, navigator);
            Delete = new DeleteMovieCommand(library.Movies, navigator);
            Save = new SaveCommand(library, store);
            OpenConfiguration = new DelegateCommand(() => navigator.LibraryConfigurationAsync(library));
        }

        public void SelectedMovieChanged()
        {
            ((EditMovieCommand)Edit).RaiseCanExecuteChanged();
            ((DeleteMovieCommand)Delete).RaiseCanExecuteChanged();
        }
    }
}
