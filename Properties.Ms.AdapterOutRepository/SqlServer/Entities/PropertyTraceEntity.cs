using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Properties.Ms.AdapterOutRepository.SqlServer.Entities
{
    public class PropertyTraceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }
        [ForeignKey("IdProperty")]
        public PropertyEntity PropertyEntity { get; set; }
    }
}
