using System.Collections.Generic;

namespace Locadora.Model
{
	public class Cliente : Entity
	{
		public string Nome { get; set; }
		public string Documento { get; set; }
		public bool Ativo { get; set; }

		public List<Locacao> Locacoes { get; set; }


	}
}
