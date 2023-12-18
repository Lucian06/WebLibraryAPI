using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLibrary.DAL.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }
        public byte[] Cover { get; set; }

        [Required]
        public List<Author> Authors { get; set; }
    }
}
