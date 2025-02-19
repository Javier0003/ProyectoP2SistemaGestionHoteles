namespace SGHT.Domain.Base
{
    public abstract class Auditoria
    {
        public bool? Estado { get; set; }

        protected Auditoria() {
            this.Estado = true;
        }
        
        public DateTime? FechaCreacion {get; set;}
    }
}
