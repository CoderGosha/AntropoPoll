using AntropoPollWebApi.Core.Extensions;
using AntropoPollWebApi.Core.Interfaces;
using AntropoPollWebApi.Core.Services;
using AntropoPollWebApi.Core.Services.Questions;
using AntropoPollWebApi.Core.Settings;
using AntropoPollWebApi.Middleware;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace AntropoPollWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddScoped<IPoll, PollService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ClosedQuestionService>();
            services.AddScoped<ResultService>();

            services.Configure<AuthSettings>(Configuration.GetSection(nameof(AuthSettings)));
            services.AddSingleton<TokenService>();
            var tokenService = services.BuildServiceProvider().GetService<TokenService>();

            services.Configure<AntropoPollSettings>(Configuration.GetSection(nameof(AntropoPollSettings)));

            services.AddTransient<InitAntropoPollService>();
            var initTicketSystemService = services.BuildServiceProvider().GetService<InitAntropoPollService>();
            initTicketSystemService.Init();



            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            #endregion

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            #region Auth

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = false,
                        // строка, представляющая издателя
                        ValidIssuer = "Antropo",
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = false,
                        // установка потребителя токена
                        ValidAudience = "localhost",
                        // будет ли валидироваться время существования
                        ValidateLifetime = false,
                        // установка ключа безопасности
                        IssuerSigningKey = tokenService.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };

                });

            //Включаем по умолчанию
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            #endregion

            services.AddCors(o => o.AddPolicy("CorsDefault", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            var assemblyVersion = Assembly.GetAssembly(typeof(Startup)).GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo()
                    {
                        Title = "Antropo Poll Web Api",
                        Description = $"Antropo Poll Web Api: {assemblyVersion}",
                        Version = "v" + assemblyVersion
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var xmlCorePath = Path.Combine(AppContext.BaseDirectory, "AntropoPollWebApi.Core.xml");
                c.IncludeXmlComments(xmlCorePath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors("CorsDefault");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Antropo Poll Web API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
