using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class ReviewImage
{
    public int Id { get; set; }
    public int? OrderId { get; set; }
    public byte[]? ReviewImage1 { get; set; }
    public virtual Reviews Review { get; set; }
}
