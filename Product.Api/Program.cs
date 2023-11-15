using Product.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureHttpLogging();
builder.Services.ConfigureLogger();
builder.Services.ConfigureDb();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureAutoMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();