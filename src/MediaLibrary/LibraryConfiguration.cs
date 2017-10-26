using Neptuo.Observables;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    public class LibraryConfiguration : ObservableModel
    {
        /// <summary>
        /// Gets a library where configuration belongs.
        /// </summary>
        public Library Library { get; private set; }

        /// <summary>
        /// Gets or sets a name of the library.
        /// </summary>
        public string Name
        {
            get { return this.GetValueOrDefault(nameof(Name), (string)null); }
            set { TrySetValue(nameof(Name), value); }
        }

        /// <summary>
        /// Gets or sets a path to file where library is saved.
        /// </summary>
        public string FilePath
        {
            get { return this.GetValueOrDefault(nameof(FilePath), (string)null); }
            set { TrySetValue(nameof(FilePath), value); }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="library"></param>
        internal LibraryConfiguration(Library library)
            : base(library.ConfigurationDefinition)
        { }
    }
}
