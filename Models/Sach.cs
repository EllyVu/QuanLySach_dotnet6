using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace QuanLySach.Models;


public partial class Sach
{

    [Key]
    public int Masach { get; set; }

    [ForeignKey(nameof(NhaXuatBan))]
    public int? Maxb { get; set; }

    [ForeignKey (nameof(LoaiSach))]

    public int? Maloai { get; set; }

    public string? Tensach { get; set; }

    public string? Tacgia { get; set; }

    public LoaiSach LoaiSach { get; set; }

    public NhaXuatBan NhaXuatBan { get; set; }
}
