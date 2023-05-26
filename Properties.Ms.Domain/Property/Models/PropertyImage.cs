namespace Properties.Ms.Domain.Property.Models
{
    public class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
    public class PropertyImageRequest
    {
        public int IdProperty { get; set; }
        public string FileBase64 { get; set; }
        public bool Enabled { get; set; }
        public string mimeType { get; set; }
    }
        
}
