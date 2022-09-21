using WK.Catalog.Domain.Exceptions;
using WK.Catalog.Domain.SeedWork;
using WK.Catalog.Domain.Validation;

namespace WK.Catalog.Domain.Entity
{
    public class Category : AggregateRoot
    {      
        public string Name { get; private set; }
        public Category(string name):base()
        {                 
            Name = name;         
            Validate();
        }

        public void Update(string name)
        {
            Name = name;
            Validate();
        }
        private void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        }

    }
}
