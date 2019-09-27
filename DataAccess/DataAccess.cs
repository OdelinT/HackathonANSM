
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;


public class DataAccess {
    // Les données dans ce fichier contiennent un champ FaussesInitialesPatient.
    // Ces fausses initiales ont été générées dans Excel via la commande "=CONCAT(CAR(ALEA.ENTRE.BORNES(65;90)); " ";CAR(ALEA.ENTRE.BORNES(65;90)))"
    // ATTENTION : ne pas mettre le fichier, qui contient des données sensibles, sur git.
    static private string FakeErreursMedicamenteusesBDCSV = "../Donnees.csv";
    public static List<ErreurMedicamenteuse> GetEMByRPPS(string RPPS)
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
            int RPPSFieldIndex = fieldNames.IndexOf(fieldNames.Where(s => s == "FauxRPPSSignalant").FirstOrDefault());
            if(values[RPPSFieldIndex] == RPPS)
            {
                result.Add(new ErreurMedicamenteuse(){
                    idProduit = values[fieldNames.IndexOf(fieldNames.Where(s => s == "idProduit").FirstOrDefault())],
                    dateReception = values[fieldNames.IndexOf(fieldNames.Where(s => s == "dateReception").FirstOrDefault())],
                    FaussesInitialesPatient = values[fieldNames.IndexOf(fieldNames.Where(s => s == "FaussesInitialesPatient").FirstOrDefault())],
                    FauxRPPSSignalant = values[fieldNames.IndexOf(fieldNames.Where(s => s == "FauxRPPSSignalant").FirstOrDefault())]
                });
            }
        }
        return result;
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
}
