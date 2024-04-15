using Agent.DI;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAgent(builder.Configuration);

var app = builder.Build();

app.Run();