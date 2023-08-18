using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DBLayer
{
    public class Order
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double ? Price { get; set; }

    }
}
