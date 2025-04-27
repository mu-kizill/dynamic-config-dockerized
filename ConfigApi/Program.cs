using ConfigApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigin = builder.Configuration["Cors:AllowedOrigin"];
Console.WriteLine("Allowed Origin from config: " + allowedOrigin);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine(" Migration başlatılıyor...");
        dbContext.Database.Migrate();
        Console.WriteLine("Migration tamamlandı.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Migration sırasında hata oluştu:");
        Console.WriteLine(ex.Message);
    }
}

app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
