using Citacoes.Api.Domain;
using Microsoft.AspNetCore.Mvc;
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

        public CitacoesController(CitacoesDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Ok(context.Citacoes);
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
            context.Set<Citacao>().Add(citacao);
            context.SaveChanges();

            return Created(nameof(Get), citacao);
        }
    }
}
