using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemploCorrutinas : MonoBehaviour
{
    public int segundos;
    public int minutos;
    public int horas;

    public bool contarTiempo = true;

    [Range(0f, 10f)]
    public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
        //CorrutinaSegundos(contarTiempo);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("CONTADOR TIEMPO: " + Time.deltaTime);

        velocidad += Input.GetAxis("Jump")*Time.deltaTime;
    }

    public void CorrutinaSegundos(bool condicion)
    {
        StartCoroutine(_CorrutinaSegundos(condicion));
    }

    IEnumerator _CorrutinaSegundos(bool condicion)
    {
        do
        {
            yield return new WaitForSeconds(1f);
            if(segundos < 59)
                segundos++;
            else
            {
                if (minutos < 59)
                {
                    segundos = 0;
                    minutos++;
                }
                else
                {
                    segundos = 0;
                    minutos = 0;
                    horas++;
                }
            }
        }
        while (condicion);
    }
}
