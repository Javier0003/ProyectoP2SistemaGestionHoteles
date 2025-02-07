namespace SGHT.Domain.Base
{
    public abstract class Auditoria
    {
        protected Auditoria() {
            this.Estatus = true;
        }
        public bool? Estatus {get; set;}

        public DateTime? FechaCreacion {get; set;}
        
        public string? Descripcion { get; set;}
    }
}
