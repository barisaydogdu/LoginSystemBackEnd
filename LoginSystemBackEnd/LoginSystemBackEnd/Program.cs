using LoginSystemBackEnd;
using LoginSystemBackEnd.Data;
using LoginSystemBackEnd.Services;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;



// Servisleri yap�land�rma ve middleware eklemek i�in kullan�l�r



var app = Startup.InitializeApp(args);
app.Run();

//builder.Services.AddSingleton<DbContextProvider>();
//builder.Services.AddSingleton<IUserService, UserService>();





//Database ConnectionString




// Configure the HTTP request pipeline.
