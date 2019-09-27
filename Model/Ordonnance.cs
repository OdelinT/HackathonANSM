
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Ordonnance {

    public string NumeroOrdonance {get; set;}

    public ProfessionnelDeSante prescripteur {get; set;}

    public DateTime Horodatage {get; set;}

    public List<Posologie> posologies {get; set;}

    public Patient patient {get; set;}

    public List<Medicament> medicaments {get; set;}

}