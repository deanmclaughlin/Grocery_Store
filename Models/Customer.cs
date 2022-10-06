using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClassroomStart.Models
{
    [Table("customer")]
    public partial class Customer
    {
        public Customer(int customerId, string firstName, string lastName, string homeAddress, long phoneNumber)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            HomeAddress = homeAddress;
            PhoneNumber = phoneNumber;
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("CustomerID", TypeName = "int(10)")]
        public int CustomerId { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(50)]
        public string HomeAddress { get; set; } = null!;

        [Column(TypeName = "bigint(10)")]
        public long PhoneNumber { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
