using System.Globalization;
using CSharpOrders.Data;
using CSharpOrders.Repositories;
using Microsoft.AspNetCore.Localization;
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
builder.Services.AddScoped<IOrderItemsRepository, EFOrderItemsRepository>();
builder.Services.AddScoped<IProviderRepository, EFProviderRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProjectContext>();
    dbContext.Database.Migrate();
}

var supportedCultures = new[]
{
 new CultureInfo("ru-RU"),
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru-RU"),
    // Formatting numbers, dates, etc.
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});

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
