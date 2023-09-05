using System;
using System.Collections.Generic;

namespace MedicineRest.Models.Db;

public partial class Medicinedetail
{
    public int? Id { get; set; }

    public int? Categoryid { get; set; }

    public string? Mediname { get; set; }

    public int? Prize { get; set; }

    public string? Expirydate { get; set; }

    public int? Stocklvl { get; set; }

    public virtual Categorydetail? Category { get; set; }
}
