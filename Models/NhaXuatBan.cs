using System;
using System.Collections.Generic;

namespace QuanLySach.Models;

public partial class NhaXuatBan
{
    public int MaXb { get; set; }

    public string? Tenxb { get; set; }

    public string? DiaChi { get; set; }

    public string? GhiChu { get; set; }

    public ICollection<Sach> Sach { get; set; }
}
