using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DakiShop.Logic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DakiShop.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddBlazoredToast();


AWSManager.UserKey = Config.configuration.GetSection("AWSKeys")["UserKey"];
AWSManager.SecretKey = Config.configuration.GetSection("AWSKeys")["SecretKey"];

DBService.InitDB();

//DBService.AddDakimakura(1, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/248345166.jpg", "���i��� ����������",5000, "170x60", "Sintepon", 4);
//DBService.AddDakimakura(3, @"https://dakisource.s3.eu-north-1.amazonaws.com/266947307.jpg", "�i��i ���i�����",14000, "195x80", "Sintepon", 2);


//AWSManager.UploadFile(File.Open(@"C:\Users\User\Desktop\photo_2022-05-26_12-34-43.jpg", FileMode.Open), "dakisource", "new amogus2");

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
