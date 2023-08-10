using System.Text.Json.Serialization;
using Notes.Api.Conventions;
using Notes.Api.Extensions;
using Notes.Application.Extensions;
using Notes.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);
builder.Services.AddSettings(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

//Layers
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddControllers(options => options.Conventions.Add(new ApiRoutePrefixConvention("api")))
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

var app = builder.Build();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();