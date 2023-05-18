using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(a =>
                 {
                     a.LogoutPath = "/home/index";
                     a.LoginPath = "/user/login";
                 });
builder.Services.AddTransient<IBook, BookDB>();
builder.Services.AddDbContext<BookDbContext>(a =>
{
    a.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
});

builder.Services.AddSession(a =>
{
    a.Cookie.Name = "Session-Name";

});


var app = builder.Build();

//app.Use(async (context,next) =>
//{
//    if (context.Request.Method == "POST")
//        await context.Response.WriteAsync("POSTING");

//    else
//    {
//        //await context.Response.WriteAsync("POSTING");
//        context.
//        await next.Invoke();
//    }
//});

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();    
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
