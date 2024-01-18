using Integration.BancoBTG.Service.Data;
using Integration.BancoBTG.Service.Models;
using Integration.BancoBTG.Service.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Integration.BancoBTG.Service.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Result<IEnumerable<Cliente>> GetAllClientes()
        {
            try
            {
                var clientes = _context.Clientes.ToList();
                return Result<IEnumerable<Cliente>>.Success(clientes);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Cliente>>.Failure(ex.Message);
            }
        }

        public Result<Cliente> GetClienteById(int id)
        {
            try
            {
                var cliente = _context.Clientes
                                      .FirstOrDefault(c => c.ClienteId == id);

                if (cliente == null)
                {
                    return Result<Cliente>.Failure("Cliente não encontrado.");
                }

                return Result<Cliente>.Success(cliente);
            }
            catch (Exception ex)
            {
                return Result<Cliente>.Failure(ex.Message);
            }
        }
    }
}
