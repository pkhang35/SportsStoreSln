using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//TH 04
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddDbContext<StoreDbContext>(opts => {
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

app.UseStaticFiles();
//TH 04
app.UseSession();
app.MapControllerRoute("catpage",
"{category}/Page{productPage:int}",
new { Controller = "Home", action = "Index" });
app.MapControllerRoute("page", "Page{productPage:int}",
new
{
    Controller = "Home",
    action = "Index",
    productPage = 1
});
app.MapControllerRoute("category", "{category}",
new
{
    Controller = "Home",
    action = "Index",
    productPage = 1
});
app.MapControllerRoute("pagination",
"Products/Page{productPage}",
new
{
    Controller = "Home",
    action = "Index",
    productPage = 1
});
/*
app.MapControllerRoute("pagination",
"Products/Page{productPage}",
new { Controller = "Home", action = "Index" });
*/
app.MapControllerRoute("pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index" });
app.MapDefaultControllerRoute();
//TH04
app.MapRazorPages();
SeedData.EnsurePopulated(app);

app.Run();