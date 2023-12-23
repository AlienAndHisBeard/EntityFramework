using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using EntityFramework.Models;

namespace EntityFramework
{

    /// <summary>
    /// Specify how to create examplary data for the shop 
    /// i.e. Items, Clients, Orders, EClients, EOrders, and order entries.
    /// </summary>
    public static class FakeData
    {
        public static List<Item> Items = new List<Item>();
        public static List<Client> Clients = new List<Client>();
        public static List<Order> Orders = new List<Order>();
        public static List<EClient> EClients = new List<EClient>();
        public static List<EOrder> EOrders = new List<EOrder>();
        public static List<OrderEntry> OrderEntries = new List<OrderEntry>();
        private static int _clientId = 1;
        private static int _orderId = 1;
        private static int _itemId = 1;
        private static int _orderEntryId = 1;
        public static void Init(int count)
        {
            int Id = 1;
            var itemFaker = new Faker<Item>()
                .RuleFor(p => p.Id, _ => _itemId++)
                .RuleFor(p => p.Name, f => f.Name.JobTitle())
                .RuleFor(p => p.Stock, f => (new Random()).Next(10000))
                .RuleFor(p => p.Price, f => (new Random()).Next(1000));

            int clientId = 1;
            var clientFaker = new Faker<Client>()
                .RuleFor(b => b.Id, _ => _clientId++)
                .RuleFor(b => b.Name, f => f.Name.FullName())
                .RuleFor(b => b.Address, f => f.Address.FullAddress())
                .RuleFor(b => b.OrderedPriceSum, 0);

            int eClientId = 1;
            var eClientFaker = new Faker<EClient>()
                .RuleFor(b => b.Id, _ => _clientId++)
                .RuleFor(b => b.Name, f => f.Name.FullName())
                .RuleFor(b => b.Address, f => f.Address.FullAddress())
                .RuleFor(b => b.OrderedPriceSum, 0)
                .RuleFor(b => b.IpAddress, f => f.Internet.Ip());

            int orderEntryId = 1;
            var orderEntriesFaker = new Faker<OrderEntry>()
                .RuleFor(b => b.Id, _ => _orderEntryId++)
                .RuleFor(b => b.ItemId, f =>
                {
                    var item = itemFaker.Generate();
                    FakeData.Items.Add(item);
                    return item.Id;
                })
                .RuleFor(b => b.Amount, f => (new Random()).Next(1000));

            int orderId = 1;
            var orderFaker = new Faker<Order>()
                .RuleFor(b => b.Id, _ => _orderId++)
                .RuleFor(b => b.ClientId, f =>
                {
                    var client = clientFaker.Generate();
                    FakeData.Clients.Add(client);
                    return client.Id;
                })
                .RuleFor(b => b.IsCompleted, false)
                .RuleFor(b => b.OrderEntries, (f, b) => 
                {
                    var orderEntries = orderEntriesFaker.GenerateBetween(3, 5);
                    foreach (var orderEntry in orderEntries)
                    {
                        orderEntry.OrderId = b.Id;
                    }
                    FakeData.OrderEntries.AddRange(orderEntries);
                    return null;
                });

            int eOrderId = 1;
            var eOrderFaker = new Faker<EOrder>()
                .RuleFor(b => b.Id, _ => _orderId++)
                .RuleFor(b => b.ClientId, (f, b) =>
                {
                    var client = eClientFaker.Generate();
                    FakeData.EClients.Add(client);
                    b.IpAddress = client.IpAddress;
                    return client.Id;
                })
                .RuleFor(b => b.IsCompleted, false)
                .RuleFor(b => b.OrderEntries, (f, b) =>
                {
                    var orderEntries = orderEntriesFaker.GenerateBetween(3, 5);
                    foreach (var orderEntry in orderEntries)
                    {
                        orderEntry.OrderId = b.Id;
                    }
                    FakeData.OrderEntries.AddRange(orderEntries);

                    return null;
                });



            var orders = orderFaker.Generate(count);
            FakeData.Orders.AddRange(orders);
            var eOrders = eOrderFaker.Generate(count);
            FakeData.EOrders.AddRange(eOrders);



        }
    }
}
