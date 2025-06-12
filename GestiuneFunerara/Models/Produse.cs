using System;
using System.Collections.Generic;

namespace GestiuneFunerara.Models
{
    public partial class Produse
    {
        public Produse()
        {
            ComenziProduses = new HashSet<ComenziProduse>();
            Precomenzis = new HashSet<Precomenzi>();
        }

        public int id_produs { get; set; }
        public string nume { get; set; } = null!;
        public string? descriere { get; set; }
        public decimal pret_achizitie { get; set; }
        public decimal pret_vanzare { get; set; }
        public int stoc_curent { get; set; }
        public bool este_personalizat { get; set; }
        public string? imagine_url { get; set; }


        public virtual ICollection<ComenziProduse> ComenziProduses { get; set; }
        public virtual ICollection<Precomenzi> Precomenzis { get; set; }
    }
}
