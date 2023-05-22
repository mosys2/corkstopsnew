using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Common;
using Store.Application.Services.Users.Commands.EditeUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Commands.RemoveUser;
using Store.Application.Services.Users.Queries.GetAllRolls;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnectionString")));

builder.Services.AddAntiforgery(options => options.HeaderName = "RequestVerificationToken");

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

builder.Services.AddScoped<IGetUsersServices, GetUsersServices>();
builder.Services.AddScoped<IGetAllRolls_ForAdmin, GetAllRolls_ForAdmin>();
builder.Services.AddScoped<IRegisterUser_Admin, RegisterUser_Admin>();
builder.Services.AddScoped<IRemoveUserServices_Admin, RemoveUserServices_Admin>();
builder.Services.AddScoped<IGetUserDetailServices, GetUserDetailServices>();
builder.Services.AddScoped<IEditeUserServicess, EditeUserServicess>();
builder.Services.AddScoped<ICheckUserExistByEmailServices, CheckUserExistByEmailServices>();
builder.Services.AddScoped<ICheckUserExistByMobileServices, CheckUserExistByMobileServices>();
builder.Services.AddScoped<ICheckUserExistByUsernameServices, CheckUserExistByUsernameServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{

    //پیج پیشفرض سایت
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


    //پیج پیشفرض آریا مدیریت
    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});
app.Run();
