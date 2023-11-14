using System;
using System.Collections.Generic;

namespace QuanLySach.Models;

public partial class LoaiSach
{
    public int MaLoai { get; set; }

    public string? TenLoai { get; set; }

    public ICollection<Sach> Sach { get; set; }
}
