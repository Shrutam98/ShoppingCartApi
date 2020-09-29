using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShoppingCart.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
        public double? Gst { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Category Category { get; set; }
    }
}
