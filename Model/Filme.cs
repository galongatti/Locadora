using System.Collections.Generic;

namespace Locadora.Model
{
	public class Filme : Entity
	{
		public string Nome { get; set; }
		public bool Ativo { get; set; }
		public bool Disponivel { get; set; }
		public List<LocacaoItem> LocacoesItens { get; set; }
	}
}
