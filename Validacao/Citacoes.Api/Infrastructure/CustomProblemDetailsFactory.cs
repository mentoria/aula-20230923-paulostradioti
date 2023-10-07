using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Citacoes.Api.Infrastructure
{
    public class CustomProblemDetailsFactory : ProblemDetailsFactory
    {
        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, 
            int? statusCode = null, 
            string? title = null, 
            string? type = null, 
            string? detail = null, 
            string? instance = null)
        {
            return new ValidationProblemDetails()
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = title ?? "Ocorreram erros de validação",
                Type = "https://authority.com/modelvalidationproblem",
                Detail = detail ?? "Verifique a propriedade de erros para mais detalhes",
                Instance = instance ?? httpContext.Request.Path
            };
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
            ModelStateDictionary modelStateDictionary, 
            int? statusCode = null, 
            string? title = null, 
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            return new ValidationProblemDetails(modelStateDictionary)
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = title ?? "Ocorreram erros de validação",
                Type = "https://authority.com/modelvalidationproblem",
                Detail = detail ?? "Verifique a propriedade de erros para mais detalhes",
                Instance = instance ?? httpContext.Request.Path
            };
        }
    }
}
