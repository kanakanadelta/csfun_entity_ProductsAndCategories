using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.Collections.Generic;

namespace ProductsAndCategories.Models
{
  public class Product
  {
    [Key]
    public int ProductId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(45)]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [DataType(DataType.Text)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; } 

    public List<Association> Associations { get; set; }

    public Product()
    {
      Associations = new List<Association>();
    }

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
  }
}