using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ShortUrl
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debes introducir un hash para tu URL")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Debes introducir una URL para tu hash")]
        public string Url { get; set; }
    }
}
