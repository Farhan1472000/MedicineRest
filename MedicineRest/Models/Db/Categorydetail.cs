using System;
using System.Collections.Generic;

namespace MedicineRest.Models.Db;

public partial class Categorydetail
{
    public int Categoryid { get; set; }

    public string? Categoryname { get; set; }

    public virtual ICollection<Medicinedetail> Medicinedetails { get; set; } = new List<Medicinedetail>();
}
