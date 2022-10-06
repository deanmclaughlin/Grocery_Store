using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClassroomStart.Models
{
    [Table("orders")]
    [Index("CustomerId", Name = "customer_id")]
    [Index("ProductId", Name = "product_id")]
    public partial class Order
    {
        public Order()
        {
        }

        [Key]
        [Column("orderID", TypeName = "int(10)")]
        public int OrderId { get; set; }

        [Column("customer_id", TypeName = "int(10)")]
        public int CustomerId { get; set; }

        [Column("product_id", TypeName = "int(10)")]
        public int ProductId { get; set; }

        [Column(TypeName = "int(6)")]
        public int QuantitySold { get; set; }

        [Precision(10, 2)]
        public decimal TotalPrice { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey("ProductId")]
        [InverseProperty("Orders")]
        public virtual Inventory Inventory { get; set; } = null!;
    }
}
