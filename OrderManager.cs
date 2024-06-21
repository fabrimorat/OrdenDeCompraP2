namespace OrdenDeCompraP2
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int OrderId { get; set; }
        public string PokemonName { get; set; }
        public int Quantity { get; set; }
        public string CustomerInfo { get; set; }
    }

    public class OrderManager
    {
        private List<Order> orders = new List<Order>();

        // Método para añadir órdenes con los parámetros correctos
        public void AddOrder(string pokemonName, int quantity, string customerInfo)
        {
            orders.Add(new Order
            {
                PokemonName = pokemonName,
                Quantity = quantity,
                CustomerInfo = customerInfo
            });
        }

        // Método para obtener todas las órdenes
        public List<Order> GetAllOrders()
        {
            return orders;
        }
    }
}
