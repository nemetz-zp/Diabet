using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diabet.Models;

namespace Diabet.View
{
    /// <summary>
    /// Годовой отчет по сумме выданых медикаментов и накопленых по ним дозам
    /// </summary>
    public class MedicamentYearSumDozage
    {
        public Medicament Medicament { get; set; }
        public decimal DayDoze { get; set; }
        public int TabsSum { get; set; }
        public string YearSumDozage
        {
            get
            {
                return string.Format("{0} {1}", 
                    TabsSum, 
                    Medicament.MedicamentType);
            }
        }
    }
}
