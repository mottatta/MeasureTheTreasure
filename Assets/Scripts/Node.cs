using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject body;
    void Start()
    {
        body.SetActive(false);
    }

    public void Show()
    {
        body.SetActive(true);
    }
}
