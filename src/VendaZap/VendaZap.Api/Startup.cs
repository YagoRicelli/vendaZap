using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SimpleInjector;
using System.Text;
using VendaZap.Api.Filters;
using VendaZap.Comum.Configuracoes;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;

namespace VendaZap.Api
{
    public class Startup
    {
        private Container container = new Container();
        private readonly string CorsAllowSpecificOrigins = "_corsAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            this.container.Options.ResolveUnregisteredConcreteTypes = false;
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VendaZap API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            services.Configure<ConfiguracaoAppSettings>(this.Configuration.GetSection("Configuracoes"));
            var chaveJWT = this.Configuration.GetSection("Configuracoes")["ChaveJWT"];
            var key = Encoding.ASCII.GetBytes(chaveJWT);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSimpleInjector(this.container, options =>
            {
                options.AddAspNetCore().AddControllerActivation();
            });

            services.AddScoped<ServicoAssinaturaFilter>();
            this.ConfigurarDominios(services);
        }

        private void ConfigurarDominios(IServiceCollection services)
        {
            Usuarios.Infra.Dependencias.Resolver(this.container);
            Comum.Infra.Dependencias.Resolver(this.container);


            Usuarios.Infra.Banco.Registrar(services, this.Configuration.GetConnectionString("Usuarios"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Erro inesperado, tente novamente mais tarde.");
                    });
                });
            }

            app.UseSimpleInjector(this.container);
            app.UseRouting();
            app.UseCors(CorsAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
            });

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.RoutePrefix = string.Empty;
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            this.container.Verify();
        }
    }
}
