using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEjemploAnimacion : MonoBehaviour
{
    public Animator animatorCubo;
    public bool isCrouched = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            animatorCubo.SetTrigger("jumpTrigger");

        if (Input.GetAxis("Vertical") != 0f)
            animatorCubo.SetBool("walkBool", true);
        else
            animatorCubo.SetBool("walkBool", false);

        if(Input.GetButtonDown("Crouch"))
        {
            if(isCrouched == false)
            {
                animatorCubo.SetTrigger("crouchTrigger");
            }
            else if (isCrouched == true)
            {
                animatorCubo.SetTrigger("standingTrigger");
            }
            isCrouched = !isCrouched;
        }
    }
}
