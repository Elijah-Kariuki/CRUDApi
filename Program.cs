using System;
using CRUDApi.Data;
using CRUDApi.Interfaces;
using CRUDApi.Models;
using CRUDApi.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

// Add user secrets configuration source
builder.Configuration.AddUserSecrets<JobRepository>();

builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Services.AddMemoryCache();

// Add your DbContext configuration here
builder.Services.AddDbContext<IndeedJobsContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("JobsContext"),
    new MySqlServerVersion(new Version(8, 0, 33))));

// Register your repository and other services here
builder.Services.AddScoped<IJobRepository, JobRepository>();
// Add other services as needed

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



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();