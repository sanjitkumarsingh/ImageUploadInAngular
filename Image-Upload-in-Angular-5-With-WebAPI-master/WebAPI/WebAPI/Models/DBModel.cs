using System.Data.Entity;

namespace WebAPI.Models
{
    public class DBModel: DbContext
    {
        public DBModel()
           : base("name=DBModel")
        {
        }
        public DbSet<Image> Images { get; set; }
    }
}