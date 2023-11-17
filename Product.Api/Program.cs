
using Product.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureHttpLogging();
builder.Services.ConfigureLogger();
builder.Services.ConfigureDb();
builder.Services.ConfigureRepository();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.MapHealthChecks("/health");
app.UseHttpsRedirection();
app.UseHttpLogging();
app.UseCors(ServiceExtensions.corsName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();