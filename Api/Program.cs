using Api.Authentication.Torob;
using Api.HangFire;
using Api.Swagger;
using Application.Configures;
using Application.Services.Accounting.UserTokenSrv.Iface;
using Hangfire;
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
    options.AddPolicy("AllowAnyOrigin", policy =>
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
                Encoding.UTF8.GetBytes(builder.Configuration["JWtConfig:key"])
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

app.UseRequestLocalization();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAnyOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseOutputCache();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    c.DefaultModelsExpandDepth(-1);
    c.RoutePrefix = "swagger";
});

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    time = DateTime.Now
}));

app.UseHangfireDashboard("/hangfire");

app.MapControllers();

app.Run();