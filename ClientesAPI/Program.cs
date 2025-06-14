using Asp.Versioning.ApiExplorer;
using ClientesAPI.API.Configuration;
using ClientesAPI.Application.Map;
using ClientesAPI.Application.Services;
using ClientesAPI.Application.Services.Interface;
using ClientesAPI.Domain.Interface;
using ClientesAPI.Infrastructure.Data;
using ClientesAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.OperationFilter<SwaggerDefaultValues>();
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ClienteContext>(options =>
    options.UseInMemoryDatabase("ClientesDb"));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddApiVersioning()
    .AddMvc()
    .AddApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    });

builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

//Logging configurações
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            var version = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in version.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json"
                    , $"Web Api - {description.GroupName.ToUpper()}");
            }
        }
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
