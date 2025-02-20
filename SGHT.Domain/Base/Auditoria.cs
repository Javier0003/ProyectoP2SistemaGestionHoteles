namespace SGHT.Domain.Base
{
    public abstract class Auditoria
    {
        public bool? Estado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string? Descricion { get; set; }

        protected Auditoria() {
            this.Estado = true;
        }
        
       
    }
}
