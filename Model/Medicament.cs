
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class Medicament {

    public string NumeroBDM {get; set;}

    public string Nom {get; set;}

    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public List<Ordonnance> ordonnances {get; set;}

    public Medicament GetMedicamentById(string id)
    {
        return DataAccess.GetMedicamentById(id);
    }
}