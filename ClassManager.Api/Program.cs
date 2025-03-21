using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ClassManager.Api.Endpoints;
using ClassManager.Api.Services;
using ClassManager.Api.Data;
using ClassManager.Api.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddDbContext<TaskContext>
        (options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));


builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<DepartmentService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ClassService>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var secretKey = builder.Configuration.GetValue<string>("Jwt:Secret") ?? throw new InvalidOperationException("Jwt:Secret is missing from configuration.");
        var symmetricKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            IssuerSigningKey = symmetricKey,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
    {
        var allowedOriginsStr = builder.Configuration.GetValue<string>("AllowedOrigins") ?? string.Empty;
        var allowedOrigins = allowedOriginsStr.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        p.WithOrigins(allowedOrigins)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials();
    });
});


builder.Services.AddAuthorization();



var app = builder.Build();
#if DEBUG
ApplyMigrations(app.Services);
#endif
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication()
    .UseAuthorization();
app.UseCors();

app.MapAuthEndpoints();
app.MapDepartmentEndpoint();   
app.MapUserEndpoint();  
app.MapClassEndpoints();    

app.Run();



static void ApplyMigrations(IServiceProvider sp)
{
    var scope = sp.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TaskContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}