using Locadora.Context;
using Locadora.Interface;
using Locadora.Logica;
using Locadora.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClienteController : ControllerBase
	{
		private readonly IClienteService _clienteService;

		public ClienteController(IClienteService clienteService)
		{
			_clienteService = clienteService;
		}

		/// <summary>
		/// Cadastra um cliente
		/// </summary>
		[HttpPost]
		public ActionResult<Cliente> CadastrarCliente(Cliente cliente)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto cliente inválido"); }

				List<string> erros = _clienteService.ValidarDados(cliente);
				

				Cliente clienteExistente = _clienteService.ObterClientePorDocumento(cliente.Documento);

				if (clienteExistente != null) 
				{
					erros.Add("Já existe um cliente com esse documento");
				}

				if(erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Cliente newCliente = _clienteService.Adicionar(cliente);
					return Ok(newCliente);
				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao cadastrar cliente");
			}
		}

		/// <summary>
		/// Atualiza os dados do cliente
		/// </summary>
		[HttpPut]
		public ActionResult<Cliente> AtualizarCliente(Cliente cliente)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto cliente inválido"); }

				List<string> erros = _clienteService.ValidarDados(cliente);


				Cliente clienteExistente = _clienteService.ObterClientePorDocumento(cliente.Documento);

				if (clienteExistente == null)
				{
					erros.Add("Não existe um cliente com esse documento");
				}

				if (erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Cliente newCliente = _clienteService.Atualizar(cliente);
					return Ok(newCliente);
				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao atualizar cliente");
			}
		}

		/// <summary>
		/// Busca todos os clientes ativos
		/// </summary>
		[HttpGet("BuscarTodosClientes")]
		public ActionResult<List<Cliente>> BuscarTodosClientes()
		{
			try
			{
				return _clienteService.ObterTodos();
			}
			catch (Exception)
			{
				return BadRequest("Erro ao buscar clientes");
			}
		}

		/// <summary>
		/// Busca todos os clientes inativos
		/// </summary>
		[HttpGet("BuscarTodosClientesInativos")]
		public ActionResult<List<Cliente>> BuscarTodosClientesInativos()
		{
			try
			{
				return _clienteService.ObterTodosInativos();
			}
			catch (Exception)
			{
				return BadRequest("Erro ao buscar clientes");
			}
		}

		/// <summary>
		/// Busca um cliente de acordo com seu documento
		/// </summary>
		[HttpGet("BuscarByDoc/{documento}")]
		public ActionResult<Cliente> BuscarClientePorDocumento(string documento)
		{
			try
			{
				Cliente cliente = _clienteService.ObterClientePorDocumento(documento);
				return cliente;
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar clientes");
			}
		}

		/// <summary>
		/// Busca um cliente de acordo com seu id
		/// </summary>
		[HttpGet("BuscarById/{id:int}")]
		public ActionResult<Cliente> BuscarClientePorId(int id)
		{
			try
			{
				return _clienteService.ObterPorId(id);
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar clientes");
			}
		}


	}
}
