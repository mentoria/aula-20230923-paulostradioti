using Citacoes.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Citacoes.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options => 
                {
                    options.InvalidModelStateResponseFactory = context => 
                    {
                        var defaultProblemDetailsFactory = context.HttpContext.RequestServices.GetService<ProblemDetailsFactory>();
                        var problemDetails = defaultProblemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                        problemDetails.Title = "Ocorreram erros de validação";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;

                        return new UnprocessableEntityObjectResult(problemDetails);
                    };
                });

            builder.Configuration.AddUserSecrets<Program>();

            //var dbPassword = builder.Configuration["DatabasePassword"];
            //var connectionString = builder.Configuration.GetConnectionString("Default");

            //var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            //connectionStringBuilder.Password = dbPassword;

            builder.Services.AddDbContext<CitacoesDbContext>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => 
            {
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                options.IncludeXmlComments(filePath, true);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}