﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace myapp.api.Models;

public partial class PracownikLadunek
{
    public int? IdPracownika { get; set; }

    public int? IdLadunku { get; set; }

    public virtual Ladunek IdLadunkuNavigation { get; set; }

    public virtual Pracownicy IdPracownikaNavigation { get; set; }
}