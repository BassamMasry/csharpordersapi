using CSharpOrders.Data;
using CSharpOrders.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();

var DBConnectionString = builder.Configuration.GetConnectionString("ProjectContext")
                         ?? throw new ArgumentException("DB connection string cannot be null");
builder.Services.AddDbContext<ProjectContext>(options => options.UseMySQL(DBConnectionString));

builder.Services.AddScoped<IOrdersRepository, EFOrdersRepository>();
builder.Services.AddScoped<IProviderRepository, EFProviderRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProjectContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
