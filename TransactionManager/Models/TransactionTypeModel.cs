using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionManager.Models
{
    public class TransactionTypeModel
    {
        [Key]
        public int TransactionTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Commission { get; set; }
    }
}
