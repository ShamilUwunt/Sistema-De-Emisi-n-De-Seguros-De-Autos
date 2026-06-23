namespace SegurosLafise.Api.Services
{
    // Excepcion para errores de reglas de negocio.
    // EsNoEncontrado = true  -> el controlador responde 404
    // EsNoEncontrado = false -> el controlador responde 400
    public class NegocioException : Exception
    {
        public bool EsNoEncontrado { get; }

        public NegocioException(string mensaje, bool esNoEncontrado = false) : base(mensaje)
        {
            EsNoEncontrado = esNoEncontrado;
        }
    }
}
