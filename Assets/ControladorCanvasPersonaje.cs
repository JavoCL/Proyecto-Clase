using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorCanvasPersonaje : MonoBehaviour
{
    public ControladorEtapa controlador;
    public ControladorPersonajeEjemplo controladorPersonaje;
    public Text textoPuntaje;
    public Text textoSalud;
    public Text textoTime;
    public Text textoPlayer;

    public GameObject panelMenu;
    public GameObject panelGuardar;

    // Start is called before the first frame update
    void Start()
    {
        //textoPlayer.text = "Nombre: " + PlayerPrefs.GetString("nombre");
        Debug.Log(PlayerPrefs.HasKey("RUT") == true ? "EXISTE!" : "NO EXISTE");
        Debug.Log("Nombre Player: " + PlayerPrefs.GetString("nombrePlayer"));
    }

    // Update is called once per frame
    void Update()
    {
        textoTime.text = "TIME: " + controlador.tiempoActual;
        textoPuntaje.text = "SCORE: " + controladorPersonaje.puntaje;
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            panelMenu.SetActive(!panelMenu.activeSelf);
            panelGuardar.SetActive(false);
        }
    }

    public void GuardarPartida()
    {
        panelGuardar.SetActive(true);
    }

    public void ButtonSi()
    {
        string json = "";
        DatosGuardados datos = new DatosGuardados();

        datos.nombrePlayer = controladorPersonaje.nombrePlayer;
        datos.vidaCubo = controladorPersonaje.vidaCubo;
        datos.puntaje = controladorPersonaje.puntaje;
        datos.municionCubo = controladorPersonaje.municionCubo;
        datos.fueDañado = controladorPersonaje.fueDañado;
        datos.tiempoActual = controlador.tiempoActual;

        datos.posicion = controladorPersonaje.transform.position;
        datos.rotacion = controladorPersonaje.transform.eulerAngles;

        json = JsonUtility.ToJson(datos);

        Debug.Log("JSON PARTIDA GUARDADA: " + json);
        File.WriteAllText(Application.persistentDataPath + "/datosPartidaGuardada.js", json);

        SceneManager.LoadScene(0);
    }

    public void ButtonNo()
    {
        panelGuardar.SetActive(false);
        panelMenu.SetActive(false);
    }

}
