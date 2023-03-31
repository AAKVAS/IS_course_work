using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class WorkerService
    {
        private readonly ISWildberriesContext _context;

        public WorkerService(ISWildberriesContext context)
        {
            _context = context;
        }

        public ObservableCollection<dynamic> GetWorkers()
        {
            return new ObservableCollection<dynamic>(_context.Workers.Include(w => w.Post).ToList());
        }

    }
}
