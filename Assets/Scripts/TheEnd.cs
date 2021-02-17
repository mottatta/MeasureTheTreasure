using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SharedState.GameComplete();
    }
}
