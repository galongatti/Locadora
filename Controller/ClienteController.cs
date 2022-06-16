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

		[HttpPost]
		public async Task<ActionResult<Cliente>> CadastrarCliente(Cliente cliente)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto cliente inválido"); }

				List<string> erros = _clienteService.ValidarDados(cliente);
				

				Cliente clienteExistente = await _clienteService.ObterClientePorDocumento(cliente.Documento);
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
					Cliente newCliente = await _clienteService.Adicionar(cliente);
					return Ok(newCliente);
				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao cadastrar cliente");
			}
		}

		[HttpPut]
		public async Task<ActionResult<Cliente>> AtualizarCliente(Cliente cliente)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto cliente inválido"); }

				List<string> erros = _clienteService.ValidarDados(cliente);


				Cliente clienteExistente = await _clienteService.ObterClientePorDocumento(cliente.Documento);
				if (clienteExistente == null)
				{
					erros.Add("Já existe um cliente com esse documento");
				}

				if (erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Cliente newCliente = await _clienteService.Adicionar(cliente);
					return Ok(newCliente);
				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao atualizar cliente");
			}
		}

		[HttpGet("BuscarTodosClientes")]
		public async Task<ActionResult<List<Cliente>>> BuscarTodosClientes()
		{
			try
			{
				
				return await _clienteService.ObterTodos();
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar clientes");
			}
		}

		[HttpGet("BuscarByDoc/{documento}")]
		public async Task<ActionResult<Cliente>> BuscarClientePorDocumento(string documento)
		{
			try
			{
				Cliente cliente = await _clienteService.ObterClientePorDocumento(documento);
				return cliente;
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar clientes");
			}
		}

		[HttpGet("BuscarById/{id:int}")]
		public async Task<ActionResult<Cliente>> BuscarClientePorId(int id)
		{
			try
			{
				return await _clienteService.ObterPorId(id);
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar clientes");
			}
		}


	}
}
