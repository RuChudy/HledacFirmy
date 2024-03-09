using Microsoft.EntityFrameworkCore;
using Hledac.Database;
using Hledac.Database.Context;
using Hledac.Domain.Ares.Services;
using Hledac.Domain.Firma.Services;
using Hledac.Domain.Firma;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContext<SubjektDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.Configure<AresSettings>(builder.Configuration.GetSection("Ares"));
builder.Services.AddHttpClient<AresHttpClient>();

builder.Services.AddScoped<FirmaService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Test na funkcnost
app.MapGet("/", () => "Hello World!")
    .WithName("HelloWorld")
    .WithOpenApi();

// Vyhledávaè ièa
app.MapPost("/najdi-ico/{ico}", ApiPostNajdiIco)
    .WithName("NajdiIco")
    .Produces(404)
    .Produces<FirmaDto>()
    .WithOpenApi();

app.Run();

static async Task<IResult> ApiPostNajdiIco(string ico, FirmaService fs)
{
    var firma = await fs.NajdiFirmuDleIcoNeboNullAsync(ico);
    return (firma == null) ? TypedResults.NotFound() : TypedResults.Ok(firma);
}
