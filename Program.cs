using Hw5forExam.Data;
using Hw5forExam.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CvDb"));

builder.Services.AddScoped<ICvService, CvService>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    // Ensure DB is created (even in-memory)
    dbContext.Database.EnsureCreated();

    // Only seed if it's empty
    if (!dbContext.cvs.Any())
    {
        dbContext.cvs.AddRange(new List<CvData>
        {
            new CvData
            {
                FName = "Ali",
                LName = "Ahmed",
                BDay = new DateTime(1998, 5, 12),
                Nationality = "Syrian",
                Sex = "M",
                Email = "ali@example.com",
                Password = "password123",
                Skills = new List<string> { "C#", "ASP.NET", "SQL" },
                PhotoFileName = "default.png"
            },
            new CvData
            {
                FName = "Sara",
                LName = "Youssef",
                BDay = new DateTime(2000, 8, 22),
                Nationality = "American",
                Sex = "F",
                Email = "sara@example.com",
                Password = "secret456",
                Skills = new List<string> { "HTML", "CSS", "JavaScript" },
                PhotoFileName = "default.png"
            }
        });

        dbContext.SaveChanges();
    }
}

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
