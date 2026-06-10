
using System.Linq.Expressions;
using ECommerce.SharedLibrary.Interface;
using OrderApi.Domain.Entites;

namespace OrderApi.Application.Interface
{
    public interface IOrder: IGenericInterface<Order>
    {
        Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate);
    }
}
