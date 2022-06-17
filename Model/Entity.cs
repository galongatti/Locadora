using System;

namespace Locadora.Model
{
	public abstract class Entity
	{
		public int Id { get; set; }
		public DateTime DataInclusao { get; set; }
		public DateTime DataAlteracao { get; set; }
	}
}
