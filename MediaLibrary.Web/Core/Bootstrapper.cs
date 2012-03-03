using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using MediaLibrary.Web.Models.Domain;
using MediaLibrary.Web.Models.Repository;

namespace MediaLibrary.Web.Core
{
    public static class Bootstrapper
    {
        public static void Initialize()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>();            

            container.RegisterControllers();
            container.RegisterType<IRepository<UserAccount>, UserAccountRepository>();
            container.RegisterType<IRepository<Database>, DatabaseRepository>();
            container.RegisterType<IRepository<DatabaseRevision>, DatabaseRevisionRepository>();
            container.RegisterType<IAuthProvider, FormsAuthProvider>();

            return container;
        }
    }
}