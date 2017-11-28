using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI.Controls
{
    /// <summary>
    /// A that want's to be notified when a <see cref="UserFieldPresenter"/> is attached to the tree of objects.
    /// </summary>
    public interface IUserFieldPresenterRegister
    {
        /// <summary>
        /// Sets <paramref name="presenter"/> to be presenter of field <paramref name="identifier"/>.
        /// </summary>
        /// <param name="identifier">A field identifier.</param>
        /// <param name="presenter">A field presenter.</param>
        void Add(string identifier, UserFieldPresenter presenter);
    }
}
