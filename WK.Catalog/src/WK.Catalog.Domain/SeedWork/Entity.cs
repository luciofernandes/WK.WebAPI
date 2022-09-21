
namespace WK.Catalog.Domain.SeedWork
{
    public abstract class Entity
    {
        public Guid Id { get;  set; } = Guid.NewGuid();

    }
}
