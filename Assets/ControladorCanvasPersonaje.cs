using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCanvasPersonaje : MonoBehaviour
{
    public ControladorEtapa controlador;
    public Text textoPuntaje;
    public Text textoSalud;
    public Text textoTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textoTime.text = "TIME: " + controlador.tiempoActual;
    }
}
