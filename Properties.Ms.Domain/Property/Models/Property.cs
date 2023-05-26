
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Properties.Ms.Domain.Property.Models
{
    public class Property
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public short Year { get; set; }
        public int? IdOwner { get; set; }
    }

    public class PropertyRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public short Year { get; set; }
        public int? IdOwner { get; set; }
    }

    public class ChangePricePropertyRequest
    {
        public decimal Price { get; set; }
    }

    public class PropertyResponse
    {
        public string Message { get; set; }
        public sbyte code { get; set; }
    }
}
