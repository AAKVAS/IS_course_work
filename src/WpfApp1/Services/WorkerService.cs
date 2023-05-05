using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, предоставляющий методы для работы с данными, связанными с сотрудниками.
    /// </summary>
    public class WorkerService
    {
        private readonly static ISWildberriesContext _context = App.Context;

        /// <summary>
        /// Метод, возвращающий список сотрудников.
        /// </summary>
        /// <returns>Сотрудники.</returns>
        public static ObservableCollection<dynamic> GetWorkers()
        {
            return new ObservableCollection<dynamic>(_context.Workers.Include(w => w.Post).ToList());
        }

        /// <summary>
        /// Метод, возвращающий сотрудника по его Id.
        /// </summary>
        /// <param name="id">Id сотрудника.</param>
        /// <returns>Сотрудник.</returns>
        public static Workers GetWorkerById(int id)
        {
            return _context.Workers.Where(w => w.Id == id).Include(w => w.Post).FirstOrDefault() ?? new Workers();
        }
    }
}
