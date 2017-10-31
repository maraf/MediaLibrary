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
                    library = new LibraryViewModel(LibraryModel, new MockNavigator(), new MockLibraryStore());
                    library.Sorts.First().IsActive = true;
                }

                return library;
            }
        }
    }
}
