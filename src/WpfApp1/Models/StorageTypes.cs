using System.Collections.Generic;

namespace WpfApp1.Models
{
    /// <summary>
    /// Модель, описывающая вид склада.
    /// </summary>
    public partial class StorageTypes
    {
        /// <summary>
        /// Идентификатор пункта выдачи товаров.
        /// </summary>
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