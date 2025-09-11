using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Online_Quiz_Platform.Data;
using Online_Quiz_Platform.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";   // redirect here if not logged in
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

var app = builder.Build();

// Correct order of middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();


app.UseSession();

// ✅ Seed sample quiz data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!db.Quizzes.Any())
    {
        var quiz = new Quiz
        {
            Title = "General Knowledge Quiz",
            IsPublished = true,
            CreatedDate = DateTime.Now,
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "What is the capital of France?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Paris" },
                        new Option { Text = "London" },
                        new Option { Text = "Berlin" },
                        new Option { Text = "Madrid" }
                    }
                },
                new Question
                {
                    Text = "What is the capital of India?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Mumbai" },
                        new Option { Text = "New Delhi" },
                        new Option { Text = "Chennai" },
                        new Option { Text = "Kolkata" }
                    }
                }
            }
        };

        db.Quizzes.Add(quiz);
        db.SaveChanges();

        // ✅ Fix correct answers after IDs are generated
        var franceQ = quiz.Questions.First(q => q.Text.Contains("France"));
        franceQ.Correctoption = franceQ.Options.First(o => o.Text == "Paris").Id;

        var indiaQ = quiz.Questions.First(q => q.Text.Contains("India"));
        indiaQ.Correctoption = indiaQ.Options.First(o => o.Text == "New Delhi").Id;

        db.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
