using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DakiShop.Logic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

AWSManager.UserKey = Config.configuration.GetSection("AWSKeys")["UserKey"];
AWSManager.SecretKey = Config.configuration.GetSection("AWSKeys")["SecretKey"];
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();
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
