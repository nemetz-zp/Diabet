using Diabet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.View
{
    public class ExcelYearMedicamentNeedItem
    {
        public Commune Commune { get; set; }
        public Medicament Medicament { get; set; }
        public decimal NumOfTablets { get; set; }
        

        public int numOfPackages
        {
            get
            {
                return Convert.ToInt32(Math.Round(NumOfTablets / Medicament.NumInPack));
            }
        }
    }
}
