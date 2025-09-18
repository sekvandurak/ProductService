using ProductService.Application;
using ProductService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- otomatik migrate ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    db.Database.Migrate();
}
// -------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // container'da kapalı tutmak mantıklı
app.MapControllers();

app.Run();
