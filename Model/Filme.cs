using System.Collections.Generic;

namespace Locadora.Model
{
	public class Filme
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public bool Ativo { get; set; }
		public bool Disponivel { get; set; }
		public List<LocacoesItens> LocacoesItens { get; set; }
	}
}
