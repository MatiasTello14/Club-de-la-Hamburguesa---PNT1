using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public enum Acompañamiento
    {
        [Display(Name = "Papas con cheddar")]
        PapasConCheddar = 4000,
        [Display(Name = "Aros de cebolla")]
        ArosDeCebolla = 3000 ,
        [Display(Name = "Palitos de muzzarella")]
        PalitosDeMuzzarella = 3500,
        [Display(Name = "Batatas con cheddar")]
        BatatasConCheddar = 3500,
        Ninguno = 0
    }
}
