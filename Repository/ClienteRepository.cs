using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

	}
}
