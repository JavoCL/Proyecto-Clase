using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAnimacion : MonoBehaviour
{
    public Animator animatorPrueba;
    public float velocidadAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("VELOCIDAD ACTUAL: " + animatorPrueba.GetCurrentAnimatorStateInfo(0).speed);
    }
}
