using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        // Get api/Patient/values
        [HttpGet("{ClassName}/{id}")]
        public string Get(string ClassName, string id)
        {
            try{
                if(ClassName == "Patient")
                {
                    Patient patient = Patient.GetPatientByNumeroSecuriteSociale(id);
                    if(patient != null)
                    {
                        return JsonConvert.SerializeObject(patient, Formatting.Indented);
                    }
                    else
                    {
                        return "Aucun patient trouvé pour le numéro de sécurité sociale " + id;
                    }
                }
                else if(ClassName == "EM")
                {
                    List<ErreurMedicamenteuse> EMs = ErreurMedicamenteuse.GetEMByRPPS(id);
                    if(EMs.Count > 0)
                    {
                        return JsonConvert.SerializeObject(EMs, Formatting.Indented);
                    }
                    else
                    {
                        return "Aucune donnée d'EM déclarées par le soignant " + id + " trouvées pour le numéro de sécurité sociale.";
                    }
                }
                return "Aucune table ne correspond à " + ClassName;
            }
            catch(Exception ex)
            {
                return "Erreur au sein de l'appel API : " + ex.StackTrace;
            }
        }
        
        // PUT api/values
        [HttpPut("{ClassName}")]
        public ActionResult<string> Mock(string ClassName)
        {
            string destinationFile = "./MockSNIRAM/";
            if(ClassName == "Patient")
            {
                destinationFile += ClassName + ".json";
                Patient JohnDoe = new Patient(){
                    NumeroSecuriteSociale = "123",
                    Prenom = "John",
                    Nom = "Doe",
                    DateNaissance = new DateTime(1970, 01, 01)
                };
                string json = JsonConvert.SerializeObject(JohnDoe, Formatting.Indented);
                System.IO.File.WriteAllText(destinationFile, json);
            }
            else if(ClassName == "Ordonnance")
            {
                destinationFile += ClassName + ".json";
                List<string> listeMedicamentsId = new List<string>();
                listeMedicamentsId.Add("123");
                List<Medicament> medicaments = new List<Medicament>();
                medicaments.Add(DataAccess.GetMedicamentById("123"));
                Ordonnance OrdonnanceJohnDoe = new Ordonnance(){
                    NumeroOrdonance = "123",
                    medicaments = medicaments,
                    patient = Patient.GetPatientByNumeroSecuriteSociale("123")
                };
                string json = JsonConvert.SerializeObject(OrdonnanceJohnDoe, Formatting.Indented);
                System.IO.File.WriteAllText(destinationFile, json);
            }
            else if(ClassName == "Medicament")
            {
                destinationFile += ClassName + ".json";
                List<string> listeMedicamentsId = new List<string>();
                listeMedicamentsId.Add("123");
                Medicament medicamentQuiPoseProbleme = new Medicament(){
                    NumeroBDM = "123",
                    Nom = "Problématix"
                };
                string json = JsonConvert.SerializeObject(medicamentQuiPoseProbleme, Formatting.Indented);
                System.IO.File.WriteAllText(destinationFile, json);
            }
            return "Mock data created in file " + destinationFile;
        }
    }
}
