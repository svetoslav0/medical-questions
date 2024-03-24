using MedicalQuestions.Data;
using MedicalQuestions.Services;
using MedicalQuestions.Email;
using MedicalQuestions.Email.Config;
using MedicalQuestions.Email.Interfaces;

var builder = WebApplication.CreateBuilder(args);

EmailConfiguration emailConfig = builder
    .Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MladostPublicContext>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1440);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddSingleton<EmailConfiguration>(emailConfig);

var app = builder.Build();

builder.Configuration
    .SetBasePath(app.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{app.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
