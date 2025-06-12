using System;
using System.Collections.Generic;

namespace GestiuneFunerara.Models
{
    public partial class Precomenzi
    {
        public Precomenzi()
        {
            ComenziProduses = new HashSet<ComenziProduse>();
            ConfirmariComanda = new HashSet<ConfirmariComandum>();
            RapoartePrecomanda = new HashSet<RapoartePrecomandum>();
        }

        public int id_precomenzi { get; set; }
        public int id_utilizator { get; set; }
        public int id_pachetefunerare { get; set; }
        public string NumeDefunct { get; set; } = null!;
        public DateTime? DataDeces { get; set; }
        public string LocatieRidicare { get; set; } = null!;
        public string? Observatii { get; set; }
        public string Stare { get; set; } = null!;
        public DateTime DataCreare { get; set; }
        public string? CodConfirmare { get; set; }
        public int? id_produs { get; set; }

        public virtual PacheteFunerare id_pachetefunerareNavigation { get; set; } = null!;
        public virtual Produse? id_produsNavigation { get; set; }
        public virtual Utilizatori id_utilizatorNavigation { get; set; } = null!;
        public virtual ICollection<ComenziProduse> ComenziProduses { get; set; }
        public virtual ICollection<ConfirmariComandum> ConfirmariComanda { get; set; }
        public virtual ICollection<RapoartePrecomandum> RapoartePrecomanda { get; set; }
       

    }
}
