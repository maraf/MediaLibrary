﻿using MediaLibrary.ViewModels;
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
                    var movie1 = libraryModel.Create("Movie 1");
                    movie1.TrySetValue("Storage", "DVD");
                    movie1.TrySetValue("Year", "2010");
                    var movie2 = libraryModel.Create("Movie 2");
                    movie2.TrySetValue("Storage", "DVD");
                    movie2.TrySetValue("Year", "1980");
                    movie2.TrySetValue("Country", "UK");
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
                    main = new MainViewModel(LibraryModel, new MockNavigator(), new MockLibraryStore(), new MockChangeTracker());
                    main.Sorts.First().IsActive = true;
                }

                return main;
            }
        }

        private static RelatedMoviesViewModel related;
        public static RelatedMoviesViewModel Related
        {
            get
            {
                if (related == null)
                {
                    related = new RelatedMoviesViewModel(new MockNavigator(), LibraryModel);
                    related.Items.Add(LibraryModel.Movies[0]);
                    related.Items.Add(LibraryModel.Movies[1]);
                    related.Items.Add(LibraryModel.Movies[2]);
                }

                return related;
            }
        }
    }
}
