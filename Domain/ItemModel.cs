using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ItemModel
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "A posição deve ser um valor entre 1 e 5.")]
        public int Posicao { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "O andar deve ser um valor entre 1 e 4.")]
        public int Andar { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "O container deve ser um valor entre 1 e 4.")]
        public int Container { get; set; }

        [Key]
        [Range(1, 4, ErrorMessage = "O container deve ser um valor entre 1 e .")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
