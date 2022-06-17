namespace Locadora.Model
{
	public class LocacaoItem : Entity
	{
		public int IDFilme { get; set; }
		public Filme Filme { get; set; }

		public int IDLocacao { get; set; }
		public Locacao Locacao { get; set; }
		public bool Ativo { get; set; }

	}
}
