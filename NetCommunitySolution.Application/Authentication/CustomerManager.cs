using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Application.Services;
using Microsoft.AspNet.Identity;
using NetCommunitySolution.Authentication.Dto;

namespace NetCommunitySolution.Authentication
{

    public class CustomerManager : UserManager<CustomerDto, int>, IApplicationService
    {
        public CustomerManager(CustomerStore store) : base(store)
        {
        }

        public override Task<ClaimsIdentity> CreateIdentityAsync(CustomerDto user, string authenticationType)
        {
            return base.CreateIdentityAsync(user, authenticationType);
        }

        public override Task<CustomerDto> FindAsync(string userName, string password)
        {
            return base.FindAsync(userName, password);
        }
        
    }
}
