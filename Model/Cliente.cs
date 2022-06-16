using System.Collections.Generic;

namespace Locadora.Model
{
	public class Cliente
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Documento { get; set; }
		public bool Ativo { get; set; }

		public List<Locacao> Locacoes { get; set; }


	}
}
