using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Net;


public class DataAccess {
    // Les données dans ce fichier contiennent un champ FaussesInitialesPatient.
    // Ces fausses initiales ont été générées dans Excel via la commande "=CONCAT(CAR(ALEA.ENTRE.BORNES(65;90)); " ";CAR(ALEA.ENTRE.BORNES(65;90)))"
    // ATTENTION : ne pas mettre le fichier, qui contient des données sensibles, sur git.
    static private string FakeErreursMedicamenteusesBDCSV = "../Donnees.csv";
    public static List<ErreurMedicamenteuse> GetAllEM()
    {
        try
        {
            string data = File.ReadAllText(FakeErreursMedicamenteusesBDCSV).Replace("\r", "");
            string[] lines = data.Split("\n");
            List<string> fieldNames = lines[0].Split(";").ToList();
            // remove first line (field names)
            lines = lines.Skip(1).ToArray();
            
            List<ErreurMedicamenteuse> result = new List<ErreurMedicamenteuse>();
            foreach(var line in lines)
            {
            string[] values = line.Split(";");
                result.Add(new ErreurMedicamenteuse(){
                    idProduit = values[fieldNames.IndexOf(fieldNames.Where(s => s == "idProduit").FirstOrDefault())],
                    dateReception = values[fieldNames.IndexOf(fieldNames.Where(s => s == "dateReception").FirstOrDefault())],
                    FaussesInitialesPatient = values[fieldNames.IndexOf(fieldNames.Where(s => s == "FaussesInitialesPatient").FirstOrDefault())],
                    FauxRPPSSignalant = values[fieldNames.IndexOf(fieldNames.Where(s => s == "FauxRPPSSignalant").FirstOrDefault())]
                });
            }
            return result;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public static List<ErreurMedicamenteuse> GetEMByRPPS(string RPPS)
    {
        return GetAllEM().Where(em => em.FauxRPPSSignalant == RPPS).ToList();
    }
    public static Medicament GetMedicamentById(string id)
    {
        
        string json = File.ReadAllText("./MockSNIRAM/Medicament.json");
        Medicament medicament =  JsonConvert.DeserializeObject<Medicament>(json);
        if(medicament.NumeroBDM == id)
        {
            return medicament;
        }
        else
        {
            return null;
        }
    }
    public static List<Medicament> GetMedicamentsFromApi()
    {
        string BDMedicamentUrl = "http://base-donnees-publique.medicaments.gouv.fr/telechargement.php?fichier=CIS_COMPO_bdpm.txt";
        List<Medicament> medicaments = new List<Medicament>();
        using (var client = new WebClient())
        {
            // client.DownloadFile(BDMedicamentUrl, "a.mpeg");
            string document = client.DownloadString(BDMedicamentUrl);
            string[] lines = document.Split("\n").Take(100).ToArray();
            foreach(var line in lines)
            {
                string[] values = line.Split("\t");
                medicaments.Add(new Medicament(){
                    NumeroBDM = values[0],
                    Nom = values[3]
                });
            }
        }
        medicaments.Add(GetMedicamentById("123"));
        return medicaments;
    }
    public static Patient GetPatientDataFromNumeroSecuriteSociale(string NumeroSecuriteSociale)
    {
        string json = File.ReadAllText("./MockSNIRAM/Patient.json");
        Patient patient =  JsonConvert.DeserializeObject<Patient>(json);
        if(patient.NumeroSecuriteSociale == NumeroSecuriteSociale)
        {
            return patient;
        }
        else
        {
            return null;
        }
    }
    public static Ordonnance GetOrdonnanceById(string id)
    {
        string json = File.ReadAllText("./MockSNIRAM/Ordonnance.json");
        Ordonnance ordonnance =  JsonConvert.DeserializeObject<Ordonnance>(json);
        if(ordonnance.NumeroOrdonance == id)
        {
            return ordonnance;
        }
        else
        {
            return null;
        }

    }
}
