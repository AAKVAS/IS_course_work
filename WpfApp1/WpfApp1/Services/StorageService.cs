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
    public class StorageService
    {
        private static readonly ISWildberriesContext _context = App.Context;

        public ObservableCollection<Storages> GetPickUpPoints()
        {
            return new ObservableCollection<Storages>(_context.Storages.Where(s => (s.StorageType ?? 0) == StorageTypes.PickUpPointId).ToList());
        }

        public ObservableCollection<dynamic> GetStoragesGenerealInfo()
        {
            return new ObservableCollection<dynamic>(_context.Storages.Include(s => s.StorageTypeNavigation).ToList());
        }

        public ObservableCollection<dynamic> GetStorageReceipts()
        {
            return new ObservableCollection<dynamic>(
                _context.ReceiptOfProductsToStorages
                    .Include(r => r.Product)
                    .Include(r => r.Storage)
                        .ThenInclude(s => s.StorageTypeNavigation)
                    .ToList());
        }

        public ObservableCollection<dynamic> GetStorageWorkerShifts()
        {
            return new ObservableCollection<dynamic>(
                _context.StorageWorkerShifts
                    .Include(s => s.Worker)
                        .ThenInclude(w => w.Post)
                    .Include(s => s.Storage)
                        .ThenInclude(s => s.StorageTypeNavigation)
                    .ToList());
        }

        public void InsertStorageWorkerShift(StorageWorkerShifts storageWorkerShifts)
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

        public ObservableCollection<dynamic> GetStoragesProductAmount()
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
