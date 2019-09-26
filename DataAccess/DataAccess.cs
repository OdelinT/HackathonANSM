
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;


public class DataAccess {
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
