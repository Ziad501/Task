using Domain.Models;

namespace Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(Guid id);
        Task<Cart?> SetCartAsync(Cart cart);
        Task<bool> DeleteCartASync(Guid id);
    }
}