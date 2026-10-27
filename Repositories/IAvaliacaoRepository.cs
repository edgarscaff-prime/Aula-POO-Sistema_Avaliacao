public interface IAvaliacaoRepository{
    IEnumerable<Avaliacao> ObterTodas();

    Avaliacao? ObterPorId(int id);

    bool Remover(int id);
}