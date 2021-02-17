using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableButtonScript : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void OnMouseDown()
    {
        animator.Play("ButtonClicked");
    }
}
