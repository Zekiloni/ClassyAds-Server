using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetOrderById(int orderId);
        Task<IEnumerable<Order>> GetAllOrders();
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(Order order);
    }
}
