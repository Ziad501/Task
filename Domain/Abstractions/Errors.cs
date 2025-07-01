namespace Domain.Abstractions
{
    public class Errors
    {
        public static readonly Error IdMissMatch= new("DifferentId", "The provided ID does not match the expected ID.");
        public static readonly Error NotFound = new("NotFound", "The requested resource was not found.");
        public static readonly Error CartUpdateFailed = new("CartUpdateFailed", "Unable to update cart");
        public static readonly Error CartDeleteFailed = new ("CartDeleteFailed", "Cart could not be deleted");
        public static readonly Error NoSuchCart = new ("NoSuchCart", "Cart could not be found");

    }

}
