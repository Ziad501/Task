
using Domain.Models;

namespace Domain.Repository.IRepository
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(Guid id);
        Task<Cart?> SetCartAsync(Cart cart);
        Task<bool> DeleteCartASync(Guid id);

    }
}
