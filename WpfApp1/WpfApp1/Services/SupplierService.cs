using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, предоставляющий методы для работы с данными, связанными с поставщиками.
    /// </summary>
    public class SupplierService
    {
        private static readonly ISWildberriesContext _context = App.Context;

        /// <summary>
        /// Метод, возвращающий коллекцию поставщиков. Используется для раздела "Поставщики / Общие сведения".
        /// </summary>
        /// <returns>Поставщики.</returns>
        public static ObservableCollection<dynamic> GetSuppliersGeneralInfo()
        {
            return new ObservableCollection<dynamic>(_context.Suppliers.ToList());
        }

        /// <summary>
        /// Метод, возвращающий прибыль поставщиков. Используется для раздела "Поставщики / Прибыль".
        /// </summary>
        /// <returns>Прибыль поставщиков.</returns>
        public static ObservableCollection<dynamic> GetSuppliersProfits()
        {
            string query = @"SELECT 
                                    s.id as Id,
                                    s.title as Title, 
                                    ISNULL(ROUND(SUM(p.supplier_percent / 100 * o.price), 2), 0) as Profit
                               FROM 
                                     suppliers s
                                LEFT JOIN products p ON p.supplier_id = s.id
                                LEFT JOIN orders o   ON o.product_id  = p.id
                                GROUP BY 
                                        s.id,
                                        s.title";

            return new ObservableCollection<dynamic> (_context.SuppliersProfitDTO.FromSqlRaw(query).ToList());
        }
    }
}
