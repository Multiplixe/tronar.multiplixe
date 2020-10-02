using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using multiplixe.api.action_filters;
using multiplixe.api.dto.settings;
using multiplixe.api.interfaces;
using multiplixe.api.log_eventos;
using multiplixe.classificador.client;
using multiplixe.enfileirador.client;
using System;
using facebook_dtos = multiplixe.facebook.dto;
using twitchoauth = multiplixe.twitch.oauth;
using twitchping = multiplixe.twitch.ping;
using twitter_dtos = multiplixe.twitter.dto;
using twitteroauth = multiplixe.twitter.oauth;
using usuario = multiplixe.usuarios.client;

namespace multiplixe.api
{
    public class Startup
    {
        private string corsName = "cors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                    options =>
                    {
                        options.AddPolicy(corsName,
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                        });
                    });

            services.AddMvc(o =>
            {
                o.InputFormatters.Add(new XmlSerializerInputFormatter(o));
            });


            services.AddScoped<TwitchValidacaoPingActionFilter>();
            services.AddScoped<TwitchTravaPingDuploActionFilter>();

            var twitterAuthContext = Configuration.GetSection("Twitter").GetSection("OAuth").Get<twitteroauth.dtos.AuthContext>();
            var twitchAuthContext = Configuration.GetSection("Twitch").GetSection("OAuth").Get<twitchoauth.dtos.AuthContext>();
            var twitchPingConfig = Configuration.GetSection("Twitch").GetSection("PingConfig").Get<twitchping.dtos.PingConfig>();

            services.AddHttpClient<twitteroauth.TwitterHttpClient>();
            services.AddHttpClient<twitchoauth.TwitchHttpClient>();

            var empresaSettings = Configuration.GetSection("Empresa").Get<EmpresaSettings>();
            var parametros = Configuration.GetSection("Parametros").Get<Parametros>();

            var facebookLogSettings = Configuration.GetSection("LogEvento").GetSection("Twitter").Get<LogEventoSettings<facebook_dtos.eventos.Evento>>();
            var twitterLogSettings = Configuration.GetSection("LogEvento").GetSection("Twitter").Get<LogEventoSettings<twitter_dtos.eventos.Evento>>();
            var youtubeLogSettings = Configuration.GetSection("LogEvento").GetSection("Youtube").Get<LogEventoSettings<YoutubeEventoTest>>();
            var instagramLogSettings = Configuration.GetSection("LogEvento").GetSection("Instagram").Get<LogEventoSettings<InstagramEventTest>>();


            services.AddSingleton(empresaSettings);
            services.AddSingleton(parametros);
            services.AddSingleton(twitterAuthContext);
            services.AddSingleton(twitchAuthContext);
            services.AddSingleton(twitchPingConfig);
            services.AddSingleton<ILogEventoSettings<facebook_dtos.eventos.Evento>>(facebookLogSettings);
            services.AddSingleton<ILogEventoSettings<twitter_dtos.eventos.Evento>>(twitterLogSettings);
            services.AddSingleton<ILogEventoSettings<YoutubeEventoTest>>(youtubeLogSettings);
            services.AddSingleton<ILogEventoSettings<InstagramEventTest>>(instagramLogSettings);

            services.AddTransient<TwitterLogEventoService>();
            services.AddTransient<FacebookLogEventoService>();
            services.AddTransient<YoutubeLogEventoService>();
            services.AddTransient<InstagramLogEventoService>();

            services.AddTransient<EnfileiradorClient>();
            services.AddTransient<usuario.UsuarioClient>();
            services.AddTransient<usuario.PerfilClient>();
            services.AddTransient<ClassificadorClient>();
            services.AddTransient<RankingClient>();
            
            services.AddTransient<twitchping.PingService>();
            services.AddTransient<twitchoauth.Servico>();
            services.AddTransient<twitteroauth.Servico>();

            services.AddTransient<consultas.Setup>();
            services.AddTransient<consultas.Dashboard>();
            services.AddTransient<consultas.Ranking>();

            services.AddControllers();

            services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer("app", options =>
           {
               options.Authority = "https://securetoken.google.com/multipixel-falkol";
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = "https://securetoken.google.com/multipixel-falkol",
                   ValidateAudience = true,
                   ValidAudience = "multipixel-falkol",
                   ValidateLifetime = true
               };
           })
           .AddJwtBearer("twitch", options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(twitchAuthContext.ExtensionSecret)),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true
               };
           })
           .AddJwtBearer("external", options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("MWU1ZTNlNTU4MjdlNDlmYzhlYjQ4ZTI4NzFhY2U5Mzk=")),
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = false,
                   ValidIssuer = "multiplixe",
                   ValidAudience = "multiplixe-external"
               };
           });

            services.AddAuthorization(options =>
            {
                var twitchPolicyBuilder = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .AddAuthenticationSchemes("twitch");

                options.AddPolicy("twitch", twitchPolicyBuilder.Build());


                var appPolicyBuilder = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .AddAuthenticationSchemes("app");

                options.AddPolicy("app", appPolicyBuilder.Build());


                var externalPolicyBuilder = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .AddAuthenticationSchemes("external");

                options.AddPolicy("external", externalPolicyBuilder.Build());


            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(corsName);

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
