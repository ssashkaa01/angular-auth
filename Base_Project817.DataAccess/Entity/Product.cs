using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Base_Project817.DataAccess.Entity
{
    [Table("tblProducts")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
