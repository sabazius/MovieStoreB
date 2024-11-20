using MovieStoreB.BL;
using MovieStoreB.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDataDependencies()
    .AddBusinessDependencies();



builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
