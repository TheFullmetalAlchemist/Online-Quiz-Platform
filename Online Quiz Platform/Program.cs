using Microsoft.EntityFrameworkCore;
using Online_Quiz_Platform.Data;
using Online_Quiz_Platform.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSession();


var app = builder.Build();


app.UseSession();

// ✅ Seed sample quiz data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!db.Questions.Any())
    {
        var q1CorrectId = Guid.NewGuid();
        var q1 = new Question
        {
            Id = Guid.NewGuid(),
            Text = "What is the capital of France?",
            Options = new List<Option>
            {
                new Option { Id = q1CorrectId, Text = "Paris" },
                new Option { Id = Guid.NewGuid(), Text = "London" }
            },
            Correctoption = q1CorrectId
        };

        var q2CorrectId = Guid.NewGuid();
        var q2 = new Question
        {
            Id = Guid.NewGuid(),
            Text = "What is the capital of India?",
            Options = new List<Option>
            {
                new Option { Id = Guid.NewGuid(), Text = "Mumbai" },
                new Option { Id = q2CorrectId, Text = "New Delhi" }
            },
            Correctoption = q2CorrectId
        };

        db.Questions.AddRange(q1, q2);
        db.SaveChanges();
    }
}


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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
