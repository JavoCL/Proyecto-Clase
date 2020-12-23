using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PruebaJson : MonoBehaviour
{
    public DatosPlayer datosCurrentPlayer;
    public List<DatosPlayer> listaPlayers;

    [SerializeField]
    private string id;
    // Start is called before the first frame update
    void Start()
    {
        LeeJson();
        Debug.Log("DATAPATH: " + Application.dataPath);
        Debug.Log("PERSISTENT DATAPATH: " + Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeeJson()
    {
        string jsonLectura = "";

        jsonLectura = File.ReadAllText(Application.persistentDataPath + "/jsonEjemplo.js");

        datosCurrentPlayer = (jsonLectura != null ? JsonUtility.FromJson<DatosPlayer>(jsonLectura) : null);
    }
}
