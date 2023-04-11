using System.Collections.Generic;

namespace WpfApp1.Models
{
    public partial class StorageTypes
    {
        public const int PickUpPointId = 1;

        public StorageTypes()
        {
            Storages = new HashSet<Storages>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Storages> Storages { get; set; }

    }
}