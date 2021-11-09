using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazeOrbital.CentralServer.Data;
using BlazeOrbital.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString))
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services
    .AddIdentityServer()
    .AddApiAuthorization<IdentityUser, ApplicationDbContext>();

builder.Services
    .AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddGrpc();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
SeedData.EnsureSeeded(app.Services);

// Allow requests from the external ManufacturingHub and  MissionControl applications
app.UseCors(cors => cors.WithOrigins(
    builder.Configuration["Apps:ManufacturingHub:Origin"],
    builder.Configuration["Apps:MissionControl:Origin"]
).AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseGrpcWeb();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapGrpcService<ManufacturingDataService>().EnableGrpcWeb();
app.MapRazorPages();
app.MapControllers();

app.Run();
