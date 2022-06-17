﻿using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface IFilmeService : IService<Filme>
	{
		Task<List<Filme>> BuscarTodosInativos();
	}
}
