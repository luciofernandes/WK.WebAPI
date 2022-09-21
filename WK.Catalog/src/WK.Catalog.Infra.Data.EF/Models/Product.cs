using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WK.Catalog.Domain.Entity;


namespace WK.Catalog.Infra.Data.EF.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
