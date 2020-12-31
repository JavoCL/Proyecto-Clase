using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ControladorPersonajeEjemplo : MonoBehaviour
{
    [Header("Configuraciones Generales")]
    public string nombrePlayer;
    [Header("Variables Estado Personaje")]
    [Range(0f, 100f)]
    public float vidaCubo; // o HP
    public float velocidadCubo; // Velocidad actual del cubo
    public float velocidadMaxCubo; // Velocidad máxima del cubo
    public float fuerzaSaltoCubo;

    [Range(0, 32)]
    public int municionCubo;

    public bool fueDañado;

    public enum Poderes { None, Normal, FlorFuego, FlorHielo, Pluma };
    public enum EstadoCubo { Idle, Moviendo, Saltando };
    public enum ClaseJugador { Rogue, Hunter, Mage };

    public Poderes poderesCubo;
    public EstadoCubo estado;
    public ClaseJugador clase;

    public string nombreCubo;

    [Header("Animacion Personaje")]
    public Animator animatorPersonaje;

    [Header("Varios")]
    public GameObject objeto;
    public Transform camara;
    public bool unaVez;
    public ControladorCanvasPersonaje canvasPersonaje;
    public AudioSource sonidoPasosPersonaje;

    [Header("Control Etapa")]
    public int puntaje;
         
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("HOLA MUNDO START");
        //Debug.Log("Mi nombre es: " + nombreCubo + ", y mi velocidad es " + velocidadCubo);

        //transform.position = new Vector3(transform.position.x + 7f, transform.position.y, transform.position.z);
        //transform.Translate(7f, 0f, 0f);
        animatorPersonaje = this.transform.GetChild(0).GetComponent<Animator>();
        nombrePlayer = (FindObjectOfType<PruebaInterEscena>() != null ? FindObjectOfType<PruebaInterEscena>().nombrePlayer : "NO HAY NOMBRE");

        //Debug.Log("Data Path: " + Application.dataPath);
        //Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
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

        if (Input.GetButtonDown("Crouch"))
        {
            animatorPersonaje.SetBool("crouchBool", !animatorPersonaje.GetBool("crouchBool"));
        }

        // MOVIMIENTO PERSONAJE
        //if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
        if (Input.GetButton("Vertical"))
        {
            if(estado != EstadoCubo.Saltando)
                estado = EstadoCubo.Moviendo;

            transform.Translate(((new Vector3(Input.GetAxis("HorizontalView"), 0f, Input.GetAxis("Vertical"))) * velocidadCubo));
            animatorPersonaje.SetBool("walkBool", true);
            if (sonidoPasosPersonaje.isPlaying == false)
                sonidoPasosPersonaje.Play();
        }
        else
        {
            animatorPersonaje.SetBool("walkBool", false);
            //estado = EstadoCubo.Idle;
            sonidoPasosPersonaje.Stop();
        }


        if (Input.GetAxis("Jump") != 0f)
        {
            if (estado != EstadoCubo.Saltando)
            {
                estado = EstadoCubo.Saltando;
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, Input.GetAxis("Jump") * fuerzaSaltoCubo, 0f));
                animatorPersonaje.SetTrigger("jumpTrigger");
            }
        }

        //Debug.Log("AXIS VERTICAL: " + Input.GetAxis("Vertical").ToString());

        transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * 0.5f, 0f));

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

        canvasPersonaje.textoSalud.text = "HP: " + vidaCubo;
        canvasPersonaje.textoPuntaje.text = "SCORE: " + puntaje;
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
        if (collision.gameObject.tag == "Obstaculo")
        {
            Debug.Log("CHOQUE AL ENTRAR. OBJETO CHOCADO: " + collision.gameObject.name);

            vidaCubo -= 50f;
        }
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

    bool scoreUnaVez = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AreaLlegada")
        {
            Debug.Log("ENTRE A LA COLISION LOGICA");
            GuardarDatos();
        }
            
        if (other.gameObject.tag == "Suelo")
        {
            if (estado == EstadoCubo.Saltando)
            {
                Debug.Log("ATERRIZANDO!");
                animatorPersonaje.SetTrigger("landingTrigger");
                estado = EstadoCubo.Idle;
            }
        }

        if(other.gameObject.tag == "ScoreZone" && scoreUnaVez == false)
        {
            Debug.Log("ANOTAMOS PUNTAJE!");
            other.gameObject.GetComponent<Animator>().SetTrigger("scoringTrigger");
            puntaje += 100;
            scoreUnaVez = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AreaLlegada")
            Debug.Log("ME QUEDO EN COLISION LOGICA");
        if (other.gameObject.tag == "Suelo")
            Debug.Log("ESTOY TOCANDO EL SUELO");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AreaLlegada")
            Debug.Log("SALI A LA COLISION LOGICA");
    }

    public void GuardarDatos()
    {
        string json;

        json = JsonUtility.ToJson(this);

        Debug.Log("JSON: " + json);

        File.WriteAllText(Application.persistentDataPath + "/jsonControladorPersonaje.js", json);
    }

    public void GuardarDatosJSON()
    {
        string json = "";
        DatosPlayer datos = new DatosPlayer();

        datos.nombrePlayer = this.nombrePlayer;
        datos.vidaCubo = this.vidaCubo;
        datos.puntaje = this.puntaje;
        datos.municionCubo = this.municionCubo;
        datos.fueDañado = this.fueDañado;

        json = JsonUtility.ToJson(datos);

        Debug.Log("EL RESULTADO DEL JSON ES: " + json);

        File.WriteAllText(Application.persistentDataPath + "/jsonEjemplo.js", json);
    }
}

[System.Serializable]
public class DatosPlayer
{
    public string nombrePlayer;
    public float vidaCubo;
    public int municionCubo;
    public bool fueDañado;
    public int puntaje;
}
