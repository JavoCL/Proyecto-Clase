using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorEjemploCanvas : MonoBehaviour
{
    public Text textoEjemplo;
    public string textoNuevo = "";
    public InputField inputEjemplo;
    public Dropdown dropdownEjemplo;
    public Slider sliderEjemplo;
    
    // Start is called before the first frame update
    void Start()
    {
        textoNuevo = textoEjemplo.text;
    }

    // Update is called once per frame
    void Update()
    {
        //textoEjemplo.text = textoNuevo;
        Debug.Log("TEXTO DE EJEMPLO DICE: " + textoEjemplo.text);
        Debug.Log("ELEMENTO SELECCIONADO DE DROPDOWN: " + dropdownEjemplo.options[dropdownEjemplo.value].text);

        Debug.Log("VALOR SLIDER: " + sliderEjemplo.value);
    }

    public void BotonEjemploOnClic()
    {
        //textoEjemplo.text = textoNuevo;
        textoEjemplo.text = inputEjemplo.text;
    }

    public void CheckeaCambio()
    {
        Debug.Log("CAMBIO EL VALOR");
    }
}
