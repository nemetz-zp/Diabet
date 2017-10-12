using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.View
{
    public class MedicamentTotalsForExcel
    {
        public List<string> MedicamentTabletCells { get; set; }
        public List<string> MedicamentPackageCells { get; set; }

        public decimal Price { get; set; }

        public MedicamentTotalsForExcel()
        {
            MedicamentTabletCells = new List<string>();
            MedicamentPackageCells = new List<string>();
        }

        public string GetTabletsSumFormula()
        {
            return "=" + string.Join("+", MedicamentTabletCells);
        }
        public string GetPackagesSumFormula()
        {
            return "=" + string.Join("+", MedicamentPackageCells);
        }
    }
}
