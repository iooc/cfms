using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cfms.IndentityServer
{
    public static class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = false,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                }
            };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            //自定义身份资源
            var customProfile = new IdentityResource(
                name: "custom.profile",
                displayName: "Custom profile",
                claimTypes: new[] { "name", "email", "status" });
            return new List<IdentityResource>
            {
                //IdentityServer自带的资源
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                customProfile
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //具有单一范围的简单API（在这种情况下，范围名称与api名称相同）
                new ApiResource("api1", "My API"),
                // 扩展版本，更多的设置选项
                new ApiResource
                {
                    Name = "api2",

                    // 用于内部校验的密码
                    // ApiSecrets是用于内部校验的，API的名称(Name)和密码(ApiSecrets)可以用于认证
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // 在访问令牌中包含以下内容（除了subject ID）
                    // UserClaims表示用户声明
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email },

                    // // 该API定义了两个范围
                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "api2.full_access",
                            DisplayName = "Full access to API 2",
                        },
                        new Scope
                        {
                            Name = "api2.read_only",
                            DisplayName = "Read only access to API 2"
                        }
                    }
                }
            };
        }
    }
}
