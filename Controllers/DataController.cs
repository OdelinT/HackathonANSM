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
            return "Aucune table ne correspond à " + ClassName;
        }
        
        // PUT api/values
        [HttpPut("{ClassName}")]
        public ActionResult<string> Mock(string ClassName)
        {
            string destinationFile = "./MockSNIRAM/";
            if(ClassName == "Patient")
            {
                destinationFile += "Patient.json";
                Patient JohnDoe = new Patient(){
                    NumeroSecuriteSociale = "123",
                    Prenom = "John",
                    Nom = "Doe",
                    DateNaissance = new DateTime(1970, 01, 01)
                };
                string json = JsonConvert.SerializeObject(JohnDoe, Formatting.Indented);
                System.IO.File.WriteAllText(destinationFile, json);
            }
            return "Mock data created in file " + destinationFile;
        }
    }
}
