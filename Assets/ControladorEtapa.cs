using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEtapa : MonoBehaviour
{
    public SectorMage sectorMage;
    public ControladorPersonajeEjemplo controladorPersonaje;

    public float tiempoLimite = 60f;
    public float tiempoActual;
    public bool timerCorriendo = false;
    public bool etapaTerminada;

    ControladorEscenaMenu controladorEscenaMenu;
    // Start is called before the first frame update
    void Start()
    {
        controladorEscenaMenu = FindObjectOfType<ControladorEscenaMenu>();

        // SI EXISTE EL OBJETO QUE NOS TRAJIMOS DE LA ESCENA INICIAL
        if (controladorEscenaMenu)
        {
            if(controladorEscenaMenu.hayDatos)
            {
                Debug.Log("EXISTEN DATOS. SE CARGA ESCENA GUARDADA PREVIAMENTE");
                EscenaCargada();
            }
            else
            {
                Debug.Log("NO EXISTEN DATOS. SE CREA ESCENA NUEVA");
                EscenaNueva();
            }
        }
        // SI NO EXISTE, CARGA UNA ESCENA COMPLETAMENTE NUEVA
        else
        {
            Debug.Log("NO EXISTE OBJETO. SE CREA ESCENA NUEVA");
            EscenaNueva();
        }
    }

    public void EscenaNueva()
    {
        tiempoActual = tiempoLimite;
        timerCorriendo = true;
        Timer();

        etapaTerminada = false;
    }

    public void EscenaCargada()
    {
        controladorPersonaje.nombrePlayer = controladorEscenaMenu.datosCargados.nombrePlayer;
        controladorPersonaje.vidaCubo = controladorEscenaMenu.datosCargados.vidaCubo;
        controladorPersonaje.puntaje = controladorEscenaMenu.datosCargados.puntaje;
        controladorPersonaje.municionCubo = controladorEscenaMenu.datosCargados.municionCubo;
        controladorPersonaje.fueDañado = controladorEscenaMenu.datosCargados.fueDañado;
        controladorPersonaje.transform.position = controladorEscenaMenu.datosCargados.posicion;
        controladorPersonaje.transform.eulerAngles = controladorEscenaMenu.datosCargados.rotacion;


        tiempoActual = controladorEscenaMenu.datosCargados.tiempoActual;
        timerCorriendo = true;
        Timer();

        etapaTerminada = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sectorMage.seCumplioObjetivo == true && tiempoActual > 0f)
        {
            // HAY VICTORIA
            timerCorriendo = false;
            etapaTerminada = true;
        }

        if (controladorPersonaje.vidaCubo <= 0f)
        {
            // HAY DERROTA
            timerCorriendo = false;
            Debug.Log("PERDISTE! TE MATARON");
            etapaTerminada = true;
        }

        if (tiempoActual <= 0f)
        {
            // HAY DERROTA
            timerCorriendo = false;
            Debug.Log("PERDISTE! SE TE ACABO EL TIEMPO");
            etapaTerminada = true;
        }

        if (etapaTerminada == true)
        {
            controladorPersonaje.enabled = false;
            controladorPersonaje.animatorPersonaje.SetBool("walkBool", false);
        }
    }

    void Timer()
    {
        StartCoroutine(_Timer());
    }

    IEnumerator _Timer()
    {
        do
        {
            yield return new WaitForSeconds(1f);
            tiempoActual--;
        }
        while (tiempoActual > 0f && timerCorriendo == true);
    }
}

[System.Serializable]
public class DatosGuardados
{
    public string nombrePlayer;
    public float vidaCubo;
    public int municionCubo;
    public bool fueDañado;
    public int puntaje;
    public float tiempoActual;
    public Vector3 posicion;
    public Vector3 rotacion;
}
