﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhiParcurs.Shared;
public class BaseDomain
{
    public int Id { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
