using Citacoes.Api.Domain;
using Citacoes.Api.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

namespace Citacoes.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Modo 1
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(ApiConfiguration.ApiBehaviorOptions);
            #endregion

            #region Modo 2
            //builder.Services.AddControllers();
            //builder.Services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();
            #endregion

            #region Fluent Validator
            //builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            #endregion

            builder.Configuration.AddUserSecrets<Program>();
            builder.Services.AddDbContext<CitacoesDbContext>();
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