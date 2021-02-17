using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;

    [SerializeField] float speed = 2f;

    [SerializeField] float minFlyInterval = 0f;
    [SerializeField] float maxFlyInterval = 1f;

    bool isFlying = false;
    GameObject target;

    void Start()
    {
        target = point2;
        SetFlyDelay();
    }

    void SetFlyDelay()
    {
        Invoke("SetFly", Random.Range(minFlyInterval, maxFlyInterval));
    }

    void SetFly()
    {
        isFlying = true;
        Vector3 newPos = new Vector3(transform.position.x, target.transform.position.y - Random.Range(0f, 3.50f));
        transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if(Mathf.Abs(transform.position.x - target.transform.position.x) < speed * Time.deltaTime)
            {
                SwitchTarget();
                isFlying = false;
                SetFlyDelay();
            }
        }
    }

    void SwitchTarget()
    {
        float scaleFactor = Random.Range(0.50f, 1);
        if (target == point1)
        {
            target = point2;
            transform.localScale = new Vector3(1 * scaleFactor, scaleFactor);
        }
        else
        {
            target = point1;
            transform.localScale = new Vector3(-1 * scaleFactor, scaleFactor);
        }
    }
}
