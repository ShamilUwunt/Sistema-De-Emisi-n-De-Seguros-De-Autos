using SegurosLafise.Api.Models;
using SegurosLafise.Api.Repositories;

namespace SegurosLafise.Api.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICoberturaRepository _coberturaRepository;

        public CatalogoService(IClienteRepository clienteRepository, ICoberturaRepository coberturaRepository)
        {
            _clienteRepository = clienteRepository;
            _coberturaRepository = coberturaRepository;
        }

        public async Task<List<Cliente>> ObtenerClientesAsync()
        {
            return await _clienteRepository.ObtenerTodosAsync();
        }

        public async Task<List<Cobertura>> ObtenerCoberturasAsync()
        {
            return await _coberturaRepository.ObtenerTodasAsync();
        }
    }
}
