using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace eCommerce.GatewaySolution.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            var jwtOptionsSection = builder.Configuration.GetSection("ApiSettings:JwtOptions");

            var secret = jwtOptionsSection.GetValue<string>("Secret")
                ?? throw new InvalidOperationException("Gateway JWT secret is missing from ApiSettings:JwtOptions:Secret.");
            var issuer = jwtOptionsSection.GetValue<string>("Issuer")
                ?? throw new InvalidOperationException("Gateway JWT issuer is missing from ApiSettings:JwtOptions:Issuer.");
            var audience = jwtOptionsSection.GetValue<string>("Audience")
                ?? throw new InvalidOperationException("Gateway JWT audience is missing from ApiSettings:JwtOptions:Audience.");

            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience
                };
            });

            builder.Services.AddAuthorization();

            return builder;
        }
    }
}
