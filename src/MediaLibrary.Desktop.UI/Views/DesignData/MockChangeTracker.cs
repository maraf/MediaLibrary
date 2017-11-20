using MediaLibrary.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.PresentationModels;

namespace MediaLibrary.Views.DesignData
{
    public class MockChangeTracker : IChangeTracker
    {
        public bool Has => true;

        public event Action Added;
        public event Action Cleared;

        public void Clear()
        { }

        public void Remove(MovieCollection collection, Movie model)
        { }

        public void UpdateModel(IModelDefinition definition, IModelValueProvider model, IModelValueGetter newState)
        { }
    }
}
