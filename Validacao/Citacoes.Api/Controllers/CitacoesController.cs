using Citacoes.Api.Domain;
using Citacoes.Api.Domain.Pagination;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Net.Mime;

namespace Citacoes.Api.Controllers
{
    /// <summary>
    /// CRUD de Citações
    /// </summary>
    [ApiController]
    [Route("api/citacoes")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CitacoesController : ControllerBase
    {
        private readonly CitacoesDbContext context;
        private readonly IValidator<Citacao> validator;

        public CitacoesController(CitacoesDbContext context, IValidator<Citacao> validator)
        {
            this.context = context;
            this.validator = validator;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]PaginationParameters pagination)
        {
           var citacoes = context.Citacoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.SearchQuery))
                citacoes = citacoes.Where(x => x.Texto.Contains(pagination.SearchQuery) || x.Autor.Contains(pagination.SearchQuery));

            return Ok(PagedList<Citacao>.Create(citacoes, pagination.PageNumber, pagination.PageSize));
        }

        /// <summary>
        /// Cria uma nova citaçao no banco de dados.
        /// </summary>
        /// <param name="citacao">Citação a ser criada</param>
        /// <response code="201">Retorna a citação recêm criada</response>
        /// <response code="422">Retorna um ProblemDetails em caso de erros de validação.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult CreateCitacao(Citacao citacao)
        {
            //var validationResult = validator.Validate(citacao);
            //if (!validationResult.IsValid)
            //{
            //    validationResult.AddToModelState(this.ModelState);
            //    return ValidationProblem();
            //}

            context.Set<Citacao>().Add(citacao);
            context.SaveChanges();

            return Created(nameof(Get), citacao);
        }


        [HttpGet("problem/{tipo:int}")]
        public IActionResult GetProblem(int tipo)
        {

            if (tipo == 1)
            {
                ModelState.AddModelError("Teste", "Retorno usando método ValidationProblem");
                //return ValidationProblem(ModelState);
                return ValidationProblem();
            }

            if (tipo == 2)
            {
                return Problem("Retorno usando método Problem");
            }

            if (tipo == 3)
            {
                return ValidationProblem("Retorno usando método ValidationProblem");
            }

            return Ok();
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
