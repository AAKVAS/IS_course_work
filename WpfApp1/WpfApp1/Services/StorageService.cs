﻿using Microsoft.EntityFrameworkCore;
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
    }
}
