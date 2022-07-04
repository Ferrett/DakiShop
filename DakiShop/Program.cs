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

//DBService.AddDakimakura(1, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/248345166.jpg", "Капiтан Зеленський",5000, 170,60, "Sintepon", 4);

//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/kaban.png", 3000, "Мото-Мото", 180,200, "Sintepon", 1);
//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/fateev.png", 6666, "Анимешник", 170,70, "Sintepon", 4);
//DBService.AddDakimakura(5, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/den.png", 909090, "Баскетбол Куроко", 180,200, "Holofiber", 4);
//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/rektor.png", 1111, "Решала", 170,69, "Holofiber", 4);
//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/borov.png", 3000, "Боров", 200,120, "Holofiber", 4);
//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/demenkov.png", 750, "Безумный дед", 160,100, "Sintepon", 4);
//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/sanya.png", 1060, "Kurwa", 160,60, "Holofiber", 1);
//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/tolik.png", 5000, "Cосисочный Вор", 170,70, "Sintepon", 1);
//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/zoha.png", 3010, "Скинхед", 170,60, "Sintepon", 4);
//DBService.AddDakimakura(6, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/extroordinary/zoha2.png", 3000, "Скинхед Помладше", 150,40, "Sintepon", 4);
//DBService.AddDakimakura(7,@"https://dakisource.s3.eu-north-1.amazonaws.com/daki/patriotic/arest.png", 6000, "Капитан \"2-3 недели\"", 180,70, "Holofiber", 1);
//DBService.AddDakimakura(5, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/meme/%D1%82%D0%BE%D0%BC%D0%B0%D1%81.png", 500, "Томас Козырёк", 170,70, "Holofiber", 2);
//DBService.AddDakimakura(3, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/classic/rem.png", 10000, "Rem", 150,40, "Holofiber", 1);
//DBService.AddDakimakura(4, @"https://dakisource.s3.eu-north-1.amazonaws.com/daki/gachi/van2.png", 6969, "Van Darkhome", 180,70, "Holofiber", 3);

//DBService.AddDakimakura(3, @"https://dakisource.s3.eu-north-1.amazonaws.com/266947307.jpg", "Бiллi Херiнгтон",14000, "195x80", "Sintepon", 2);
//DBService.AddRootUser("admin", "vovkaprikhod@gmail.com", "PRIKHOD322", "");

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
