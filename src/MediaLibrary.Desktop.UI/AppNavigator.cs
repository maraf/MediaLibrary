using MediaLibrary.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Models.Keys;

namespace MediaLibrary
{
    public class AppNavigator : INavigator
    {
        public Task CreateMovieAsync(Library library)
        {
            throw new NotImplementedException();
        }

        public Task EditMovieAsync(Library library, IKey movieKey)
        {
            throw new NotImplementedException();
        }

        public Task Library(Library library)
        {
            throw new NotImplementedException();
        }
    }
}
