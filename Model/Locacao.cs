using System;
using System.Collections.Generic;

namespace Locadora.Model
{
	public class Locacao
	{
		public int Id { get; set; }
		public Cliente Cliente { get; set; }
		public string Situacao { get; set; }
		public DateTime DataAlocacao { get; set; }
		public DateTime DataParaDevolucao { get; set; }
		public int DiasAlocacao { get; set; }
		public List<LocacoesItens> Itens { get; set; }
		
	}
}
