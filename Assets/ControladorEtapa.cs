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

    // Start is called before the first frame update
    void Start()
    {
        tiempoActual = tiempoLimite;
        timerCorriendo = true;
        Timer();

        etapaTerminada = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(sectorMage.seCumplioObjetivo == true && tiempoActual > 0f)
        {
            // HAY VICTORIA
            timerCorriendo = false;
            etapaTerminada = true;
        }

        if(controladorPersonaje.vidaCubo <= 0f)
        {
            // HAY DERROTA
            timerCorriendo = false;
            Debug.Log("PERDISTE! TE MATARON");
            etapaTerminada = true;
        }

        if(tiempoActual <= 0f)
        {
            // HAY DERROTA
            timerCorriendo = false;
            Debug.Log("PERDISTE! SE TE ACABO EL TIEMPO");
            etapaTerminada = true;
        }

        if(etapaTerminada == true)
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
