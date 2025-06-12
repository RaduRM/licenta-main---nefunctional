using System;
using System.Collections.Generic;

namespace GestiuneFunerara.Models
{
    public partial class ComenziProduse
    {
        public int id_comanda_produs { get; set; }
        public int id_precomenzi { get; set; }
        public int id_produs { get; set; }
        public string tip_livrare { get; set; } = null!;
        public string status_livrare { get; set; } = null!;
        public DateTime data_necesara { get; set; }

        public virtual Precomenzi id_precomenziNavigation { get; set; } = null!;
        public virtual Produse id_produsNavigation { get; set; } = null!;
    }
}
