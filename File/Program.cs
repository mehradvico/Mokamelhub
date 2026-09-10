using Application.Configures;
using Application.Services.Accounting.UserTokenSrv.Iface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MehradVico.Api", Version = "v1" });
    var security = new OpenApiSecurityScheme
    {
        Name = "JWT Auth",
        Description = Resource.Notification.PleaseEnterTheToken,
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
                    { security , new string[]{ } }
                });


});
builder.Services.AddControllers().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
builder.Services.AddDbContext<IDataBaseContext, DataBaseContext>(p => p.UseSqlServer(builder.Configuration["connection"], x => x.UseNetTopologySuite()));
builder.Services.AddApplicationServices();

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

builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
         .AddJwtBearer(configureOptions =>
         {
             configureOptions.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidIssuer = builder.Configuration["JWtConfig:issuer"],
                 ValidAudience = builder.Configuration["JWtConfig:audience"],
                 IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(
                         builder.Configuration["JWtConfig:Key"]
                         ?? throw new InvalidOperationException(
                             "JWtConfig:Key is not configured."
                         )
                     )
                 ),
                 ValidateIssuerSigningKey = true,
                 ValidateLifetime = true,

             };
             configureOptions.SaveToken = true;
             configureOptions.Events = new JwtBearerEvents
             {
                 OnAuthenticationFailed = context =>
                 {
                     var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<IOnTokenNotValidService>();
                     return tokenValidatorService.Execute(context);

                 },
                 OnTokenValidated = context =>
                 {
                     var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<IOnTokenValidatedService>();
                     return tokenValidatorService.Execute(context);

                 },
                 OnChallenge = context =>
                 {
                     var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<IOnTokenChallenge>();
                     return tokenValidatorService.Execute(context);
                 },
                 OnMessageReceived = context =>
                 {
                     return Task.CompletedTask;

                 },
                 OnForbidden = context =>
                 {
                     return Task.CompletedTask;

                 }
             };

         });
builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
});

var app = builder.Build();

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

app.Use(async (context, next) =>
{
    var host = context.Request.Host.Host;
    var path = context.Request.Path;
    if (host.Equals("file.mokamelhub.com", StringComparison.OrdinalIgnoreCase) &&
        path.StartsWithSegments("/swagger"))
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        return;
    }
    await next();
});
app.UseStaticFiles();
app.UseCors("AppCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.DefaultModelsExpandDepth(-1);
    });
}
app.MapControllers();
app.Run();
