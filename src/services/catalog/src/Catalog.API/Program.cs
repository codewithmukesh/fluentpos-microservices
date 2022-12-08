using BuildingBlocks.CQRS;
using BuildingBlocks.Logging;
using FluentPOS.Catalog.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddCommonSerilog(builder.Environment);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.EnableAnnotations());

// Register BB Services
builder.Services.UseCommonMediatR(typeof(CatalogRoot).Assembly, enableLoggingBehavior: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => o.DefaultModelsExpandDepth(-1));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello From Catalog Service!");
app.Run();
