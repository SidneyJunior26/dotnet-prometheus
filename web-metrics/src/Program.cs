using Prometheus;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.UseHttpClientMetrics();

var app = builder.Build();

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseHttpMetrics();
app.UseMetricServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
