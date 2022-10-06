using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClassroomStart.Models
{
    [Table("inventory")]
    public partial class Inventory
    {
        public Inventory(int productId, string productName, int quantityOnHand, decimal salePrice)
        {
            ProductId = productId;
            ProductName = productName;
            QuantityOnHand = quantityOnHand;
            SalePrice = salePrice;
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("ProductID", TypeName = "int(10)")]
        public int ProductId { get; set; }

        [StringLength(30)]
        public string ProductName { get; set; } = null!;

        [Column(TypeName = "int(6)")]
        public int QuantityOnHand { get; set; }
        
        [Precision(10, 2)]
        public decimal SalePrice { get; set; }

        public bool IsDiscontinued { get; set; }

        [InverseProperty("Inventory")]
        public virtual ICollection<Order>? Orders { get; set; }
    }
}