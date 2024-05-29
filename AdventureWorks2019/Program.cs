using AdventureWorks2019.Application;
using AdventureWorks2019.DataAccess;
using AdventureWorks2019.DataAccess.Repositories;
using AdventureWorks2019.Domain.Abstractions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using AdventureWorks2019.API.Extensions;
using AdventureWorks2019.Application.Services;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AppMappingProfile), typeof(AppMappingProfile2));
builder.Services.AddTransient<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

builder.Services.AddApiAuthentication(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler(options =>
    {
        options.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/html";
            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
            if (null != exceptionObject)
            {
                var errorMessage = $"<b>Exception Error: {exceptionObject.Error.Message}</b> {exceptionObject.Error.StackTrace}";
                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);

                Console.WriteLine(errorMessage);
            }
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
    {
        x.WithHeaders().AllowAnyHeader().AllowCredentials();
        x.WithOrigins("http://localhost:3000");
        x.WithMethods().AllowAnyMethod();
    }
);

app.Run();