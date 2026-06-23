using SegurosLafise.Api.Dtos;
using SegurosLafise.Api.Models;
using SegurosLafise.Api.Repositories;

namespace SegurosLafise.Api.Services
{
    public class PolizaService : IPolizaService
    {
        private readonly IPolizaRepository _polizaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ICoberturaRepository _coberturaRepository;

        public PolizaService(
            IPolizaRepository polizaRepository,
            IClienteRepository clienteRepository,
            ICoberturaRepository coberturaRepository)
        {
            _polizaRepository = polizaRepository;
            _clienteRepository = clienteRepository;
            _coberturaRepository = coberturaRepository;
        }

        public async Task<PolizaDetalleDto> EmitirAsync(EmitirPolizaDto datos)
        {
            // 1. Validar que el cliente exista
            var cliente = await _clienteRepository.ObtenerPorIdAsync(datos.ClienteId);
            if (cliente == null)
            {
                throw new NegocioException($"No existe el cliente con Id {datos.ClienteId}.", esNoEncontrado: true);
            }

            // 2. Validar las coberturas seleccionadas
            var idsUnicos = datos.CoberturasIds.Distinct().ToList();
            var coberturas = await _coberturaRepository.ObtenerPorIdsAsync(idsUnicos);
            if (coberturas.Count != idsUnicos.Count)
            {
                throw new NegocioException("Una o mas coberturas seleccionadas no existen.");
            }

            // 3. Crear el vehiculo con los datos recibidos
            var vehiculo = new Vehiculo
            {
                Placa = datos.Vehiculo.Placa,
                Marca = datos.Vehiculo.Marca,
                Modelo = datos.Vehiculo.Modelo,
                Anio = datos.Vehiculo.Anio,
                ValorComercial = datos.Vehiculo.ValorComercial
            };

            // 4. Calcular montos (logica en el servidor)
            //    SumaAsegurada = valor comercial del auto
            //    PrimaTotal    = suma de los montos de las coberturas
            decimal sumaAsegurada = datos.Vehiculo.ValorComercial;
            decimal primaTotal = coberturas.Sum(c => c.MontoCobertura);

            // 5. Generar el numero de poliza automatico
            int anioActual = DateTime.Now.Year;
            int cantidad = await _polizaRepository.ContarPorAnioAsync(anioActual);
            string numeroPoliza = $"POL-{anioActual}-{(cantidad + 1):D5}";

            // 6. Armar la poliza
            var poliza = new Poliza
            {
                NumeroPoliza = numeroPoliza,
                ClienteId = cliente.Id,
                Vehiculo = vehiculo,
                FechaEmision = DateTime.Now,
                SumaAsegurada = sumaAsegurada,
                PrimaTotal = primaTotal,
                PolizaCoberturas = coberturas
                    .Select(c => new PolizaCobertura { CoberturaId = c.Id })
                    .ToList()
            };

            await _polizaRepository.AgregarAsync(poliza);

            // Volver a leer con todos los datos para devolver el detalle completo
            var creada = await _polizaRepository.ObtenerPorIdAsync(poliza.Id);
            return MapearADetalle(creada!);
        }

        public async Task<PolizaDetalleDto?> ObtenerPorIdAsync(int id)
        {
            var poliza = await _polizaRepository.ObtenerPorIdAsync(id);
            if (poliza == null)
            {
                return null;
            }
            return MapearADetalle(poliza);
        }

        public async Task<List<PolizaDetalleDto>> ObtenerTodasAsync()
        {
            var polizas = await _polizaRepository.ObtenerTodasAsync();
            return polizas.Select(MapearADetalle).ToList();
        }

        // Convierte la entidad Poliza al DTO de respuesta
        private static PolizaDetalleDto MapearADetalle(Poliza poliza)
        {
            return new PolizaDetalleDto
            {
                Id = poliza.Id,
                NumeroPoliza = poliza.NumeroPoliza,
                FechaEmision = poliza.FechaEmision,
                SumaAsegurada = poliza.SumaAsegurada,
                PrimaTotal = poliza.PrimaTotal,
                ClienteNombre = poliza.Cliente?.Nombre ?? string.Empty,
                ClienteIdentificacion = poliza.Cliente?.Identificacion ?? string.Empty,
                VehiculoPlaca = poliza.Vehiculo?.Placa ?? string.Empty,
                VehiculoMarca = poliza.Vehiculo?.Marca ?? string.Empty,
                VehiculoModelo = poliza.Vehiculo?.Modelo ?? string.Empty,
                VehiculoAnio = poliza.Vehiculo?.Anio ?? 0,
                Coberturas = poliza.PolizaCoberturas
                    .Where(pc => pc.Cobertura != null)
                    .Select(pc => new CoberturaDto
                    {
                        Id = pc.Cobertura!.Id,
                        Nombre = pc.Cobertura.Nombre,
                        MontoCobertura = pc.Cobertura.MontoCobertura
                    })
                    .ToList()
            };
        }
    }
}
