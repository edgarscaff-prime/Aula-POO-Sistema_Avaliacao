using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]

public class AvaliacoesController : ControllerBase
{
    private IAvaliacaoRepository _repository;

    public AvaliacoesController(IAvaliacaoRepository repository)
    {
        _repository = repository;
    }

    
    [HttpGet]
    public IActionResult Get()
    {
        var resultado = _repository.ObterTodas().Select(avaliacao =>
        {
            return new
            {
              avaliacao.Id,
              Tipo = avaliacao.GetType().Name,
              Descricao = avaliacao.Descricao(),
              Nota = Math.Round(avaliacao.CalcularNota(), 2)
            };
        });
        return Ok(resultado);
    }
}