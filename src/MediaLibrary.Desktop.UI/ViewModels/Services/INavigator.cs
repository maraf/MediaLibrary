using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Services
{
    public interface INavigator
    {
        Task Library(Library library);
        Task CreateMovieAsync(Library library);
        Task EditMovieAsync(Library library, IKey movieKey);
    }
}
