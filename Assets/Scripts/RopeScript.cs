using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public GameObject[] points;
    LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.sortingLayerName = "Foreground";
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, points[0].transform.position);
        line.SetPosition(1, points[1].transform.position);
    }
}
