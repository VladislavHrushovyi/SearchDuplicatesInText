using SearchDuplicatesText.Middlewares;
using SearchDuplicatesText.ServiceProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
builder.Services.AddDbService(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseMiddlewares();
app.MapControllers();

app.Run();