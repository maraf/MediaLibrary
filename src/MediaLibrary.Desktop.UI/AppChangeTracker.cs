using MediaLibrary.ViewModels.Services;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    public class AppChangeTracker : IChangeTracker
    {
        public bool Has { get; private set; }
        public event Action Added;
        public event Action Cleared;

        public void Clear()
        {
            Has = false;
            Cleared?.Invoke();
        }

        public void UpdateModel(IModelDefinition definition, IModelValueProvider model, IModelValueGetter newState)
        {
            CopyModelValueProvider copy = new CopyModelValueProvider(definition, true);
            copy.Update(model, newState);

            Has = true;
            Added?.Invoke();
        }
    }
}
