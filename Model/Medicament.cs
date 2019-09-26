
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Medicament {

    public Medicament() {
    }

    public string NumeroBDM {get; set;}

    public string Nom {get; set;}

    public List<Ordonnance> ordonnances {get; set;}

}