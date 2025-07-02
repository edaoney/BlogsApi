using BlogApi.Data;
using BlogApi.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();

//Her kullanıcı isteği için ayrı kopya oluşturarak, işlemlerin birbirine karışmasını önlüyoruz.
builder.Services.AddScoped<RoleSeed>();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(opt =>
    {
            
        opt.Password.RequiredLength = 1;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireLowercase = false;
        opt.Password.RequiredUniqueChars = 0;
        opt.Password.RequireDigit = false;
        opt.Password.RequiredUniqueChars = 0;
        opt.SignIn.RequireConfirmedEmail = false;
            
            
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<SecurityStampValidatorOptions>(opt => opt.ValidationInterval = TimeSpan.Zero);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<RoleSeed>();
    await seeder.SeedAsync();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGroup("Auth").MapIdentityApi<IdentityUser>().WithTags("Auth");

app.Run();