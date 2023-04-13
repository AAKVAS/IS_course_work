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
    }
}
