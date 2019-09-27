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

    public static Ordonnance GetOrdonnanceById(string id)
    {
        return DataAccess.GetOrdonnanceById(id);
    }

    /*
    Dans cette méthode, il s'agira de détailler l'intégralité de ce qui peut être considéré comme un risque :
    - Les interactions possibles avec les traitements déjà suivis par le patient
    - Un nombre conséquent d'erreurs concernant le(s) médicament(s) de la prescription

    Le projet étant seulement pour l'instant à l'état de Proof Of Concept, nous n'étudierons ici qu'un cas nominal simplifié où le patient sera notifié s'il n'y a ne serait-ce qu'une erreur pour l'un des médicaments de la prescription.
     */
    public bool ExisteRisque()
    {
        List<ErreurMedicamenteuse> allEMs = ErreurMedicamenteuse.GetAllEM();
        foreach(var em in allEMs)
        {
            if(medicaments.Count(x => x.NumeroBDM == em.idProduit)>0)
            {
                return true;
            }
        }
        return false;
    }
    public string MessageATransmettre()
    {
        List<ErreurMedicamenteuse> allEMs = ErreurMedicamenteuse.GetAllEM();
        string result = "";
        foreach(var em in allEMs)
        {
            
            Medicament medicamentProblematique = medicaments.Where(x => x.NumeroBDM == em.idProduit).FirstOrDefault();
            if(medicamentProblematique != null)
            {
                result += "\nLe médicament " + medicamentProblematique.Nom + " pose problème.";
            }
        }
        if(result == "")
        {
            result = "\nAucun médicament ne semble poser problème";
        }
        return result;
    }
}