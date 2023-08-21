using LoginSystemBackEnd;
using LoginSystemBackEnd.Data;
using LoginSystemBackEnd.Services;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;



// Servisleri yapýlandýrma ve middleware eklemek için kullanýlýr



var app = Startup.InitializeApp(args);
app.Run();

//builder.Services.AddSingleton<DbContextProvider>();
//builder.Services.AddSingleton<IUserService, UserService>();





//Database ConnectionString




// Configure the HTTP request pipeline.
