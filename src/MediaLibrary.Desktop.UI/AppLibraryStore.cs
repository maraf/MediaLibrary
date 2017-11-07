using MediaLibrary.Properties;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    public class AppLibraryStore : ILibraryStore
    {
        private readonly Settings settings;
        private readonly XmlStore inner;
        private readonly IChangeTracker changeTracker;

        internal AppLibraryStore(Settings settings, XmlStore inner, IChangeTracker changeTracker)
        {
            Ensure.NotNull(settings, "settings");
            Ensure.NotNull(inner, "inner");
            Ensure.NotNull(changeTracker, "changeTracker");
            this.settings = settings;
            this.inner = inner;
            this.changeTracker = changeTracker;
        }

        public Task LoadAsync(Library library)
        {
            return inner.LoadAsync(library);
        }

        public async Task SaveAsync(Library library)
        {
            await inner.SaveAsync(library);

            settings.DefaultFilePath = library.Configuration.FilePath;
            settings.Save();

            changeTracker.Clear();
        }
    }
}
