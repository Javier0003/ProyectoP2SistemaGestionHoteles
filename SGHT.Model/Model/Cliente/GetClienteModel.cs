namespace SGHT.Model.Model.Cliente
{
    public class GetClienteModel
    {
        public int IdCliente { get; set; }
        public string tipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string nombreCompleto { get; set; }
        public string correoElectronico { get; set; }
        public bool estado { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
