﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new ("website", new[] {"website"} ),
                new ("website2", new[] {"website2"} ),
                new ("address2", new[] {"address2"} ),
            };
        
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "My API 1"),
                new ApiScope("api2", "My API 2")
            };
        
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client (from quickstart 1)
                new()
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
        
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                
                // interactive ASP.NET Core MVC client
                new()
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },
        
                    AllowedGrantTypes = GrantTypes.Code,
        
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
        
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
        
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "address",
                        "address2",
                        "website2",
                        "website",
                        "api1",
                    },
                },
                new()
                {
                    ClientId = "blazor",
                    ClientSecrets = { new Secret("secret".Sha256()) },
        
                    AllowedGrantTypes = GrantTypes.Code,
        
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
        
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
        
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "address",
                        "api1",
                    },
                }
            };
    }
}