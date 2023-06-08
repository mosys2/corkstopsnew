using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mst_Cms.Application.Services.Users.Command.LoginUser;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Categuries.Commands.RegisterCategury;
using Store.Application.Services.Categuries.Commands.RemoveCategory;
using Store.Application.Services.Categuries.Queries.GetAllCateguryForSelectList;
using Store.Application.Services.Common;
using Store.Application.Services.FileManager.Queries.ListDirectories;
using Store.Application.Services.Users.Commands.EditeUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Commands.RemoveUser;
using Store.Application.Services.Users.Commands.Website.RegisterUser;
using Store.Application.Services.Users.Queries.GetAllRolls;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Domain.Entities.Users;
using Store.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnectionString")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();



builder.Services.AddIdentity<User,Role>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DataBaseContext>()
    .AddDefaultTokenProviders();


// Force Identity's security stamp to be validated every minute.
builder.Services.Configure<SecurityStampValidatorOptions>(o =>
                   o.ValidationInterval = TimeSpan.FromMinutes(20));

builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

//Scopded Admin
builder.Services.AddScoped<IGetUsersServices, GetUsersServices>();
builder.Services.AddScoped<IGetAllRolls_ForAdmin, GetAllRolls_ForAdmin>();
builder.Services.AddScoped<IRegisterUser_Admin, RegisterUser_Admin>();
builder.Services.AddScoped<IRemoveUserServices_Admin, RemoveUserServices_Admin>();
builder.Services.AddScoped<IGetUserDetailServices, GetUserDetailServices>();
builder.Services.AddScoped<IEditeUserServicess, EditeUserServicess>();
builder.Services.AddScoped<ICheckUserExistByEmailServices, CheckUserExistByEmailServices>();
builder.Services.AddScoped<ICheckUserExistByMobileServices, CheckUserExistByMobileServices>();
builder.Services.AddScoped<ICheckUserExistByUsernameServices, CheckUserExistByUsernameServices>();
builder.Services.AddScoped<IRegisterCategoryServices, RegisterCategoryServices>();
builder.Services.AddScoped<IGetAllCateguriesForSelectListServices,GetAllCateguriesForSelectListServices>();
builder.Services.AddScoped<IRemoveCategoryServices, RemoveCategoryServices>();
//filemanager
builder.Services.AddScoped<IGetListDirectoryServices, GetListDirectoryServices>();


//Scopded Website
builder.Services.AddScoped<IRegisterUser_Website, RegisterUser_Website>();
builder.Services.AddScoped<ILoginUserService, LoginUserService>();

builder.Services.AddSession();


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
app.UseAuthentication();
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
