using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Domain
{
    public class Amigos
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        [Display(Name = "Data de Aniversário")]
        public DateTime Aniversario { get; set; }

        public int TempoRestante()
        {
            var result = Aniversario - DateTime.Now;

            return result.Days;
        }
    }
}
