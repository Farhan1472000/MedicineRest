using System;
using System.Collections.Generic;

namespace MedicineRest.Models.Db;

public partial class Logindetail
{
    public int? Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
