
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Patient {

    private string NumeroSecuriteSociale {get; set;}

    private string Nom {get; set;}

    private string Prenom {get; set;}

    private DateTime DateNaissance {get; set;}

    public List<Ordonnance> ordonnances {get; set;}

}