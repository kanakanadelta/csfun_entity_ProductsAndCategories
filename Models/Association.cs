using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;

namespace ProductsAndCategories.Models
{
    public class Association
    {
        [Key]
        public int AssociationId { get; set; }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }

        public Association()
        {

        }

        public Association(
            Product p, 
            Category c, 
            int pId, 
            int cId)
        {
            Product = p;
            Category = c;
            ProductId = pId;
            CategoryId = cId;   
        }

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}