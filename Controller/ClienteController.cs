using Locadora.Context;
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
		private readonly LocadoraApiContext _context;

		public ClienteController(LocadoraApiContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<Cliente>> CadastrarCliente(Cliente cliente)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto cliente inválido"); }

				LogicaCliente logica = new LogicaCliente(_context);
				List<string> erros = logica.ValidarDados(cliente);

				Cliente clienteExistente = await logica.BuscarClientePorDocumento(cliente.Documento);
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
					Cliente newCliente = await logica.CadastrarCliente(cliente);
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

				LogicaCliente logica = new LogicaCliente(_context);
				List<string> erros = logica.ValidarDados(cliente);

				if (erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Cliente clienteExiste = await logica.BuscarClientePorDocumento(cliente.Documento);
					if(clienteExiste != null)
					{
						Cliente newCliente = await logica.AtualizarCliente(cliente);
						return Ok(newCliente);
					} 
					else
					{
						return BadRequest("Cliente não localizado");
					}
					
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
				LogicaCliente logica = new LogicaCliente(_context);
				return await logica.BuscarTodosClientes();
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
				LogicaCliente logica = new LogicaCliente(_context);
				return await logica.BuscarClientePorDocumento(documento);
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
				LogicaCliente logica = new LogicaCliente(_context);
				return await logica.BuscarClienteId(id);
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar clientes");
			}
		}


	}
}
