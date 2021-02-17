using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCameraScript : MonoBehaviour
{
    [SerializeField] GameObject dice1;
    [SerializeField] GameObject dice2;
    Vector3 offset;
    float startY;
    float minYDistance = 2f;
    bool follow;

    void Awake()
    {
        offset = transform.position - dice1.transform.position;
        startY = transform.position.y;
        //EnableDice1();
    }

    // Update is called once per frame
    void Update()
    {
        if (!follow && Mathf.Abs(Vector3.Distance(transform.position, dice1.transform.position)) < minYDistance) follow = true;
        if(follow) transform.position = dice1.transform.position + offset;
        if (transform.position.y < startY) follow = false;
    }

    void EnableDice1()
    {
        dice1.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        TurnOn();
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        dice1.SetActive(true);
        dice1.GetComponent<CubeScript>().ResetDice();
        dice2.GetComponent<CubeScript>().ResetDice();
        dice2.SetActive(false);
    }
}
