using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PICM_Coin : MonoBehaviour
{
    float startY;
    public Transform mostLeft;
    public Transform mostRight;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        ResetPos();
    }

    void ResetPos()
    {
        float newX = Random.Range(mostLeft.position.x, mostRight.position.x);
        transform.position = new Vector3(newX, startY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "limiter") ResetPos();
    }
}
