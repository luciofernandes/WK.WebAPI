using System.ComponentModel.DataAnnotations.Schema;
using WK.Catalog.Domain.SeedWork;
using WK.Catalog.Domain.Validation;

namespace WK.Catalog.Domain.Entity
{
    public class Product : AggregateRoot
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public Category? Category { get; private set; }
        public Product(string? name, string? description, Category? category) : base()
        {
            Name = name;
            Description = description;
            Category = category;
            Validate();
        }

        public void Update(string name,string description, Category category)
        {
            Name = name;
            Description = description;
            Category = category;
            Validate();
        }
        private void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
            DomainValidation.NotNullOrEmpty(Description, nameof(Description));
        }
    }
}
