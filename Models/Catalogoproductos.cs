using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace MenuComidaMVC.Models
{
    public class Catalogoproductos
    {
        public static List<ProductoComida> ObtenerProductos()
        {
            return new List<ProductoComida>
            {
                new ProductoComida
                {
                    Nombre = "Hamburguesa Nuclear",
                    Precio = 169.00m,
                    Imagen = "/images/hamburguesa.jpg"
                },

                new ProductoComida
                {
                    Nombre = "Pollo Radiactivo",
                    Precio = 149.00m,
                    Imagen = "/images/pollo.jpg"
                },

                new ProductoComida
                {
                    Nombre = "Alitas Atómicas",
                    Precio = 139.00m,
                    Imagen = "/images/alas.jpg"
                },

                new ProductoComida
                {
                    Nombre = "Boneless del Yermo",
                    Precio = 159.00m,
                    Imagen = "/images/boneles.jpg"
                },

                new ProductoComida
                {
                    Nombre = "Tiras del Refugio",
                    Precio = 129.00m,
                    Imagen = "/images/tiras.jpg"
                },

                new ProductoComida
                {
                    Nombre = "Papas Vault",
                    Precio = 79.00m,
                    Imagen = "/images/papas.jpg"
                },

                new ProductoComida
                {
                    Nombre = "Cajita Pip-Boy",
                    Precio = 189.00m,
                    Imagen = "/images/cajita.jpg"
                },

                new ProductoComida
                {
                    Nombre = "Nuka-Cola",
                    Precio = 45.00m,
                    Imagen = "/images/refresco.jpg"
                },

                new ProductoComida
                {
                    Nombre = "Postre del Yermo",
                    Precio = 69.00m,
                    Imagen = "/images/postres.jpg"
                }
            };

        }
    }
}
