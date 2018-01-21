using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NetCommunitySolution.Authentication;
using NetCommunitySolution.Authentication.Dto;
using NetCommunitySolution.Domain.Customers;

namespace NetCommunitySolution
{
    [DependsOn(typeof(NetCommunitySolutionCoreModule), typeof(AbpAutoMapperModule))]
    public class NetCommunitySolutionApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                mapper.CreateMap<Customer, CustomerDto>();
            });
            IocManager.Register<IAuthenticationManager, AuthenticationManager>();
            IocManager.Register<IUserStore<CustomerDto, int>, CustomerStore>();
            IocManager.Register<UserManager<CustomerDto, int>, CustomerManager>();
            IocManager.Register<SignInManager<CustomerDto, int>, LoginManager>();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
