public class AvaliacaoRepository : IAvaliacaoRepository
{
    private static Dictionary<int, Avaliacao> _avaliacoes = new Dictionary<int, Avaliacao>()
    {
        {1, new Prova{ Id = 1, Titulo = "Prova 1", Acertos=5, Total = 10}},
        {2, new Trabalho{ Id = 2, Titulo = "Projeto Final", conteudo = 5, Apresentacao = 6}},
        {3, new Participacao{Id = 3, Titulo = "Presenca em aula", Presencas = 18, Aulas = 20}}
    };


    public IEnumerable<Avaliacao> ObterTodas()
    {
        return _avaliacoes.Values;
    }

    public Avaliacao? ObterPorId(int id)
    {
        if (!_avaliacoes.ContainsKey(id))
        {
            return null;
        }

        return _avaliacoes[id];
    }


    public bool Remover(int id)
    {
        if (!_avaliacoes.ContainsKey(id))
        {
            return false;
        }

        _avaliacoes.Remove(id);
        return true;
    }
}