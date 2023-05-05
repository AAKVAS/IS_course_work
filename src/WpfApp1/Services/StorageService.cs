using Microsoft.Data.SqlClient;
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
    /// Класс, предоставляющий методы для работы с данными, связанными со складами.
    /// </summary>
    public class StorageService
    {
        private static readonly ISWildberriesContext _context = App.Context;

        /// <summary>
        /// Метод, возвращающий коллекцию пунктов выдачи заказов.
        /// </summary>
        /// <returns>Пункты выдачи товаров.</returns>
        public static List<Storages> GetPickUpPoints()
        {
            return _context.Storages.Where(s => (s.StorageType ?? 0) == StorageTypes.PickUpPointId).ToList();
        }

        /// <summary>
        /// Метод, возвращающий коллекцию складов. Используется для раздела "Склады / Общие сведения".
        /// </summary>
        /// <returns>Коллекция складов.</returns>
        public static ObservableCollection<dynamic> GetStoragesGeneralInfo()
        {
            return new ObservableCollection<dynamic>(_context.Storages.Include(s => s.StorageTypeNavigation).ToList());
        }

        /// <summary>
        /// Метод, возвращающий коллекцию поступлений товаров на склады. Используется для раздела "Склады / Поступления на склады".
        /// </summary>
        /// <returns>Поступления товаров на склады.</returns>
        public static ObservableCollection<dynamic> GetStorageReceipts()
        {
            return new ObservableCollection<dynamic>(
                _context.ReceiptOfProductsToStorages
                    .Include(r => r.Product)
                    .Include(r => r.Storage)
                        .ThenInclude(s => s.StorageTypeNavigation)
                    .ToList());
        }

        /// <summary>
        /// Метод, возвращающий коллекцию смен сотрудников на складе. Используется для раздела "Склады / Работа сотрудников на складе".
        /// </summary>
        /// <returns>Смены сотрудников на складе.</returns>
        public static ObservableCollection<dynamic> GetStorageWorkerShifts()
        {
            return new ObservableCollection<dynamic>(
                _context.StorageWorkerShifts
                    .Include(s => s.Worker)
                        .ThenInclude(w => w.Post)
                    .Include(s => s.Storage)
                        .ThenInclude(s => s.StorageTypeNavigation)
                    .ToList());
        }

        /// <summary>
        /// Метод, выполняющий добавление смены сотрудника на складе в БД.
        /// </summary>
        /// <param name="storageWorkerShifts">Смена сотрудника.</param>
        public static void InsertStorageWorkerShift(StorageWorkerShifts storageWorkerShifts)
        {
            string query = @"INSERT INTO storage_worker_shifts (
                                        storage_id,
	                                    started_shift_at, 
	                                    finished_shift_at,
	                                    worker_id
                                )
                                VALUES (
                                        @storage_id,
	                                    @started_shift_at, 
	                                    @finished_shift_at,
	                                    @worker_id
                                )";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@storage_id", storageWorkerShifts.Storage.Id),
                new SqlParameter("@started_shift_at", storageWorkerShifts.StartedShiftAt),
                new SqlParameter("@finished_shift_at", storageWorkerShifts.FinishedShiftAt),
                new SqlParameter("@worker_id", storageWorkerShifts.Worker.Id)
            };
            _context.Database.ExecuteSqlRaw(query, parameters);
        }

        /// <summary>
        /// Метод, возвращающий количество продуктов на складах.
        /// </summary>
        /// <returns>Количество продуктов на складах.</returns>
        public static ObservableCollection<dynamic> GetStoragesProductAmount()
        {
            return new ObservableCollection<dynamic>(
                _context.ProductsOnStorages
                    .Include(p => p.Product)
                    .Include(p => p.Storage)
                        .ThenInclude(s => s.StorageTypeNavigation)
                    .ToList());
        }
    }
}
