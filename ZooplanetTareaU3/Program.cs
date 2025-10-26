using NuGet.Protocol.Core.Types;
using ZooplanetTareaU3.Models.Entities;
using ZooplanetTareaU3.Services;
using ZooplanetTareaU3.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<AnimalesContext>();
builder.Services.AddScoped<ClasesService>();
builder.Services.AddScoped<EspeciesServices>();
builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));

var app = builder.Build();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapDefaultControllerRoute();
app.UseStaticFiles();
app.Run();
