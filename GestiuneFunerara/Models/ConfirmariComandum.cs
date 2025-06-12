using System;
using System.Collections.Generic;

namespace GestiuneFunerara.Models
{
    public partial class ConfirmariComandum
    {
        public int id_confirmare { get; set; }
        public int id_precomenzi { get; set; }
        public string metoda_confirmare { get; set; } = null!;
        public bool este_confirmat { get; set; }
        public DateTime data_confirmare { get; set; }
        public string inregistrat_de { get; set; } = null!;

        public virtual Precomenzi id_precomenziNavigation { get; set; } = null!;
    }
}
