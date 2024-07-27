using DomainDrivenDesign.Application;
using DomainDrivenDesign.Infrastructure;
using DomainDrivenDesign.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    //bu boyut olarak daha küçük turuyor ama daha fazla zaman alýyor
    //options.Providers.Add<GzipCompressionProvider>(); 

    //Bu boyut olarak normal ama süresi daha uzun 
    //options.Providers.Add<BrotliCompressionProvider>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.UseResponseCompression();

Extensions.DatabaseMigrate(app);
Extensions.CreateFirstAdminUser(app).Wait();

app.Run();
