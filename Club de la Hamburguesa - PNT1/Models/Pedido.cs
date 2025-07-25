﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [EnumDataType(typeof(Entrega))]
        public Entrega MetodoEntrega { get; set; }

        [EnumDataType(typeof(Pago))]
        public Pago MetodoPago { get; set; }
        public string ? Direccion { get; set; }
        public Estado Estado { get; set; }
        public int UsuarioId { get; set; }
        [ValidateNever]
        public Usuario Usuario { get; set; }

        [EnumDataType(typeof(Bebida))]
        public Bebida BebidaElegida { get; set; }

        [EnumDataType(typeof(Acompañamiento))]
        public Acompañamiento IngredienteAdicional { get; set; }
        public DateTime Fecha { get; set; }
        public ICollection<Item> Items { get; set; }





        public Pedido() {
            Items = new List<Item>();
        }

        public Pedido(Entrega metodoEntrega, Pago metodoPago, string direccion, int usuarioId, Bebida bebida, Acompañamiento acompañamiento)
        {
            MetodoEntrega = metodoEntrega;
            MetodoPago = metodoPago;
            Direccion = direccion;
            UsuarioId = usuarioId;
            Estado = Estado.EnPreparacion;
            BebidaElegida = bebida;
            IngredienteAdicional = acompañamiento;
            Items = new List<Item>();
        }

        public double ObtenerMontoTotal()
        {
            double total = 0;

            foreach (var item in Items)
            {
                total += item.Hamburguesa.Precio * item.Cantidad;
            }

            total += (int)BebidaElegida;
            total += (int)IngredienteAdicional;

            return total;
        }
    }


}
