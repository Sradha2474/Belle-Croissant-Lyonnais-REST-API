using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Buffers.Text;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Session1Api
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No Authorization header is found");
            }
            try
            {
                var authheader = Request.Headers["Authorization"].ToString();
                if (!authheader.StartsWith("Basic ")){ throw new Exception($"Not Valid Auth Header {authheader}"); }
                var base64String = authheader.Substring("Basic ".Count()).Trim();
                var ReadableString = Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
                var credentials = ReadableString.Split(":");
                if (credentials[0] == "staff" && credentials[1] == "WSA2025")
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name , credentials[0])
                    };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principals = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principals, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                } else
                    throw new Exception("Credentials Incorrect");

            }catch(Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
            throw new NotImplementedException();
        }
    }
    
}
