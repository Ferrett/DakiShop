using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DakiShop.Logic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DakiShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<AppState>();
AWSManager.UserKey = Config.configuration.GetSection("AWSKeys")["UserKey"];
AWSManager.SecretKey = Config.configuration.GetSection("AWSKeys")["SecretKey"];

//DBService.InitDB();
//DBService.AddRootUser("admin","vovkaprikhod@gmail.com","11111111");
//DBService.AddDakimakuraToDB(1, @"https://dakisource.s3.eu-north-1.amazonaws.com/266947368.jpg", "Капiтан Зеленський",5000, "170x60",1,4);
DBService.GetDataFromDB();

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
