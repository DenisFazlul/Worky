using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using Worky.Data;
using Worky.Data.Tags;
using Worky.Services;
using Worky.Services.EmailService;
using Worky.Users;

var builder = WebApplication.CreateBuilder(args);





Worky.AdminData.Set(builder.Configuration);
// Add services to the container.


var connectionStringProjectDb= builder.Configuration.GetConnectionString("ProjecDb");
Worky.DataDB.connectionStrinProjectDb = connectionStringProjectDb;





builder.Services.AddDbContext<Worky.Data.Project.ProjectDbContext>(options =>
    options.UseSqlServer(connectionStringProjectDb));



var connectionStringUsersDb = builder.Configuration.GetConnectionString("UsersDb");
builder.Services.AddDbContext<IUsersCollection, UserDbContext>(options =>
 options.UseSqlServer(connectionStringUsersDb));

var connectionToTagsDb= builder.Configuration.GetConnectionString("TagsDb");
builder.Services.AddDbContext<ITagsDb, TagsDb>(options =>
 options.UseSqlServer(connectionToTagsDb));

builder.Services.AddSingleton<INotificationService, EmailNotifyService>();

builder.Services.AddSingleton<IBookCashe, MemoryBookCache>();


builder.Services.AddReact();
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();



builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.SlidingExpiration = true;


                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                    //options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Account/Login");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Projects}/{action=YourProjects}/{id?}");
    
app.MapRazorPages();



app.Run();


