
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class Ordonnance {

    public string NumeroOrdonance {get; set;}

    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public ProfessionnelDeSante prescripteur {get; set;}

    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public DateTime Horodatage {get; set;}

    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public List<Posologie> posologies {get; set;}

    public Patient patient {get; set;}

    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public List<Medicament> medicaments {get; set;}

}