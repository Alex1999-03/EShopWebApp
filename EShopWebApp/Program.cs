using EShopWebApp.Data;
using EShopWebApp.Services;
using EShopWebApp.Services.EmailProvider;
using EShopWebApp.Services.Interfaces;
using EShopWebApp.Utils;
using EShopWebApp.Utils.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStrings.IDENTITY_CONNECTION)));

// Add connection to database
builder.Services.AddDbContext<EShopDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStrings.ESHOP_CONNECTION)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


// Dependency Injection Email Sender
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

// Dependency Injection Services
builder.Services.AddSingleton(CloudinaryConfiguration.GetInstance(builder));
builder.Services.AddScoped<IBrandServices, BrandServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<IStateServices, StateServices>();
builder.Services.AddScoped<IImageServices, ImageServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<IProductCartServices, ProductCartServices>();
builder.Services.AddScoped<IAddressServices, AddressService>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderDetailServices, OrderDetailServices>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "MyAreaAdmin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.MapRazorPages();

app.Run();
