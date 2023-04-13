﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WpfApp1.Models
{
    public partial class StorageWorkerShifts : ICopied<StorageWorkerShifts>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int StorageId { get; set; }

        private DateTime? _startedShiftAt;
        public DateTime StartedShiftAt 
        {
            get
            {
                return _startedShiftAt ?? DateTime.Now;
            }
            set
            {
                _startedShiftAt = value;
                FinishedShiftAt = StartedShiftAt.AddHours(8);
            }
        }

        private DateTime? _finishedShiftAt;
        public DateTime? FinishedShiftAt 
        { 
            get
            {
                return _finishedShiftAt ?? StartedShiftAt.AddHours(8);
            }
            set
            {
                _finishedShiftAt = value;
                OnPropertyChanged();
            }
        }

        public virtual Storages Storage { get; set; }
        public virtual Workers Worker { get; set; }

        public StorageWorkerShifts Clone()
        {
            StorageWorkerShifts storageWorkerShifts = new StorageWorkerShifts();
            storageWorkerShifts.Id = Id;
            storageWorkerShifts.WorkerId = WorkerId;
            storageWorkerShifts.StorageId = StorageId;
            storageWorkerShifts.StartedShiftAt = StartedShiftAt;
            storageWorkerShifts.FinishedShiftAt = FinishedShiftAt;
            storageWorkerShifts.Storage = Storage;
            storageWorkerShifts.Worker = Worker;
            return storageWorkerShifts;
        }

        public void Copy(StorageWorkerShifts storageWorkerShifts)
        {
            Id = storageWorkerShifts.Id;
            WorkerId = storageWorkerShifts.WorkerId;
            StorageId = storageWorkerShifts.StorageId;
            StartedShiftAt = storageWorkerShifts.StartedShiftAt;
            FinishedShiftAt = storageWorkerShifts.FinishedShiftAt;
            Storage = storageWorkerShifts.Storage;
            Worker = storageWorkerShifts.Worker;
        }


    }
}