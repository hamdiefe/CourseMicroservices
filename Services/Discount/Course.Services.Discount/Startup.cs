using Course.Services.Discount.Services;
using Course.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Course.Services.Discount
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = Configuration["IdendityServerUrl"];
                options.Audience = "resource_discount";
                options.RequireHttpsMetadata = false;
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddHttpContextAccessor();

            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddScoped<IDiscountService, DiscountService>();

            //User zorunlu olduðu için user authorization policy oluþturuldu ve addcontrollers metotuna verildi.
            var requiredAuthorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requiredAuthorizationPolicy));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Course.Services.Discount", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course.Services.Discount v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
