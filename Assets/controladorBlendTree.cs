using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorBlendTree : MonoBehaviour
{
    public Animator animatorBlendTree;

    [Range(-1f, 1f)]
    public float valorHorizontal;
    [Range(-1f, 1f)]
    public float valorVertical;
    [Range(0f, 1f)]
    public float inputJump;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        valorHorizontal = Input.GetAxis("Horizontal");
        valorVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetAxis("Vertical");

        //animatorBlendTree.SetFloat("horizontalFloat", valorHorizontal);
        //animatorBlendTree.SetFloat("verticalFloat", valorVertical);

        animatorBlendTree.SetFloat("velocidadSalto", inputJump);
    }
}
