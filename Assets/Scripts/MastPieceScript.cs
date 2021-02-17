using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastPieceScript : MonoBehaviour
{

    public GameObject particles;

    public void PlayParticles()
    {
        GameObject p = Instantiate(particles);
        p.transform.position = transform.position;
    }
}
