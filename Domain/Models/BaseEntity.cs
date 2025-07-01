namespace Domain.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid(); 
        }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
