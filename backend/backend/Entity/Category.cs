using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entity
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
