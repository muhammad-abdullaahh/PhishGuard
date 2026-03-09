using Microsoft.EntityFrameworkCore;
using PhishGuard.Models;
using PhishGuard.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var sqlConnStr = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=localhost\\SQLEXPRESS;Integrated Security=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<YourDbContext>(options =>
    options.UseSqlServer(sqlConnStr, sql => sql.EnableRetryOnFailure())
);

builder.Services.AddScoped<LinkCheckerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<YourDbContext>();
        try
        {
            db.Database.Migrate();
        }
        catch (Exception migrateEx)
        {
            Console.WriteLine($"Migrations failed: {migrateEx.Message}. Attempting EnsureCreated().");
            try
            {
                db.Database.EnsureCreated();
            }
            catch (Exception ensureEx)
            {
                Console.WriteLine($"EnsureCreated failed: {ensureEx.Message}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database initialization error: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
