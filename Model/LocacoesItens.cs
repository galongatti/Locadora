namespace Locadora.Model
{
	public class LocacoesItens
	{
		public int Id { get; set; }
		public Filme Filme { get; set; }
		public Locacao Locacao { get; set; }
	}
}
