using MediaLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Views.DesignData
{
    public static class ViewModelLocator
    {
        private static Library libraryModel;
        public static Library LibraryModel
        {
            get
            {
                if (libraryModel == null)
                {
                    libraryModel = new Library();
                    libraryModel.Configuration.Name = "Library";
                    libraryModel.Create("Movie 1");
                    libraryModel.Create("Movie 2");
                    libraryModel.Create("Movie 3");
                }

                return libraryModel;
            }
        }

        private static LibraryViewModel library;
        public static LibraryViewModel Library
        {
            get
            {
                if (library == null)
                {
                    library = new LibraryViewModel(LibraryModel);
                    library.Sorts.First().IsActive = true;
                }

                return library;
            }
        }

        private static MainViewModel main;
        public static MainViewModel Main
        {
            get
            {
                if (main == null)
                {
                    main = new MainViewModel(LibraryModel, new MockNavigator(), new MockLibraryStore());
                    main.Sorts.First().IsActive = true;
                }

                return main;
            }
        }

        private static RelatedMoviesViewModel relatedMovies;
        public static RelatedMoviesViewModel RelatedMovies
        {
            get
            {
                if (relatedMovies == null)
                {
                    relatedMovies = new RelatedMoviesViewModel(new MockNavigator(), LibraryModel.Movies);
                    relatedMovies.Items.Add(LibraryModel.Movies[0]);
                    relatedMovies.Items.Add(LibraryModel.Movies[1]);
                    relatedMovies.Items.Add(LibraryModel.Movies[2]);
                }

                return relatedMovies;
            }
        }
    }
}
