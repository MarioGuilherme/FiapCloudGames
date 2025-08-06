using FiapCloudGames.API.Filters;
using FiapCloudGames.API.Middlewares;
using FiapCloudGames.Application;
using FiapCloudGames.Infrastructure;
using FiapCloudGames.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration);

builder.Services
    .AddControllers(options => options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() {
        Title = "API - Fiap Cloud Games",
        Description = "Developed by Mário Guilherme de Andrade Rodrigues",
        Version = "v1",
        Contact = new() {
            Name = "Mário Guilherme de Andrade Rodrigues",
            Email = "marioguilhermedev@gmail.com"
        },
        License = new() {
            Name = "MIT",
            Url = new("https://opensource.org/licenses/MIT")
        }
    });

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta maneira: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new() { {
        new() {
            Reference = new() {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        },
        Array.Empty<string>()
    }});
});

WebApplication app = builder.Build();

#region Garante a criação do banco de dados na inicialização
try {
    using AsyncServiceScope asyncServiceScope = app.Services.CreateAsyncScope();
    IServiceProvider services = asyncServiceScope.ServiceProvider;
    FiapCloudGamesDbContext context = services.GetRequiredService<FiapCloudGamesDbContext>();
    if ((await context.Database.GetPendingMigrationsAsync()).Any())
        await context.Database.MigrateAsync();
} catch (Exception ex) {
    Console.WriteLine($"Erro durante inicialização do banco! {ex.Message}");
}
#endregion

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
