using BitcoinPriceTracker.Data;
using BitcoinPriceTracker.Interfaces;
using BitcoinPriceTracker.Repository;
using BitcoinPriceTracker.Seeders;
using BitcoinPriceTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<PriceBackgroundService>();
builder.Services.AddHttpClient<IBitcoinPrice, BitcoinPriceRepositorycs>();
builder.Services.AddScoped<IBitcoinPrice, BitcoinPriceRepositorycs>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Seeder 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    var seeder = new BitcoinPriceSeeder(context);
    await seeder.SeedAsync(); 
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
