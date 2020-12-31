using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEtapa : MonoBehaviour
{
    [Header("Configuraciones Generales")]
    public SectorMage sectorMage;
    public ControladorPersonajeEjemplo controladorPersonaje;

    public float tiempoLimite = 60f;
    public float tiempoActual;
    public bool timerCorriendo = false;
    public bool etapaTerminada;
    public bool hayVictoria = false;

    ControladorEscenaMenu controladorEscenaMenu;
    public AudioSource musicaFondo;
    public AudioSource audioPasos;

    [Header("Configuraciones Victoria-Derrota")]
    public AudioSource musicaVictoria;
    public AudioSource musicaDerrota;

    // Start is called before the first frame update
    void Start()
    {
        controladorEscenaMenu = FindObjectOfType<ControladorEscenaMenu>();

        // SI EXISTE EL OBJETO QUE NOS TRAJIMOS DE LA ESCENA INICIAL
        if (controladorEscenaMenu)
        {
            if (controladorEscenaMenu.hayDatos)
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

        StartCoroutine(_FinalEtapa());
        StartCoroutine(_Loop());
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
            hayVictoria = true;
        }

        if (controladorPersonaje.vidaCubo <= 0f)
        {
            // HAY DERROTA
            timerCorriendo = false;
            Debug.Log("PERDISTE! TE MATARON");
            etapaTerminada = true;
            hayVictoria = false;
        }

        if (tiempoActual <= 0f)
        {
            // HAY DERROTA
            timerCorriendo = false;
            Debug.Log("PERDISTE! SE TE ACABO EL TIEMPO");
            etapaTerminada = true;
            hayVictoria = false;
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

    IEnumerator _FinalEtapa()
    {
        Debug.Log("ESPERANDO EL FINAL DE ETAPA");
        yield return new WaitUntil(() => etapaTerminada == true);

        Debug.Log("COMENZANDO EL FINAL DE ETAPA");

        controladorPersonaje.enabled = false;
        controladorPersonaje.animatorPersonaje.SetBool("walkBool", false);

        musicaFondo.Stop();
        audioPasos.Stop();

        controladorPersonaje.canvasPersonaje.uiGameplay.SetActive(false);
        controladorPersonaje.canvasPersonaje.uiFinal.SetActive(true);

        if (hayVictoria == true)
        {
            controladorPersonaje.canvasPersonaje.textoVictoria.SetActive(true);
            controladorPersonaje.canvasPersonaje.textoDerrota.SetActive(false);
            musicaDerrota.Stop();
            musicaVictoria.Play();

            //yield return new WaitForSeconds(musicaVictoria.clip.length);
            yield return new WaitWhile(() => musicaVictoria.isPlaying == true);
        }
        else
        {
            controladorPersonaje.canvasPersonaje.textoVictoria.SetActive(false);
            controladorPersonaje.canvasPersonaje.textoDerrota.SetActive(true);
            musicaDerrota.Play();
            musicaVictoria.Stop();

            //yield return new WaitForSeconds(musicaDerrota.clip.length);
            yield return new WaitWhile(() => musicaDerrota.isPlaying == true);
        }

        SceneManager.LoadScene(0);
    }

    public bool comienzaLoop = false;

    IEnumerator _Loop()
    {
        yield return new WaitUntil(() => comienzaLoop);

        do
        {
            Debug.Log("HOLA, SOY UNA CORRUTINA EN LOOP");

            yield return null;
        }
        while (etapaTerminada == false);

        Debug.Log("SALI DEL LOOP");
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

