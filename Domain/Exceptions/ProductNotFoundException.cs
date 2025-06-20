using Domain.Exceptions.Base;

namespace Domain.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid ProductId) : base($"the product with {ProductId} Not Found!")
        {
        }
    }
}
