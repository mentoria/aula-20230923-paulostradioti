using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Citacoes.Api
{
    /// <summary>
    /// Contains the API Behavior Configurations
    /// </summary>
    public static class ApiConfiguration
    {
        internal static void ApiBehaviorOptions(ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                var validationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                validationProblemDetails.Detail = "Verifique a propriedade de erros para mais detalhes";
                validationProblemDetails.Instance = context.HttpContext.Request.Path;

                validationProblemDetails.Type = "https://authority.com/modelvalidationproblem";
                validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                validationProblemDetails.Title = "Ocorreram erros de validação";

                return new UnprocessableEntityObjectResult(validationProblemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            };
        }
    }
}
