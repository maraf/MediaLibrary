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
        private static LibraryViewModel library;

        public static LibraryViewModel Library
        {
            get
            {
                if (library == null)
                {
                    Library model = new Library();
                    model.Configuration.Name = "Library";
                    model.Create("Movie 1");
                    model.Create("Movie 2");
                    model.Create("Movie 3");

                    library = new LibraryViewModel(model, new MockNavigator());
                }

                return library;
            }
        }
    }
}
