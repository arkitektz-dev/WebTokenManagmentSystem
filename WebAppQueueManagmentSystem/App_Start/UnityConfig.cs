using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using WebAppQueueManagmentSystem.BLL.Counter;
using WebAppQueueManagmentSystem.BLL.Token;
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
            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}