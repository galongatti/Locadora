using Locadora.Context;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Logica
{
	public class LogicaCliente
	{
		private readonly LocadoraApiContext _context;
		private readonly DbSet<Cliente> _dbSet;
		public LogicaCliente(LocadoraApiContext context)
		{
			_context = context;
			_dbSet = context.Cliente;
		}

		public List<string> ValidarDados(Cliente cliente)
		{
			List<string> listaErros = new List<string>();

			if (string.IsNullOrEmpty(cliente.Nome))
				listaErros.Add("O nome do cliente deve ser preenchido");

			if(string.IsNullOrEmpty(cliente.Documento))
				listaErros.Add("O nome do cliente documento deve ser preenchido");

			return listaErros;
		}

		public async Task<Cliente> CadastrarCliente(Cliente cliente)
		{
			_context.Cliente.Add(cliente);
			await _context.SaveChangesAsync();
			return cliente;
		}

		public async Task<Cliente> AtualizarCliente(Cliente cliente)
		{
			_context.Cliente.Update(cliente);
			await _context.SaveChangesAsync();
			return cliente;
		}

		public async Task<Cliente> BuscarClientePorDocumento(string documento)
		{
			Cliente cliente = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Documento.Equals(documento));
			return cliente;
		}

		public async Task<Cliente> BuscarClienteId(int id)
		{
			Cliente cliente = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
			return cliente;
		}

		public async Task<List<Cliente>> BuscarTodosClientes()
		{
			List<Cliente> clientes = await _dbSet.AsNoTracking().ToListAsync();
			return clientes;
		}

	}
}
