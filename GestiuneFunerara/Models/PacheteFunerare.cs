using System;
using System.Collections.Generic;

namespace GestiuneFunerara.Models
{
    public partial class PacheteFunerare
    {
        public PacheteFunerare()
        {
            Precomenzis = new HashSet<Precomenzi>();
        }

        public int id_pachetefunerare { get; set; }
        public string Denumire { get; set; } = null!;
        public string Descriere { get; set; } = null!;
        public decimal Pret { get; set; }
        public string? ImagineURL { get; set; }

        public virtual ICollection<Precomenzi> Precomenzis { get; set; }
    }
}
