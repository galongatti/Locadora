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


	    public Cliente ObterClientePorDocumento(string documento)
		{
			Cliente cliente = Db.Cliente.AsNoTracking().FirstOrDefault(x => x.Documento.Equals(documento));
			return cliente;
		}

		public override List<Cliente> ObterTodos()
		{
			List<Cliente> cliente = (from c in Db.Cliente.AsNoTracking()
									 where c.Ativo == true
									 select c).ToList();
			return cliente;
		}

		public List<Cliente> ObterTodosInativos()
		{
			List<Cliente> cliente = (from c in Db.Cliente.AsNoTracking()
										  where c.Ativo == false
										  select c).ToList();
			return cliente;
		}
	}
}
