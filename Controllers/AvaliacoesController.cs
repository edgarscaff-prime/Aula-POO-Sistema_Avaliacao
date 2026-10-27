using Microsoft.AspNetCore.Http.HttpResults;
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


[HttpPost("{id}/recuperar")]

public IActionResult Recuperar(int id, [FromQuery] double novaNota)
{
    var avaliacao = _repository.ObterPorId(id);

    if (avaliacao == null)
    {
        return NotFound("Avaliacao nao encontrada");
    }
    if (avaliacao is not IRecuperavel itemRecuperavel)
{
        return BadRequest(
            $"Avaliacao '{avaliacao.Titulo}' do tipo '{avaliacao.GetType().Name}' Não aceita recperação"
            );
    }
    double notaAntiga = avaliacao.CalcularNota();
    double notaFinal = itemRecuperavel.Recuperar(novaNota);

    return Ok(new
    {
        mensagem = "Recuperação realizada com sucesso",
        titulo = avaliacao.Titulo,
        notaAntiga = Math.Round(notaAntiga, 2),
        notaFinal = Math.Round(notaFinal, 2)
    });}


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool removida = _repository.Remover(id);

        if (!removida)
        {
            return NotFound("Avaliação não encontrada");
        }
        return Ok("Avaliacao excluida com sucesso");
    }
}
