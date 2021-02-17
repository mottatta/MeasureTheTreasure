using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerScript : MonoBehaviour
{
    private float speed = 4f;
    [SerializeField] GameObject claw;
    [SerializeField] ClawScript clawScript;
    [SerializeField] GameObject[] wheels;
    public GameObject instructions;

    public GameObject picm;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(picm != null)
        {
            if (picm.transform.position.x == 0) return;
        }
        if (instructions.activeInHierarchy) return;
        GameObject clawChildObject = clawScript.GetChildObject();
        //Debug.Log(clawChildObject);
        if (!claw.activeInHierarchy && clawChildObject == null)
        {
            int dir = 0;
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            if (mousePos.x + speed * Time.deltaTime < transform.position.x) dir = -1;
            else if (mousePos.x - speed * Time.deltaTime > transform.position.x) dir = 1;
            transform.position += (Vector3.right * Time.deltaTime * speed) * dir;
            if (dir != 0)
            {
                if(dir > 0)
                {
                    moveWheels("right");
                }
                else
                {
                    moveWheels("left");
                }
            }
        }
    }

    private void moveWheels(string dir)
    {
        foreach(GameObject wheel in wheels)
        {
            wheel.GetComponent<WheelScript>().OnParentMoving(dir);
        }
    }
}
