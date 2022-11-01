using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class MenuItem : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int VisualizationOrder { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        [Column(name: "CATEGORY_ID")]
        public int CategoryId { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
