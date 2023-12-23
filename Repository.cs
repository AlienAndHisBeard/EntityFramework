using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class Repository
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddItem(Item item)
        {
            _context.Items.Add(item);
            _ = await _context.SaveChangesAsync();
        }

        public async Task<Item> GetItem(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<List<Item>> GetItems()
        {
            return await _context.Items.Select(p => new Item()
            {
                Id = p.Id,
                Name = p.Name,
                Stock = p.Stock,
                Price = p.Price

            }).ToListAsync();
        }

        /// <summary>
        /// Get all orders and eorders
        /// </summary>
        /// <param name="contains"></param>
        /// <returns></returns>
        public async Task<List<Client>> GetClients(string contains)
        {
            var client = new List<Client>();
            client.AddRange(_context.Clients.Where(p => p.Name.Contains(contains)));
            client.AddRange(_context.EClients.Where(p => p.Name.Contains(contains)));
            return client;
        }

        public async Task AddToItemStock(Item item, int added)
        {
            _context.Items.Find(item).Stock += added;
            _ = await _context.SaveChangesAsync();
        }
        public async Task AddToItemStock(int id, int added)
        {
            _context.Items.Find(id).Stock += added;
            _ = await _context.SaveChangesAsync();
        }

        public async Task SubtractItemStock(Item item, int subtracted)
        {
            _context.Items.Find(item).Stock -= subtracted;
            _ = await _context.SaveChangesAsync();
        }
        public async Task SubtractItemStock(int id, int subtracted)
        {
            _context.Items.Find(id).Stock -= subtracted;
            _ = await _context.SaveChangesAsync();
        }

        public async Task AddOrder(Order order)
        {
            if (order is EOrder eOrder) _context.EOrders.Add(eOrder);
            else _context.Orders.Add(order);
            _ = await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all orders and eorders
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetOrders()
        {
            var orders = new List<Order>();
            orders.AddRange(_context.Orders.Include(p => p.Client).Include(p => p.OrderEntries).ThenInclude(o => o.Item));
            orders.AddRange(_context.EOrders.Include(p => p.Client).Include(p => p.OrderEntries).ThenInclude(o => o.Item));
            return orders;
        }

        public async Task AddOrderEntry(OrderEntry orderEntry)
        {
            _context.OrderEntries.Add(orderEntry);
            _ = await _context.SaveChangesAsync();
        }


        /// <summary>
        /// try to complete the order, if any of the products amounts go into negative,
        /// rollback the change and mark incompleted
        /// </summary>
        /// <param name="order">Order with order entries to validate</param>
        /// <returns></returns>
        public async Task CompleteOrder(Order order)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            _context.Attach(order).State = EntityState.Modified;

            foreach (var orderEntry in order.OrderEntries)
            {
                if (orderEntry.Item.Stock < orderEntry.Amount)
                {
                    await transaction.RollbackAsync();
                    return;
                }
                orderEntry.Item.Stock -= orderEntry.Amount;
                _context.Attach(orderEntry.Item).State = EntityState.Modified;
            }

            order.IsCompleted = true;

            try
            {
                _ = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await transaction.RollbackAsync();
            }
        }
    }
}
