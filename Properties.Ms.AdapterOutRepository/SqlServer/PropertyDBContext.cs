using Microsoft.EntityFrameworkCore;
using Properties.Ms.AdapterOutRepository.SqlServer.Entities;

namespace Properties.Ms.AdapterOutRepository.SqlServer
{
    public class PropertyDBContext : DbContext
    {
        public PropertyDBContext(DbContextOptions<PropertyDBContext> options) : base(options) { }
        public virtual DbSet<PropertyEntity> Property { get; set; }
        public virtual DbSet<PropertyImageEntity> PropertyImage { get; set; }
        public virtual DbSet<PropertyTraceEntity> PropertyTrace { get; set; }
    }
}
