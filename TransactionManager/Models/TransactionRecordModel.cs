using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionManager.Models
{
    public class TransactionRecordModel
    {
        [Key]
        public int TransactionRecordId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal SharePrice { get; set; }
        [Display(Name ="Company")]
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        [Display(Name = "TransactionType")]
        public int TransactionTypeId { get; set; }
        public TransactionTypeModel TransactionType { get; set; }
    }
}
