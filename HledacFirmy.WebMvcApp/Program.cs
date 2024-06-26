﻿using Hledac.Database;
using Hledac.Database.Context;
using Hledac.Domain.Rss.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<RssSettings>(builder.Configuration.GetSection(RssSettings.SectionName));

// Zaregistrujeme databazi
builder.Services.AddDbContext<SubjektDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

// Zaregistrujeme Rss reader a repository
builder.Services.AddHttpClient<RssHttpClient>();
builder.Services.AddScoped<IRssReaderService, RssReaderService>();
builder.Services.AddScoped<IRssRepositoryService, RssRepositoryService>();

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
