using MediaLibrary.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Models.Keys;

namespace MediaLibrary.Views.DesignData
{
    internal class MockNavigator : INavigator
    {
        public Task CreateMovieAsync(Library library)
        {
            return Task.CompletedTask;
        }

        public Task EditMovieAsync(Library library, IKey movieKey)
        {
            return Task.CompletedTask;
        }

        public Task Library(Library library)
        {
            throw new NotImplementedException();
        }
    }
}
