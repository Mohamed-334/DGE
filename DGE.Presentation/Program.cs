using DGE.Core;
using DGE.Domain.Entities;
using DGE.Domain.Shared.EmailModels;
using DGE.Domain.Shared.JwtModels;
using DGE.Infrastructure;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.Context.Interceptors;
using DGE.Infrastructure.Hubs;
using DGE.Infrastructure.Seeder;
using DGE.Presentation.Shared.Csrf;
using DGE.Presentation.Shared.ExceptionHandling;
using DGE.Presentation.Shared.Hangfire;
using DGE.Presentation.Shared.HealthChecks;
using DGE.Presentation.Shared.RateLimiting;
using DGE.Service;
using DGE.Service.Shared.Security;
using Asp.Versioning;
using Hangfire;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Globalization;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace DGE.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            #region API Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            #endregion

            #region Swagger Config
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Base Project",
                    Version = "v1",
                    Description = "API Documentation for Base Project"
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securityScheme,
                        new string[] {}
                    }
                };
                c.AddSecurityRequirement(securityRequirement);
            });

            #endregion

            #region Context Registration

            // Register the DbContext with the connection string from configuration
            builder.Services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var interceptor = sp.GetRequiredService<LoggerSaveChangesInterceptor>();
                options.UseSqlServer(builder.Configuration.GetConnectionString("DGE"));
                options.AddInterceptors(interceptor);
            });

            #endregion

            #region Identity Config
            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.AllowedUserNameCharacters = null;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailTokenProvider<User>>(TokenOptions.DefaultEmailProvider);
            #endregion

            #region Application Cookies Config
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = string.Empty;
                options.AccessDeniedPath = string.Empty;
                options.SlidingExpiration = true;
            });
            #endregion

            #region Jwt Authentication Config
            var jwtSettings = new JwtSettings();
            builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
            builder.Services.AddSingleton(jwtSettings);
            var tokenEncryptionKey = TokenEncryptor.ParseKey(jwtSettings.EncryptionKey);

            var authenticationBuilder = builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidAudience = jwtSettings.Audience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifeTime,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        var encryptedToken = !string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ")
                            ? authHeader.Substring("Bearer ".Length).Trim()
                            : context.Request.Cookies["AccessToken"];

                        // A token that fails to decrypt is left unset, so the request is treated as unauthenticated (401)
                        if (TokenEncryptor.TryDecrypt(encryptedToken, tokenEncryptionKey, out var token))
                            context.Token = token;

                        return Task.CompletedTask;
                    }
                };
            });

            // External logins are registered only when configured; an empty ClientId fails options validation on every request
            if (!string.IsNullOrEmpty(builder.Configuration["Authentication:Google:ClientId"]))
            {
                authenticationBuilder.AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? string.Empty;
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
                    options.CallbackPath = "/api/Authentication/GoogleAuthenticationCallBack";
                    options.SaveTokens = true;
                });
            }

            if (!string.IsNullOrEmpty(builder.Configuration["Authentication:LinkedIn:ClientId"]))
            {
                authenticationBuilder.AddOAuth("LinkedIn", options =>
                {
                    var linkedInConfig = builder.Configuration.GetSection("Authentication:LinkedIn");

                    options.ClientId = linkedInConfig["ClientId"] ?? string.Empty;
                    options.ClientSecret = linkedInConfig["ClientSecret"] ?? string.Empty;
                    options.CallbackPath = "/api/Authentication/LinkedInAuthenticationCallBack";

                    options.AuthorizationEndpoint = "https://www.linkedin.com/oauth/v2/authorization";
                    options.TokenEndpoint = "https://www.linkedin.com/oauth/v2/accessToken";
                    options.UserInformationEndpoint = "https://api.linkedin.com/v2/userinfo";

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");

                    options.SaveTokens = true;

                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    options.ClaimActions.MapJsonKey("urn:linkedin:picture", "picture");
                    options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");

                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = async context =>
                        {
                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();

                            var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                            context.RunClaimActions(user.RootElement);
                        },
                        OnRemoteFailure = context =>
                        {
                            context.Response.Redirect("/api/auth/linkedin-error?error=" + context.Failure.Message);
                            context.HandleResponse();
                            return Task.CompletedTask;
                        }
                    };
                });
            }


            #endregion

            #region Dependency Injection
            builder.Services.AddInfrastructureDependencies()
                .AddServiceDependencies()
                .AddCoreDependencies();
            #endregion

            #region Localization configuration
            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalization(opt => opt.ResourcesPath = "");

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("de"),
                    new CultureInfo("fr"),
                    new CultureInfo("ar")
                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            #endregion

            #region AllowCORS
            var CorsConfigName = builder.Configuration.GetSection("CorsConfig")["CorsConfigName"]!;

            // Credentials (the AccessToken cookie) are allowed, so only explicitly trusted origins may call the API.
            // "null" and "*" are rejected: "null" is sent by sandboxed iframes and file:// pages and would let any site in.
            var allowedOrigins = (builder.Configuration.GetSection("CorsConfig:AllowedOrigins").Get<string[]>() ?? [])
                .Select(origin => origin.Trim().TrimEnd('/'))
                .Where(origin => Uri.TryCreate(origin, UriKind.Absolute, out var uri)
                                 && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CorsConfigName,
                    policy =>
                    {
                        policy.WithOrigins(allowedOrigins);
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowCredentials();
                    });
            });
            #endregion

            #region Email Config
            var emailSettings = new EmailSettings();
            builder.Configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            builder.Services.AddSingleton(emailSettings);

            #endregion

            #region SignalR Services
            builder.Services.AddSignalR();
            #endregion

            #region Response Compression
            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            builder.Services.Configure<BrotliCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);
            builder.Services.Configure<GzipCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);
            #endregion

            #region Rate Limiting
            builder.Services.AddAppRateLimiting();
            #endregion

            #region CSRF Protection
            builder.Services.AddAppCsrfProtection();
            #endregion

            #region Exception Handling
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            #endregion

            #region Health Checks
            builder.Services.AddAppHealthChecks();
            #endregion

            #region Hangfire Services
            builder.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DGE"));
            });
            builder.Services.AddHangfireServer();

            #endregion

            var app = builder.Build();

            #region Seeders
            using (var scope = app.Services.CreateScope())
            {
                var Users = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var Roles = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                await RoleSeeder.SeedAsync(Roles);
                await UserSeeder.SeedAsync(Users);
            }
            #endregion

            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (httpContext, _, exception) =>
                    exception is not null || httpContext.Response.StatusCode >= StatusCodes.Status500InternalServerError
                        ? LogEventLevel.Error
                        : httpContext.IsHealthCheckRequest() ? LogEventLevel.Verbose : LogEventLevel.Information;
            });

            #region Localization middleware
            var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions!.Value);
            #endregion

            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Base Architecture v1"));
                app.MapScalarApiReference("scalar", options =>
                {
                    options.WithOpenApiRoutePattern(
                        "/swagger/{documentName}/swagger.json");
                });
            }

            // Compressing responses that carry secrets (tokens, CSRF token) next to request-reflected data over HTTPS
            // opens them to BREACH-style attacks, so the authentication endpoints are served uncompressed
            app.UseWhen(context => !(context.Request.Path.Value?.Contains("/Authentication/", StringComparison.OrdinalIgnoreCase) ?? false),
                        branch => branch.UseResponseCompression());
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseCors(builder.Configuration.GetSection("CorsConfig")["CorsConfigName"]!);

            app.UseAuthentication();
            app.UseAuthorization();

            // After authentication so user-based policies can read the user id
            app.UseRateLimiter();

            // Validates X-CSRF-TOKEN for state-changing requests authenticated by the AccessToken cookie
            app.UseAppCsrfProtection();

            #region SignalR Middleware

            app.MapHub<NotificationHub>("/notificationHub");

            #endregion

            #region Hangfire Dashboard
            app.UseHangfireDashboard("/dashboard", new DashboardOptions
            {
                Authorization = new[]
                {
                    new HangfireDashboardAuthorizationFilter("Admin", app.Environment.IsDevelopment())
                }
            });
            #endregion

            #region Health Checks Endpoints
            app.MapAppHealthChecks();
            #endregion

            app.MapControllers();

            app.Run();
        }
    }
}