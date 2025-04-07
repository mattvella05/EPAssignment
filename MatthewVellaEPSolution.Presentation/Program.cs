using MatthewVellaEPSolution.DataAccess;
using MatthewVellaEPSolution.Presentation.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register PollDbContext with connection string
builder.Services.AddDbContext<PollDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register PollRepository to be used with constructor injection
builder.Services.AddScoped<CommonPollRepository, PollRepository>();
//builder.Services.AddScoped<CommonPollRepository, PollFileRepository>();

builder.Services.AddScoped<AuthorizeVoteAttribute>();

// Add services to support session state
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // optional timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add MVC controllers and views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable Session before Authorization
app.UseSession();

app.UseAuthorization();

// Set default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Poll}/{action=Index}/{id?}");

app.Run();
