//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoProgra2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ESTUDIANTES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESTUDIANTES()
        {
            this.INSCRIPCION_ESTUDIANTES = new HashSet<INSCRIPCION_ESTUDIANTES>();
            this.COLA_ESTUDIANTES = new HashSet<COLA_ESTUDIANTES>();
        }
    
        public string CARNET { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<System.DateTime> FECHA_NACIMIENTO { get; set; }
        public Nullable<System.DateTime> FECHA_REGISTRO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSCRIPCION_ESTUDIANTES> INSCRIPCION_ESTUDIANTES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COLA_ESTUDIANTES> COLA_ESTUDIANTES { get; set; }
    }
}
