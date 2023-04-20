using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Wifi.SD.Core.Entities;
using Wifi.SD.Core.Services;

namespace SD.Application.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService userService;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                           ILoggerFactory logger,
                                           UrlEncoder encoder,
                                           ISystemClock clock,
                                           IUserService userService) : base(options, logger, encoder, clock)
        {
            this.userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization")) 
            {
                Response.Headers.Add("WWW-Authenticate", "Basic realm=\"\"");
                return await Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }

            User user;
            try
            {
                var autHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                // base64 in byte konvertieren
                var credentialByte = Convert.FromBase64String(autHeader.Parameter);

                //byte Array in string umwandeln
                var credentials = Encoding.UTF8.GetString(credentialByte).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];

                user = await this.userService.AuthenticateAsync(username, password);

            }
            catch
            {
                return AuthenticateResult.Fail("No valid Authorization Header");
            }

            if (user == null)
            {
                return AuthenticateResult.Fail("Invalid Username or Password!");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }

        

    }
}
