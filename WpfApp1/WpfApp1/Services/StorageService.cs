using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class StorageService
    {
        private readonly ISWildberriesContext _context;

        public StorageService(ISWildberriesContext context)
        {
            _context = context;
        }

        public ObservableCollection<Storages> GetPickUpPoints()
        {
            return new ObservableCollection<Storages>(_context.Storages.Where(s => (s.StorageType ?? 0) == StorageTypes.PickUpPointId).ToList());
        }
    }
}
