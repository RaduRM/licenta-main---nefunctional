using System;
using System.Collections.Generic;

namespace GestiuneFunerara.Models
{
    public partial class Utilizatori
    {
        public Utilizatori()
        {
            Precomenzis = new HashSet<Precomenzi>();
        }

        public int id_utilizator { get; set; }
        public string NumeComplet { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefon { get; set; } = null!;
        public string Parola_hashuita { get; set; } = null!;
        public string? Rol { get; set; }
        public DateTime DataInregistrare { get; set; }
        public string? Fotografie { get; set; }
        public string? CodResetare { get; set; }
        public DateTime? CodExpiraLa { get; set; }
        public string? CodActivare { get; set; }
        public bool? EsteActiv { get; set; }

        public virtual ICollection<Precomenzi> Precomenzis { get; set; }
    }
}
