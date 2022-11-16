using System.ComponentModel;

namespace EComm2022.Web.Models.Marca
{
    public class MarcaListVm
    {
        public int MarcaId { get; set; }

        [DisplayName("Marca")]
        public string NombreMarca { get; set; }
        public bool Activo { get; set; }
    }

}
