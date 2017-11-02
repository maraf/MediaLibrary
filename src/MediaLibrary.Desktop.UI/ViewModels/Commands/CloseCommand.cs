using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Commands
{
    public class CloseCommand : Command
    {
        private readonly INavigatorContext context;

        public CloseCommand(INavigatorContext context)
        {
            Ensure.NotNull(context, "context");
            this.context = context;
        }

        public override bool CanExecute() => true;
        public override void Execute() => context.Close();
    }
}
