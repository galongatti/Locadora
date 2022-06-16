using System.Collections.Generic;

namespace Locadora.Model
{
	public class Locacao
	{
		public int Id { get; set; }
		public int IdCliente { get; set; }
		public Cliente Cliente { get; set; }
		public List<Filme> FilmesAlugados { get; set; }
	}
}
