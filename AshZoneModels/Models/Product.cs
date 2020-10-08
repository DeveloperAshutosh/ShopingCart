using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AshZoneModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace AshZoneModels.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", TypeName = "int")]
        public int ID { get; set; }
        [Required]
        [Column("ProductName", TypeName = "varchar(50)")]
        // Declare the C# property that will map onto that database column.
        public string ProductName { get; set; }
        [Required]
        [Column("ProductType", TypeName = "varchar(50)")]
        // Declare the C# property that will map onto that database column.
        public string ProductType { get; set; }
        [Column("ProductDescription", TypeName = "varchar(1000)")]
        // Declare the C# property that will map onto that database column.
        public string ProductDescription { get; set; }
        [Range(0, int.MaxValue)]
        [Column("Quantity", TypeName = "int")]
        public int Quantity { get; set; }
        [Range(0, int.MaxValue)]
        [Column("Price", TypeName = "int")]
        public int Price { get; set; }
        [Required]
        [DefaultValue("1")]
        [Column("IsAvailable", TypeName = "bit")]
        public bool IsAvailable { get; set; }
        [Column("ImagePath", TypeName = "varchar(100)")]
        public string ImagePath { get; set; }
        [NotMapped]
        public  IFormFile ImageFile{get;set;}

    }
}
