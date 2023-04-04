using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp1.Models;

public partial class ProductImage
{
    public int Id { get; set; }
    public int? ProductId { get; set; }
    public byte[]? ProductImage1 { get; set; }
    public virtual Products Product { get; set; } = null!;

    [NotMapped]
    public byte[]? Image
    {
        get { return ProductImage1; }
        set { ProductImage1 = value; }
    }
}
