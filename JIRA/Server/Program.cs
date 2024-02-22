using JIRA.Server.Domain;
using JIRA.Shared.Domain;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using JIRA.Shared.Entity;
using TabBlazor;
using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Server.Domain.Repositories.EntityFramework;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        var configuration = builder.Configuration;
        builder.Services.AddTransient<IProjectRepository, EFProjectRepository>();
        builder.Services.AddTransient<DataManager>();
        builder.Services.AddDbContext<ProjectManagementContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(ProjectManagementContext)));
        });
        builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false; // Установите параметры, если это необходимо
            options.User.RequireUniqueEmail = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
        })
            .AddEntityFrameworkStores<ProjectManagementContext>()
            .AddDefaultTokenProviders(); // Добавьте токен-провайдеры, если они необходимы

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();


        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}