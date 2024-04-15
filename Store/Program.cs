using Microsoft.AspNetCore.Builder;
using Store.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStore(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});
app.UseRouting();
app.MapControllers();

app.Run();