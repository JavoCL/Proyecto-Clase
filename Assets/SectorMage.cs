using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorMage : MonoBehaviour
{
    [Header("NNN")]
    public GameObject puerta;
    public bool seCumplioObjetivo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("ES PLAYER");

            if(other.gameObject.GetComponent<ControladorPersonajeEjemplo>().clase == ControladorPersonajeEjemplo.ClaseJugador.Mage)
            {
                if(other.gameObject.GetComponent<ControladorPersonajeEjemplo>().puntaje > 0f)
                {
                    Debug.Log("HEY! BIENVENIDO. PUEDES PASAR");
                    puerta.SetActive(false);
                }
                else
                {
                    Debug.Log("NO PUEDES PASAR, NO CONSEGUISTE EL OBJETIVO");
                    puerta.SetActive(true);
                }
            }
            else
            {
                Debug.Log("STOP! NO PUEDES PASAR, SECTOR EXCLUSIVO DE MAGOS");
                puerta.SetActive(true);
            }
        }
    }
}
