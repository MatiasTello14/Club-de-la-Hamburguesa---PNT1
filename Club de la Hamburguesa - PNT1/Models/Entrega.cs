using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public enum Entrega
    {
        [Display(Name = "Retiro en local")]
        RetiroLocal = 0,
        [Display(Name = "Envío a domicilio")]
        EnvioDomicilio = 1
    }
}
