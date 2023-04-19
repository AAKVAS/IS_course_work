using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp1.Models;

/// <summary>
/// Модель, описывающая изображение к отзыву товара.
/// </summary>
public partial class ReviewImage
{
    public int Id { get; set; }
    public int? OrderId { get; set; }
    public byte[]? ReviewImage1 { get; set; }
    public virtual Reviews Review { get; set; } = null!;

    [NotMapped]
    public byte[]? Image
    {
        get { return ReviewImage1; }
        set { ReviewImage1 = value; }
    }
}
