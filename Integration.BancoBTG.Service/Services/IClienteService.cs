using Integration.BancoBTG.Service.Models;
using Integration.BancoBTG.Service.Util;
using System.Collections.Generic;

namespace Integration.BancoBTG.Service.Services
{
    public interface IClienteService
    {
        Result<IEnumerable<Cliente>> GetAllClientes();
        Result<Cliente> GetClienteById(int id);
    }
}
