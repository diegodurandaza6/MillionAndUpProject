using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Properties.Ms.AdapterOutRepository.SqlServer.Entities
{
    public class PropertyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public short Year { get; set; }
        public int? IdOwner { get; set; }
    }
}
