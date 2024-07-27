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
    //bu boyut olarak daha k���k turuyor ama daha fazla zaman al�yor
    //options.Providers.Add<GzipCompressionProvider>(); 

    //Bu boyut olarak normal ama s�resi daha uzun 
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
