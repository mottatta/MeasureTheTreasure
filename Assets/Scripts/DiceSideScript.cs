using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideScript : MonoBehaviour
{
    CubeScript cubeScript;
    [SerializeField] int oppositeValue;
    private void Awake()
    {
        cubeScript = transform.parent.GetComponent<CubeScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        cubeScript.upSide = oppositeValue;
    }
}
