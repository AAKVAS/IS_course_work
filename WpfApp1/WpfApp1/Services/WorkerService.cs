using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;
using WpfApp1.Models.DTO;

namespace WpfApp1.Services
{
    public class WorkerService
    {
        private readonly ISWildberriesContext _context = App.Context;

        public ObservableCollection<dynamic> GetWorkers()
        {
            return new ObservableCollection<dynamic>(_context.Workers.Include(w => w.Post).ToList());
        }

        public Workers GetWorkerById(int id)
        {
            return _context.Workers.Where(w => w.Id == id).Include(w => w.Post).FirstOrDefault() ?? new Workers();
        }

    }
}
