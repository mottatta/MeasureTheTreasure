using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public bool isFiring = false;
    Animator playerAnimator;
    [SerializeField] ClawScript clawScript;
    [SerializeField] GameObject clawObject;
    [SerializeField] GameObject clawSkin;
    public GameObject instructions;

    public GameObject picm;
    // Start is called before the first frame update
    void Awake()
    {
        //this.transform.parent.gameObject.SetActive(false);
        playerAnimator = transform.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(picm != null)
        {
            if (Input.GetMouseButtonDown(0) && !isFiring && picm.transform.position.x != 0 && !instructions.activeInHierarchy)
            {
                Launch();
            }
        }
        else if(Input.GetMouseButtonDown(0) && !isFiring && !instructions.activeInHierarchy)
        {
            Launch();
        }
    }

    void Launch()
    {
        isFiring = true;
        playerAnimator.speed = 0;

        RaycastHit hit;
        Vector3 down = gameObject.transform.TransformDirection(Vector3.down);
        
        if(Physics.Raycast(transform.position, down, out hit, Mathf.Infinity))
        {
            clawSkin.SetActive(false);
            clawObject.SetActive(true);
            clawScript.SetTarget(hit.point);
        }
    }

    public void CollectObject()
    {
        isFiring = false;
        playerAnimator.speed = 1;
        clawSkin.SetActive(true);
    }
}
