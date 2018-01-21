using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using NetCommunitySolution.Customers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace NetCommunitySolution.Providers
{

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId;
            string clientSecret;
            context.TryGetBasicCredentials(out clientId, out clientSecret);

            if (clientId == "1234"
                && clientSecret == "5678")
            {
                context.Validated(clientId);
            }

            await base.ValidateClientAuthentication(context);
        }


        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            Uri expectedRootUri = new Uri(context.Request.Uri, "/");

            if (expectedRootUri.AbsoluteUri == context.RedirectUri)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }

        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }

        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            return base.ValidateAuthorizeRequest(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var customerService = Abp.Dependency.IocManager.Instance.Resolve<ICustomerService>();

            var loginResult = await customerService.ValidateAsyncCustomer(context.UserName, context.Password);

            switch (loginResult.Result)
            {
                case Authentication.Dto.LoginResults.Deleted:
                    context.SetError("invalid_grant", "该用户已经被冻结");
                    break;
                case Authentication.Dto.LoginResults.NotRegistered:
                    context.SetError("invalid_grant", "该用户不存在");
                    break;
                case Authentication.Dto.LoginResults.Unauthorized:
                    context.SetError("invalid_grant", "该用户不能登录");
                    break;
                case Authentication.Dto.LoginResults.WrongPassword:
                    context.SetError("invalid_grant", "密码错误");
                    break;
                case Authentication.Dto.LoginResults.Successful:
                    var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, loginResult.Customer.Id.ToString()));
                    var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
                    context.Validated(ticket);

                    await base.GrantResourceOwnerCredentials(context);
                    break;
                default:
                    context.SetError("invalid_grant", "未知错误");
                    break;
            }
            return;

        }


    }
}
