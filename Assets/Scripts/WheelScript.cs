using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    private float rotatingSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnParentMoving(string dir)
    {
        Vector3 newRotation;
        if(dir == "right")
        {
            transform.Rotate(new Vector3(0, 0, 1) * -rotatingSpeed * Time.deltaTime);
            //newRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z * rotatingSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 1) * rotatingSpeed * Time.deltaTime);
            //newRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z * -rotatingSpeed * Time.deltaTime);
        }
    }
}
