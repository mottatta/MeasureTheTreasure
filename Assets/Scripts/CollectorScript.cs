using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour
{
    private float movingForce = 0.10f;
    private float jumpForce = 10f;
    Rigidbody rb;
    BoxCollider capsuleCollider;
    BoxCollider floorCollider;
    bool grounded;
    [SerializeField] GameObject floor;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<BoxCollider>();
        floorCollider = floor.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h != 0)
        {
            if(h > 0)
            {
                transform.position += new Vector3(movingForce, 0, 0);
            }
            else
            {
                transform.position -= new Vector3(movingForce, 0, 0);
            }
        }
        if(Input.GetButtonDown("Fire1") && grounded)
        {
            grounded = false;
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == floor)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == floor)
        {
            grounded = false;
        }
    }
}
