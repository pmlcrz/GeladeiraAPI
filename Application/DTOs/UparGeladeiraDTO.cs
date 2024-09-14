using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UparGeladeiraDTO
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
    }
}
