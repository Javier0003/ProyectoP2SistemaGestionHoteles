using System;

namespace SGHT.Domain.Base
{
    public abstract class Auditoria
    {
        public bool? Estatus { get; set; }

        protected Auditoria() {
            this.Estatus = true;
        }
        
        public DateTime? FechaCreacion {get; set;}
        public string? Descripcion { get; set;}
    }
}
