using DomainDrivenDesign.Application;
using DomainDrivenDesign.Domain.ShoppingCarts;
using DomainDrivenDesign.Domain.ShoppingCarts.Dtos;
using DomainDrivenDesign.Infrastructure;
using DomainDrivenDesign.Infrastructure.Backgrounds;
using DomainDrivenDesign.Infrastructure.Context;
using DomainDrivenDesign.Infrastructure.Options;
using DomainDrivenDesign.WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using StmpOptions = DomainDrivenDesign.Domain.Options.StmpOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("myconfig.json", optional: true, reloadOnChange: true);

builder.Services.Configure<StmpOptions>(builder.Configuration.GetSection("SMTP"));

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

var srv = builder.Services.BuildServiceProvider();
var jwt = srv.GetRequiredService<IOptions<Jwt>>().Value;

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddHttpContextAccessor();

builder.Services.CreateServiceTool();

builder.Services.AddHostedService<OutboxBackground>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.UseExceptionHandler();

app.UseResponseCompression();

Extensions.DatabaseMigrate(app);
Extensions.CreateFirstAdminUser(app).Wait();

app.MapGet("/test", (IOptionsSnapshot<StmpOptions> options) =>
{
    return Results.Ok(options.Value);
});


app.MapGet("/cycleTest", (ApplicationDbContext context) =>
{
    var response = context.Set<ShoppingCart>().Include(p => p.Product).Select(s => new ShoppingCartDto(
        s.Id.Value,
        s.ProductId.Value,
        s.Product!.Name.Value,
        s.Quantity.Value,
        s.Product.Price.Value)).ToList();

    var response2 = (from c in context.Set<ShoppingCart>()
                     join p in context.Products on c.ProductId equals p.Id
                     select new
                     {
                         Id = c.Id.Value,
                         ProductId = p.Id.Value,
                         ProductName = p.Name.Value,
                         Quantity = c.Quantity.Value,
                         ProductPrice = p.Price.Value

                     }).ToList();
    return response;
});

app.Run();
