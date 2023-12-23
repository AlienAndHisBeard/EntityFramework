using EntityFramework.Models;

namespace EntityFramework
{
    public class ShopController
    {

        /// <summary>
        /// Repository used by the shop
        /// </summary>
        private readonly Repository _repository;

        /// <summary>
        /// Selected client/eclient
        /// </summary>
        private Client _currentClient = null;

        /// <summary>
        /// list of selected clients and eclients
        /// </summary>
        private List<Client> _clients = null;

        /// <summary>
        /// filter for the shop clients search
        /// </summary>
        private string _filter = "";

        /// <summary>
        /// specifies when to exit the appliaction
        /// </summary>
        private bool _exit = false;

        /// <summary>
        /// specifies when to return to the main menu
        /// </summary>
        private bool _toMenu = false;

        /// <summary>
        /// Creates shop controller with given repository
        /// </summary>
        /// <param name="repository"></param>
        public ShopController(Repository repository)
        {
            _repository = repository;
        }
        

        /// <summary>
        /// Verifies if the store repository is not empty
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SetUp()
        {
            _clients = await _repository.GetClients(_filter);
            return _clients.Count != 0;
        }

        /// <summary>
        /// Represents main menu of the application
        /// </summary>
        /// <returns></returns>
        public async Task RunShop()
        {
            await ChooseClient();
            while (!_exit)
            {
                Console.Clear();
                Console.WriteLine($"Current user: {_currentClient.Name}");
                Console.WriteLine("Pick 1 to choose your user,\nPick 2 to make a new Order,\nPick 3 to list orders,\nPick anything else to exit\n");

                var value = Console.ReadLine();
                if (value == null) continue;

                if (value.Length == 1 && int.TryParse(value, out var id))
                {
                    switch (id)
                    {
                        case 1:
                            await ChooseClient();
                            break;
                        case 2:
                            await CreateOrder();
                            break;
                        case 3:
                            await OrderList();
                            break;
                        default:
                            _exit = true;
                            break;
                    }
                }
            }

        }

        /// <summary>
        /// Menu to choose used client/eclient, filtered by the user (string) input from the console
        /// </summary>
        /// <returns></returns>
        public async Task ChooseClient()
        {
            _filter = "";
            while (true)
            {
                var i = 0;
                _clients = await _repository.GetClients(_filter);
                Console.Clear();
                Console.WriteLine($"You filter by: {_filter}");
                Console.WriteLine("0-9 pick your user,\n 'reset' to reset filter,\n anything other added to filter on enter");
                foreach (var client in _clients.TakeWhile(_ => i < 10))
                {
                    Console.WriteLine($"{i}. " + client.Id + client.Name);
                    i++;
                }

                var value = Console.ReadLine();

                if (value == null) continue;

                if (value.Length == 1 && int.TryParse(value, out var id))
                {
                    if(id >= _clients.Count) continue;
                    _currentClient = _clients[id];
                    break;
                }

                if (value.Equals("reset")) _filter = "";
                else _filter += value;
            }
        }

        /// <summary>
        /// Menu to create and add the order/eorder (based on the type of selected client) to the store repository
        /// </summary>
        /// <returns></returns>
        public async Task CreateOrder()
        {
            var order = _currentClient is EClient ? new EOrder() : new Order();
            order.Client = _currentClient;
            order.IsCompleted = false;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Pick 1 to add another item,\nPick 2 to save order,\nPick 3 to go back to the menu\n");
                var value = Console.ReadLine();

                if (value is not { Length: 1 } || !int.TryParse(value, out var id)) continue;

                if (id == 2){ await _repository.AddOrder(order); break;}

                if (id == 3) break;

                var items = await _repository.GetItems();

                Console.WriteLine("Pick the item:");
                var i = 0;
                foreach (var item in items)
                {
                    Console.WriteLine($"{i}. " + item.Name + " for: " + item.Price + " usd, available now: " + item.Stock);
                    i++;
                }

                value = Console.ReadLine();
                if (value == null || !int.TryParse(value, out id)) continue;
                var pickedItem = items[id];

                Console.Clear();
                Console.WriteLine($"Picked " + pickedItem.Name + " for: " + pickedItem.Price + " usd, available now: " + pickedItem.Stock);

                while (true)
                {
                    Console.WriteLine("Specify amount to order:");
                    value = Console.ReadLine();
                    if (value == null || !int.TryParse(value, out id)) {Console.WriteLine("Amount must be an integer"); continue;}
                    if (id < 1) {Console.WriteLine("Amount cannot be negative"); continue;}
                    break;
                }

                var orderEntry = new OrderEntry
                {
                    Amount = id,
                    ItemId = pickedItem.Id,
                    OrderId = order.Id,
                };

                order.OrderEntries.Add(orderEntry);
                
            }
        }

        /// <summary>
        /// Menu to list all orders/eorders, grouped in pages, with the ability to complete order/eorder,
        /// order/eorder is only complited if the amount of the products specified by the order/eorder are available in the warehouse 
        /// </summary>
        /// <returns></returns>
        public async Task OrderList()
        {
            var currentPage = 0;
            _toMenu = false;
            var orders = await _repository.GetOrders();
            while (!_toMenu)
            {
                Console.Clear();
                Console.WriteLine($"Page: {currentPage}, p - previous page , n - next page, 0-4 ");
                var i = 0;
                var count = orders.Count - currentPage * 5;
                if (count - 5 > 0) count = 5;

                Console.WriteLine($"id. | itemsAmount | orderTotalPrice | clientName | isEOrder | isCompleted");
                foreach (var order in orders.GetRange(currentPage * 5, count))
                {
                    Console.WriteLine($"{i}. " + order.AmountOfItems() + ", " + order.TotalPrice() + " usd, " + order.Client?.Name + " " + (order is EOrder) + " " + order.IsCompleted);
                    i++;
                }

                var value = Console.ReadLine();
                if (value == null) continue;

                switch (value)
                {
                    case "n":
                        currentPage++;
                        if (orders.Count - currentPage * 5 < 0) currentPage--;
                        break;
                    case "p":
                        if(currentPage != 0) currentPage--;
                        break;
                    case "0":
                        await _repository.CompleteOrder(orders.GetRange(currentPage * 5, count)[0]);
                        orders = await _repository.GetOrders();
                        break;
                    case "1":
                        await _repository.CompleteOrder(orders.GetRange(currentPage * 5, count)[1]);
                        orders = await _repository.GetOrders();
                        break;
                    case "2":
                        await _repository.CompleteOrder(orders.GetRange(currentPage * 5, count)[2]);
                        orders = await _repository.GetOrders();
                        break;
                    case "3":
                        await _repository.CompleteOrder(orders.GetRange(currentPage * 5, count)[3]);
                        orders = await _repository.GetOrders();
                        break;
                    case "4":
                        await _repository.CompleteOrder(orders.GetRange(currentPage * 5, count)[4]);
                        orders = await _repository.GetOrders();
                        break;
                    default:
                        _toMenu = true;
                        break;
                }
            }
        }

    }
}
