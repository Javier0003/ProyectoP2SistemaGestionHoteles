namespace SGHT.Model.Model.Cliente
{
    public class AgregarClienteModel 
    {
        public string? tipoDocumento { get; set; }
        public string? documento { get; set; }
        public string? nombreCompleto { get; set; }
        public string? correo { get; set; }
        public bool estado { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
