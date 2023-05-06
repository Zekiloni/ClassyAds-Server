using Microsoft.EntityFrameworkCore;
using MyAds.Entities;
using MyAds.Interfaces;

namespace MyAds.Services
{
    public class OrderService : IOrderService
    {
        private readonly Context _database;

        public OrderService(Context database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _database.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderById(int classifiedId)
        {
            return await _database.Orders.FirstOrDefaultAsync(classified => classified.Id == classifiedId);
        }

        public async Task CreateOrder(Order classified)
        {
            await _database.Orders.AddAsync(classified);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteOrder(Order classified)
        {
            _database.Orders.Remove(classified);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            order.UpdatedAt = DateTime.Now;
            _database.Entry(order).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }
    }
}
