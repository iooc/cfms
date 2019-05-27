using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Cfms.Authentication
{
    /// <summary>
    /// IServiceCollection 接口扩展函数
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCfmsAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OAuthDefaults.DisplayName;
            })
            .AddCookie()
            .AddOAuth(OAuthDefaults.DisplayName, options =>
            {
                options.ClientId = "oauth.code";
                options.ClientSecret = "secret";
                options.AuthorizationEndpoint = "/connect/authorize";
                options.TokenEndpoint = "/connect/token";
                options.CallbackPath = "/signin-oauth";
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.SaveTokens = true;
                // 事件执行顺序 ：
                // 1.创建Ticket之前触发
                options.Events.OnCreatingTicket = context => Task.CompletedTask;
                // 2.创建Ticket失败时触发
                options.Events.OnRemoteFailure = context => Task.CompletedTask;
                // 3.Ticket接收完成之后触发
                options.Events.OnTicketReceived = context => Task.CompletedTask;
                // 4.Challenge时触发，默认跳转到OAuth服务器
                // options.Events.OnRedirectToAuthorizationEndpoint = context => context.Response.Redirect(context.RedirectUri);
            });

            return services;
        }
    }
}
