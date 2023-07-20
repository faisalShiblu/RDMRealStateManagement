using FluentValidation.AspNetCore;
using FluentValidation;
using RealStateMVCWebApp.Models;
using RealStateMVCWebApp.Service;
using MongoDB.Driver;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using RealStateMVCWebApp.DTO.PropertyListing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
                (MongoDBConnection.ConnectionString, MongoDBConnection.DBName);

builder.Services.AddSingleton<IMongoClient>(provider =>
{
    return new MongoClient(MongoDBConnection.ConnectionString);
});



builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<CreatePropertyListingDTO>, CreatePropertyListingValidator>();
builder.Services.AddScoped<IValidator<EditPropertyListingDTO>, EditPropertyListingDTOValidator>();

builder.Services.AddScoped<PropertyService>();


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


app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Account}/{action=DualSign}/{id?}");
//pattern: "{controller=PropertyListing}/{action=Index}/{id?}");
pattern: "{controller=PropertyListing}/{action=PropertySearching}/{id?}");
//pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.Run();
