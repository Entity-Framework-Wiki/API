using System;
using System.Collections.Generic;

namespace API.Modules.Products.DAL.Fournisseur;

public partial class Fournisseur
{
    public int FournisseurId { get; set; }

    public string? Nom { get; set; }

    public string? Contact { get; set; }
}
