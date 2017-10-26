using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Services
{
    public interface ILibraryStore
    {
        Task LoadAsync(Library library);
        Task SaveAsync(Library library);
    }
}
