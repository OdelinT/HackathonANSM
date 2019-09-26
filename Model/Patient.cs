
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class Patient {

    public string NumeroSecuriteSociale {get; set;}

    public string Nom {get; set;}

    public string Prenom {get; set;}

    public DateTime DateNaissance {get; set;}

    public List<Ordonnance> ordonnances {get; set;}

    public static Patient GetPatientByNumeroSecuriteSociale(string NumeroSecuriteSociale)
    {
        return DataAccess.GetPatientDataFromNumeroSecuriteSociale(NumeroSecuriteSociale);
    }
}