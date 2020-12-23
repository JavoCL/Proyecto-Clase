using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorCanvasMenu : MonoBehaviour
{
    public InputField inputNombrePlayer;
    public ControladorEscenaMenu controladorEscenaMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SiguienteNivel()
    {
        //Debug.Log(FindObjectOfType<PruebaInterEscena>().nombrePlayer);
        SceneManager.LoadScene(1);
    }

    public void CargaNivel()
    {
        DatosGuardados datosCargados;
        string jsonCargado;

        jsonCargado = File.ReadAllText(Application.persistentDataPath + "/datosPartidaGuardada.js");

        datosCargados = JsonUtility.FromJson<DatosGuardados>(jsonCargado);

        controladorEscenaMenu.datosCargados = datosCargados;
        controladorEscenaMenu.hayDatos = true;

        SiguienteNivel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
