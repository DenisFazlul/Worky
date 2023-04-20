using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using Worky.Data;
using Worky.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);





Worky.AdminData.Set(builder.Configuration);
// Add services to the container.


var connectionStringProjectDb= builder.Configuration.GetConnectionString("ProjecDb");
Worky.DataDB.connectionStrinProjectDb = connectionStringProjectDb;


builder.Services.AddSingleton<INotificationService,EmailNotifyService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionStringProjectDb));


builder.Services.AddDbContext<Worky.Data.Project.ProjectDbContext>(options =>
    options.UseSqlServer(connectionStringProjectDb));



builder.Services.AddReact();
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

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

app.UseReact(config => { });

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


