using MediatR;
using Microsoft.OpenApi.Models;
using Questao5;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using System.Data;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Registrar IDbConnection como conexão SQLite
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<DatabaseConfig>();
    return new SqliteConnection(config.Name); 
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Conta Corrente",
        Version = "v1",
        Description = "API para movimentação e consulta de saldo de conta corrente com suporte a idempotência",
        Contact = new OpenApiContact
        {
            Name = "Alios",
            Email = "Aliosl@Alios.com",
            Url = new Uri("https://www.ailos.coop.br/")
        }
    });

    // Configuração para ler o arquivo de documentação XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

DependencyInjection.Register(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Conta Corrente v1");
        c.RoutePrefix = "swagger"; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
var dbBootstrap = app.Services.GetService<IDatabaseBootstrap>();
dbBootstrap?.Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();
