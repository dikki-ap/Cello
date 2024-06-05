using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Cello.Web.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.Authority = configuration.GetValue<string>("jwt:authority");
                opt.Audience = configuration.GetValue<string>("jwt:audience");
                opt.RequireHttpsMetadata = configuration.GetValue<bool>("jwt:requireHttpsMetadata");
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = configuration.GetValue<string>("jwt:authority"),
                    ValidAudience = configuration.GetValue<string>("jwt:audience"),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        }
    }
}
