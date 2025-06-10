using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public enum Estado
    {
        [Display(Name = "En preparacion")]
        EnPreparacion,
        [Display(Name = "Listo para retirar")]
        ListoParaRetirar,
        [Display(Name = "Pedido enviado a domicilio")]
        EnviadoADomicilio,
        [Display(Name = "Recibido")]
        Recibido
    }
}
