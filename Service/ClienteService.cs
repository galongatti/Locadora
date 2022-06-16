using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Logica
{
	public class ClienteService : IClienteService
	{
		private readonly IClienteRepository _clienteRepository;

		public ClienteService(IClienteRepository clienteRepository)
		{
			_clienteRepository = clienteRepository;
		}

		public async Task<Cliente> Adicionar(Cliente cliente)
		{
			await _clienteRepository.Adicionar(cliente);
			return cliente;
		}

		public async Task<Cliente> Atualizar(Cliente cliente)
		{
			await _clienteRepository.Atualizar(cliente);
			return cliente;
		}

		public async Task<Cliente> ObterClientePorDocumento(string documento)
		{
			return await _clienteRepository.ObterClientePorDocumento(documento);
		}

		public async Task<Cliente> ObterPorId(int id)
		{
			return await _clienteRepository.ObterPorId(id);
		}

		public async Task<List<Cliente>> ObterTodos()
		{
			return await _clienteRepository.ObterTodos();
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
	}
}
