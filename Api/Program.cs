using Api.Authentication.Torob;
using Api.HangFire;
using Api.Swagger;
using Application.Configures;
using Application.Services.Accounting.UserTokenSrv.Iface;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Interface;
using System.Text;
using Utility.BackgroundTask.Iface;
using Utility.ExternalRequest.Iface;
using Utility.ExternalRequest.Service;
using Utility.Reflection;
using Utility.Reflection.Iface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOutputCache();
builder.Services.AddSignalR();

builder.Services.AddDbContext<IDataBaseContext, DataBaseContext>(p =>
    p.UseSqlServer(
        builder.Configuration["connection"],
        x => x.UseNetTopologySuite()
    )
);

builder.Services.AddApplicationServices();

builder.Services.AddScoped<IRestSharpApi, RestSharpApi>();
builder.Services.AddScoped<IBackgroundTask, HangFireSchedule>();
builder.Services.AddScoped<IControllerActionDiscoveryService, ControllerActionDiscoveryService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AppCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://panel.mokamelhub.com",
                "https://mokamelhub.com",
                "https://www.mokamelhub.com",
                "http://localhost:3000",
                "http://localhost:3001",
                "http://localhost:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MehradVico.Api.xml"), true);

    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Vico.Api",
        Version = "v2",
    });

    var security = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(security.Reference.Id, security);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { security, Array.Empty<string>() }
    });

    c.OperationFilter<AddRequiredHeaderParameter>();
    c.SchemaFilter<AddSwaggerSchemaFilter>();
    c.SchemaFilter<EnumSchemaFilter>();
    c.DocumentFilter<AlphabeticalTagsDocumentFilter>();
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(configureOptions =>
    {
        configureOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWtConfig:issuer"],
            ValidAudience = builder.Configuration["JWtConfig:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["JWtConfig:key"]
                    ?? throw new InvalidOperationException(
                        "JWtConfig:key is not configured."
                    )
                )
            ),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true
        };

        configureOptions.SaveToken = true;

        configureOptions.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var service = context.HttpContext.RequestServices
                    .GetRequiredService<IOnTokenNotValidService>();

                return service.Execute(context);
            },

            OnTokenValidated = context =>
            {
                var service = context.HttpContext.RequestServices
                    .GetRequiredService<IOnTokenValidatedService>();

                return service.Execute(context);
            },

            OnChallenge = context =>
            {
                var service = context.HttpContext.RequestServices
                    .GetRequiredService<IOnTokenChallenge>();

                return service.Execute(context);
            },

            OnMessageReceived = context => Task.CompletedTask,

            OnForbidden = context => Task.CompletedTask
        };
    })
    .AddScheme<TorobAuthenticationOptions, TorobAuthenticationHandler>(
        TorobAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
            builder.Configuration
                .GetSection("Torob:Authentication")
                .Bind(options);
        }
    );

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddHangfire(configuration =>
    configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration["connection"], new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        })
);

builder.Services.AddHangfireServer();

var app = builder.Build();

// Defense in depth: individual services already catch their own exceptions and
// return ex.Message inside a BaseResultDto (a separate, much larger cleanup on
// its own). This only covers whatever slips past that — e.g. an unhandled
// exception thrown directly in a controller/middleware — so it never reaches the
// client as a raw stack trace outside Development.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var exceptionFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
            if (exceptionFeature?.Error != null)
            {
                context.RequestServices.GetRequiredService<ILogger<Program>>()
                    .LogError(exceptionFeature.Error, "Unhandled exception on {Path}", exceptionFeature.Path);
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                System.Text.Json.JsonSerializer.Serialize(
                    new Application.Common.Dto.Result.BaseResultDto(
                        isSuccess: false,
                        val: Resource.Notification.Unsuccess)));
        });
    });
}

app.UseRequestLocalization();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AppCorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseOutputCache();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        c.DefaultModelsExpandDepth(-1);
        c.RoutePrefix = "swagger";
    });

    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    time = DateTime.Now
}));

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
});

app.MapControllers();

app.Run();
