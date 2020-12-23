using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ControladorInterescenas : MonoBehaviour
{
    public string texto;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
        PlayerPrefs.SetString("nombrePlayer", texto);

        //Debug.Log(PlayerPrefs.HasKey("nombrePlayer") == true ? "EXISTE!" : "NO EXISTE");
        //Debug.Log("Nombre Player" + PlayerPrefs.GetString("nombrePlayer"));

        File.WriteAllText(Application.dataPath + "/ArchivoPrueba.json", "HOLO SOY UN ARCHIVO");

        Debug.Log("ESTO LEI DEL TEXTO \"" + File.ReadAllText(Application.dataPath + "/ArchivoPrueba.json") + "\"");
        Debug.Log("Data Path" + Application.dataPath);
        Debug.Log("Persistent Data Path" + Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPrefs.SetString("nombrePlayer", texto);
    }

    private void OnDestroy()
    {
        Debug.Log("GUARDANDO");
        PlayerPrefs.Save();
        Debug.Log("GUARDADO");
    }
}
