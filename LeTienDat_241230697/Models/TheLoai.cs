using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LeTienDat_241230697.Models;

public partial class TheLoai
{
    public int MaTl { get; set; }

    public string TenTl { get; set; } = null!;

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
    //public virtual DbSet<Sach> Saches { get; set; }
}
