using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Locadora.Repository
{
	public class ClienteRepository : Repository<Cliente>, IClienteRepository
	{
		public ClienteRepository(LocadoraApiContext context) : base(context) { }


	    public async Task<Cliente> ObterClientePorDocumento(string documento)
		{
			Cliente cliente = await Db.Cliente.AsNoTracking().FirstOrDefaultAsync(x => x.Documento.Equals(documento));
			return cliente;
		}

		public override async Task<List<Cliente>> ObterTodos()
		{
			List<Cliente> cliente = await (from c in Db.Cliente.AsNoTracking()
									 where c.Ativo == true
									 select c).ToListAsync();
			return cliente;
		}

		public async Task<List<Cliente>> ObterTodosInativos()
		{
			List<Cliente> cliente = await(from c in Db.Cliente.AsNoTracking()
										  where c.Ativo == false
										  select c).ToListAsync();
			return cliente;
		}
	}
}
