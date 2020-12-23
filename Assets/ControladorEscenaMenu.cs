using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEscenaMenu : MonoBehaviour
{
    public DatosGuardados datosCargados;
    public bool hayDatos = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
