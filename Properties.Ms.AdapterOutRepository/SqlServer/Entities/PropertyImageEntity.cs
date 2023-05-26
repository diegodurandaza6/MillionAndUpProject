using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Properties.Ms.AdapterOutRepository.SqlServer.Entities
{
    public class PropertyImageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        [ForeignKey("IdProperty")]
        public PropertyEntity PropertyEntity { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
