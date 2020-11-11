using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorMage : MonoBehaviour
{
    public GameObject puerta;
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

            if(other.gameObject.GetComponent<ScriptPrueba>().clase == ScriptPrueba.ClaseJugador.Mage)
            {
                Debug.Log("HEY! BIENVENIDO. PUEDES PASAR");
                puerta.SetActive(false);
            }
            else
            {
                Debug.Log("STOP! NO PUEDES PASAR, SECTOR EXCLUSIVO DE MAGOS");
                puerta.SetActive(true);
            }
        }
    }
}
