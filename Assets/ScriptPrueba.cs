using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPrueba : MonoBehaviour
{
    [Range(0f, 100f)]
    public float vidaCubo; // o HP
    public float velocidadCubo; // Velocidad actual del cubo
    public float velocidadMaxCubo; // Velocidad máxima del cubo

    [Range(0, 32)]
    public int municionCubo;

    public bool fueDañado;

    public enum Poderes { None, Normal, FlorFuego, FlorHielo, Pluma };
    public enum EstadoCubo { Idle, Moviendo, Saltando };

    public Poderes poderesCubo;
    public EstadoCubo estado;

    public string nombreCubo;
    public GameObject objeto;

    public Transform camara;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("HOLA MUNDO START");
        //Debug.Log("Mi nombre es: " + nombreCubo + ", y mi velocidad es " + velocidadCubo);

        //transform.position = new Vector3(transform.position.x + 7f, transform.position.y, transform.position.z);
        //transform.Translate(7f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("HOLA MUNDO UPDATE");

        // {-1, ..., 0, ... 1}
        //Debug.Log("EJE HORIZONTAL: " + Input.GetAxis("Horizontal"));
        //Debug.Log("EJE VERTICAL: " + Input.GetAxis("Vertical"));

        //Debug.Log("EJE HORIZONTAL VIEW: " + Input.GetAxis("HorizontalView"));
        //Debug.Log("EJE VERTICAL VIEW: " + Input.GetAxis("VerticalView"));

        //Debug.Log("BOTON VERTICAL:" + Input.GetButton("Vertical"));
        estado = EstadoCubo.Idle;


        // MOVIMIENTO PERSONAJE
        //if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Jump") != 0f)
            {
                estado = EstadoCubo.Saltando;
            }
            else // Adelante/Atras/Izquierda/Derecha
            {
                estado = EstadoCubo.Moviendo;
            }
            transform.Translate(((new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical"))) * velocidadCubo));
        }

        transform.Rotate(new Vector3(0f, Input.GetAxis("HorizontalView"), 0f));

        // MOVIMIENTO CAMARA
        //camara.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f));

        //Debug.Log("Vector FORWARD: " + transform.forward);
        //Debug.Log("Magnitud FORWARD: " + transform.forward.magnitude);

        //if(fueDañado == true)
        //{
        //    Debug.Log("EL CUBO SUFRIO DAÑO!");
        //}
        //else
        //{
        //    Debug.Log("EL CUBO ESTA BIEN");
        //}


    }

    public void ImprimeTransform()
    {
        // POSICION DE OBJETO
        Debug.Log("Mi posicion global es: " + this.gameObject.transform.position);
        // POSICION DE OBJETO
        Debug.Log("Mi posicion local es: " + this.gameObject.transform.localPosition);

        // ROTACION GLOBAL DE OBJETO
        Debug.Log("Mi rotacion global es: " + this.gameObject.transform.eulerAngles);
        // ROTACION LOCAL DE OBJETO
        Debug.Log("Mi rotacion local es: " + this.gameObject.transform.localEulerAngles);

        // ESCALA DE OBJETO
        Debug.Log("Mi escala es: " + this.gameObject.transform.localScale);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // AL TOCAR LA COLISION POR PRIMERA VEZ
        // UNICA VEZ
        if(collision.gameObject.tag == "Obstaculo")
            Debug.Log("CHOQUE AL ENTRAR. OBJETO CHOCADO: " + collision.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        // COLISION CONTINUA, MIENTRAS SE ESTE CHOCANDO
        // MUCHAS VECES MIENTRAS SE ESTE CHOCANDO
        if (collision.gameObject.tag == "Obstaculo")
            Debug.Log("ME MANTENGO CHOCANDO");
    }

    private void OnCollisionExit(Collision collision)
    {
        // AL MOMENTO DE SALIR DE LA COLISION
        // UNICA VEZ
        if (collision.gameObject.tag == "Obstaculo")
            Debug.Log("SALI DEL CHOQUE");
    }
}
