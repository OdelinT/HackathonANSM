
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class ErreurMedicamenteuse {
    public string id {get; set;}
    public string moyenTransmissionSource {get; set;}
    public string dateReception {get; set;}
    public string professionTransmissionSource {get; set;}
    public string lieuExerciceTransmissionSource {get; set;}
    public string lieuErreur {get; set;}
    public string initialErreur {get; set;}
    public string natureErreur {get; set;}
    public string causeErreur {get; set;}
    public string populationErreur {get; set;}
    public string qualifErreur {get; set;}
    public string EI {get; set;}
    public string graviteConsequence {get; set;}
    public string idProduit {get; set;}
    public string denomination {get; set;}
    public string DCI {get; set;}
    public string codeATC {get; set;}
    public string voie {get; set;}
    // données générées via =CONCAT(CAR(ALEA.ENTRE.BORNES(65;90)); " ";CAR(ALEA.ENTRE.BORNES(65;90)))
    public string FaussesInitialesPatient {get; set;}
    // On n'est pas certains de disposer directement du numéro RPPS, mais pour simplifier le POC, on va considérer que c'est le cas
    // données générées via =ALEA.ENTRE.BORNES(100000;999999)
    public string FauxRPPSSignalant {get; set;}

    public static List<ErreurMedicamenteuse> GetEMByRPPS(string RPPS)
    {
        return DataAccess.GetEMByRPPS(RPPS);
    }
}