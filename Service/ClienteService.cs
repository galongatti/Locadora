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

		public Cliente Adicionar(Cliente cliente)
		{
			 _clienteRepository.Adicionar(cliente);
			return cliente;
		}

		public Cliente Atualizar(Cliente cliente)
		{
			 _clienteRepository.Atualizar(cliente);
			return cliente;
		}

		public Cliente ObterClientePorDocumento(string documento)
		{
			return _clienteRepository.ObterClientePorDocumento(documento);
		}

		public Cliente ObterPorId(int id)
		{
			return  _clienteRepository.ObterPorId(id);
		}

		public List<Cliente> ObterTodos()
		{
			return _clienteRepository.ObterTodos();
		}

		public List<Cliente> ObterTodosInativos()
		{
			return  _clienteRepository.ObterTodosInativos();
		}

		public List<string> ValidarDados(Cliente cliente)
		{
			List<string> listaErros = new List<string>();

			if (string.IsNullOrEmpty(cliente.Nome))
				listaErros.Add("O nome do cliente deve ser preenchido");

			if(string.IsNullOrEmpty(cliente.Documento))
				listaErros.Add("O documento deve ser preenchido");

			return listaErros;
		}
	}
}
