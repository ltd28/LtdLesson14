using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeTienDat_241230697.Models;

public partial class Sach
{
    public int MaSach { get; set; }

    [Required(ErrorMessage = "Vui long chon the loai")]
    public int MaTl { get; set; }

    [Required(ErrorMessage = "Ten sach khong duoc de trong")]
    public string TenSach { get; set; } = null!;

    [Required(ErrorMessage = "Vui long nhap gia")]
    [Range(typeof(decimal),"50","1000",ErrorMessage ="Gia phai tu 50 den 1000")]
    public decimal? Gia { get; set; }


    [Required(ErrorMessage = "Vui long nhap ten anh")]
    [RegularExpression(@"^.+\.(jpg|jpeg|png|webp)$",
        ErrorMessage ="Anh phai co duoi .jpg, .jpeg, .png hoac .webp")]
    public string? Anh { get; set; }

    public virtual TheLoai MaTlNavigation { get; set; } = null!;
}
