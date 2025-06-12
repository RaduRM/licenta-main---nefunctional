using System;
using System.Collections.Generic;

namespace GestiuneFunerara.Models
{
    public partial class RapoartePrecomandum
    {
        public int id_rapoarteprecomanda { get; set; }
        public int id_precomenzi { get; set; }
        public string nume_client { get; set; } = null!;
        public string telefon_client { get; set; } = null!;
        public string nume_defunct { get; set; } = null!;
        public string locatie_decedat { get; set; } = null!;
        public DateTime data_raport { get; set; }
        public string? pdf_url { get; set; }
        public string? locatie_tip { get; set; }

        public virtual Precomenzi id_precomenziNavigation { get; set; } = null!;
    }
}
