using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RotatingCoinScript : MonoBehaviour
{
    LevelManagerScript levelManager;
    UWT_LevelManager uwt_levelManager;
    [SerializeField] int val = 0;
    [SerializeField] int chestsCount = 2;//how many chests should be used in the put in chest menu
    [SerializeField] GameObject particles;
    public Text txt;

    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManagerScript>();
        uwt_levelManager = GameObject.FindObjectOfType<UWT_LevelManager>();
        if(levelManager != null) levelManager.levelCoins++;
        txt.text = val.ToString();
    }

    public void OnCollected()
    {
        GameObject p = Instantiate(particles);
        p.transform.position = transform.position;
        if (levelManager != null) levelManager.OnCoinCollected(val, chestsCount);
        else if (uwt_levelManager != null) uwt_levelManager.OnCoinCollected(val);
    }

    public int GetVal()
    {
        return val;
    }
}
