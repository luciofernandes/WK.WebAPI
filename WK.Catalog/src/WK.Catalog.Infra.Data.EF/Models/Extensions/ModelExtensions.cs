using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK.Catalog.Infra.Data.EF.Models.Extensions
{
    public static class ModelExtensions
    {
        public static Models.Category ToModel(this Domain.Entity.Category category)
        {
  
            Models.Category categoryModel = new Models.Category();
            categoryModel.Name = category.Name;
            categoryModel.Id = category.Id;
            
            return categoryModel;
        }
        public static Models.Product ToModel(this Domain.Entity.Product product)
        {
            Models.Product productModel = new Models.Product();
            productModel.Name = product.Name;
            productModel.Id = product.Id; 
            productModel.Description = product.Description;      
            productModel.CategoryId = product.Category!.Id;
            return productModel;
        }
        public static Domain.Entity.Category ToEntity(this Models.Category category)
        {
            Domain.Entity.Category categoryEntity = new Domain.Entity.Category(category.Name);
            categoryEntity.Id = category.Id;
            return categoryEntity;
        }
        public static Domain.Entity.Product ToEntity(this Models.Product product)
        {
            Domain.Entity.Product productModel = new Domain.Entity.Product(product.Name,
                product.Description,
                product.Category?.ToEntity()
            );
            productModel.Id = product.Id;
            return productModel;
        }

    }
   
}
