namespace Locadora.Model
{
	public class LocacoesItens : Entity
	{
		public Filme Filme { get; set; }
		public Locacao Locacao { get; set; }
	}
}
