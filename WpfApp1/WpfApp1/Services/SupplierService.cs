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
    public class SupplierService
    {
        private static readonly ISWildberriesContext _context = App.Context;

        public ObservableCollection<dynamic> GetSuppliersGeneralInfo()
        {
            return new ObservableCollection<dynamic>(_context.Suppliers.ToList());
        }

        public ObservableCollection<dynamic> GetSuppliersProfits()
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
