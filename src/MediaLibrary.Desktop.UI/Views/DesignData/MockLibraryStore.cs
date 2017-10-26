using MediaLibrary.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Views.DesignData
{
    internal class MockLibraryStore : ILibraryStore
    {
        public Task LoadAsync(Library library)
        {
            return Task.CompletedTask;
        }

        public Task SaveAsync(Library library)
        {
            return Task.CompletedTask;
        }
    }
}
