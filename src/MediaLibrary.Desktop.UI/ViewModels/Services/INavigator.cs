﻿using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Services
{
    public interface INavigator
    {
        Task SelectLibraryAsync();

        Task LibraryAsync(Library library);
        Task LibraryConfigurationAsync(Library library);
        Task CreateMovieAsync(Library library);
        Task EditMovieAsync(Library library, IKey movieKey);
        Task<IEnumerable<IKey>> SelectMoviesAsync(Library library);

        Task<bool> ConfirmAsync(string message);
    }
}
