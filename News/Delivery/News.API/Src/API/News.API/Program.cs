using _4EPlatform.PermissionsGroupingChecker;
using _4EPlatform.PremessionRegister;
using _4EPlatform_News.GrpClientsConfig;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using News.Application.Services;

using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
try
{

    // Add services to the container.

    // Add unit of work & GRepo
    builder.Services.AddUnitOfWorkRepository();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowCors",
            builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000")
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
            });
    });
    builder.Services.AddHostedService<AddPermissionsMiddleware>();

    builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagementUrl"]);
    //builder.Services.AddScoped<PermissionsCheckerMiddleware, PermissionsCheckerMiddleware>();
    builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();
    builder.Services.AddDiServices();
    builder.Services.AddDbConfig(builder.Configuration);
    builder.Host.UseSerilog((context, configuration) => configuration
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext().
            ReadFrom.Configuration(context.Configuration));

    builder.Services.AddAuthentication("Bearer")
          .AddJwtBearer("Bearer", options =>
          {
              // the name of your api resources   
              options.Audience = "UserManagementServer";
              /// identity server url                    
              options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
              options.RequireHttpsMetadata = false;
          });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ResourceOwnerPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireClaim("scope", "4EMicroServices"); // Require scope associated with Resource Owner Password token
        });

        options.AddPolicy("ClientCredentialsPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireClaim("scope", "CPAggregatorAPI"); // Require scope associated with Client Credentials token
        });
    });
    builder.Services.AddLocalizationOptions();
    builder.Services.AddRateLimitings(builder.Configuration);
    // Add grpc config to serviceCollection
    builder.Services.AddGrpc();
    //builder.Services.AddGrpcReflection();

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(IPAddress.Any, 7245, o => o.Protocols = HttpProtocols.Http2);

        // ADDED THIS LINE to fix the problem
        options.Listen(IPAddress.Any, 5299, o => o.Protocols = HttpProtocols.Http1);

    });
    builder.Services.Configure<FormOptions>(option =>
    {
        option.MultipartBoundaryLengthLimit = 20 * 1024 * 1024;
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    #region Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "News APIs Reference",
        });

        c.AddSecurityDefinition(
        "token",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization
        }
            );
        c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
        }
            );
    });
    #endregion



    builder.Services.AddControllers();
    builder.Services.AddFluentValidation();

    var app = builder.Build();
    app.MapGrpcService<NewsService>();

    app.UseMiddleware<GlobalExceptionHandler>();
    // Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
        RequestPath = "/wwwroot"
    });

    app.UseCors("AllowCors");
    app.ApplyAutoMigration();

    // Add GrpcServices
    app.AddMapGrpcServices();
    app.UseStaticFiles();

    //app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    //app.UseMiddleware<PermissionsCheckerMiddleware>();
    app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();
    app.MapControllers();
    //Add support to logging request with SERILOG
    app.UseSerilogRequestLogging();

    app.Run();
}
catch (Exception)
{

    throw;
}
