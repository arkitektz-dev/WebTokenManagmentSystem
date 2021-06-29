using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using WebAppQueueManagmentSystem.ApiHelpers.Utility;
using WebAppQueueManagmentSystem.BLL.Counter;
using WebAppQueueManagmentSystem.BLL.Token;
using WebAppQueueManagmentSystem.BLL.User;
using WebAppQueueManagmentSystem.Controllers;

namespace WebAppQueueManagmentSystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer(); 
            container.RegisterType<ITokenRepository, TokenRepository>();
            container.RegisterType<ICounterRepository, CounterRepository>();
            container.RegisterType<IApiUtility, ApiUtility>(); 
            container.RegisterType<IUserRepository, UserRepository>(); 

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}