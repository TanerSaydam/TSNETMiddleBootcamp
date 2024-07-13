using Microsoft.AspNetCore.OData;
using TodoCleanArchitecture.Application;
using TodoCleanArchitecture.Infrastructure;
using TodoCleanArchitecture.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddControllers().AddOData(action =>
{
    action.EnableQueryFeatures();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<MyExceptionHandler>().AddProblemDetails();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region SeedData
//app.MapGet("SeedData", async (ITodoRepository todoRepository) =>
//{
//    for (int i = 0; i < 10000; i++)
//    {
//        Faker faker = new();
//        Todo todo = new()
//        {
//            Work = faker.Lorem.Word(),
//            DeadLine = faker.Date.BetweenDateOnly(new DateOnly(2024, 07, 13), new DateOnly(2024, 12, 31))
//        };
//        await todoRepository.CreateAsync(todo, default);
//    }

//    return Results.Created();
//});
#endregion

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
