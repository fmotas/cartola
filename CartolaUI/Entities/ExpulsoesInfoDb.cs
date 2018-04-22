namespace CartolaUI.Entities
{
	public class ExpulsoesInfoDb
    {
		public string nome { get; set; }
	    public int quantidade_de_expulsoes { get; set; }

	    public ExpulsoesInfoDb(string nome, int quantidadeDeExpulsoes)
	    {
		    this.nome = nome;
		    quantidade_de_expulsoes = quantidadeDeExpulsoes;
	    }
	}
}
