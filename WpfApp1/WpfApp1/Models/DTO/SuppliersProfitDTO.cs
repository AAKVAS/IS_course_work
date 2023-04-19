using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.DTO
{
    /// <summary>
    /// Объект передачи данных, отображающий доход поставщика.
    /// </summary>
    public class SuppliersProfitDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Profit { get; set; }
    }
}
