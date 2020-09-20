// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FancyAppV2.STS
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("dataeventrecordsscope",new []{ "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin" , "dataEventRecords.user" } )
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("dataEventRecords", "Scope for the dataEventRecords ApiResource",
                    new List<string> { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user"}),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("theApi")
                {
                    ApiSecrets =
                    {
                        new Secret("ApiSecret".Sha256())
                    },
                    Scopes = new List<string> {"dataEventRecords"},
                    UserClaims = { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }
                },
            };
        }

        public static IEnumerable<Client> GetClients(IConfigurationSection stsConfig)
        {
            // TODO use configs in app
            //var yourConfig = stsConfig["ClientUrl"];

            return new List<Client>
            {
                new Client
                {
                    ClientName = "vuejs_code_client",
                    ClientId = "vuejs_code_client",
                    AccessTokenType = AccessTokenType.Reference,
                    // RequireConsent = false,
                    AccessTokenLifetime = 330, // 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,

                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44359",
                        "https://localhost:44359/callback.html",
                        "https://localhost:44359/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44359/",
                        "https://localhost:44359"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44359"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "dataEventRecords",
                        "dataeventrecordsscope",
                        "role",
                        "profile",
                        "email"
                    }
                }
            };
        }
    }
}