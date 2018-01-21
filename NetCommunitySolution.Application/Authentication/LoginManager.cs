using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Abp.Application.Services;
using System.Threading.Tasks;
using NetCommunitySolution.Authentication.Dto;

namespace NetCommunitySolution.Authentication
{

    public class LoginManager : SignInManager<CustomerDto, int>, IApplicationService
    {
        private readonly UserManager<CustomerDto, int> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        public LoginManager(UserManager<CustomerDto, int> userManager, AuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            this._userManager = userManager;
            this._authenticationManager = authenticationManager;
        }

        public override Task SignInAsync(CustomerDto user, bool isPersistent, bool rememberBrowser)
        {
            return base.SignInAsync(user, isPersistent, rememberBrowser);
        }        
    }
}
