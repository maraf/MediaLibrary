using MediaLibrary.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Views.DesignData
{
    internal class MockNavigatorContext : INavigatorContext
    {
        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
