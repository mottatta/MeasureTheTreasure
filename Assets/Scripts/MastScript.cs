using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastScript : MonoBehaviour
{
    public int val = 0;
    public GameObject[] pieces;

    // Start is called before the first frame update
    void Start()
    {
        SetHeight(val);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeight(int _height)
    {
        val = 0;
        for (int i = 0; i < pieces.Length; i++)
        {
            if (i < _height)
            {
                pieces[i].SetActive(true);
                val = i + 1;
            }
            else pieces[i].SetActive(false);
        }
        //Debug.Log(val);
    }

    public void ShowParticles()
    {
        foreach(GameObject obj in pieces)
        {
            if (obj.activeSelf)
            {
                obj.GetComponent<MastPieceScript>().PlayParticles();
            }
        }
    }
    public void ChangeColor(Color _color)
    {
        foreach(GameObject piece in pieces)
        {
            piece.GetComponentInChildren<SpriteRenderer>().color = _color;
        }
    }
}
