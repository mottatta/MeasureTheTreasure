using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

    private static Map _instance;
    public Pirate pirate;
    public Text debugTxt;
    public TransitionScript transition;
    public GameObject transitionPrefab;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            _instance.Show();
            return;
        }
        _instance = this;
        Show();
        DontDestroyOnLoad(this);
    }

    public void Show()
    {
        SoundManager.GetInstance().PlayMusic(SoundManager.GetInstance().mapMusic);
        GameObject transition = Instantiate(transitionPrefab);
        this.transform.position = new Vector3(0, 0, 0);
        pirate.SetNewLevelTargetNode();
    }

    public void Hide()
    {
        this.transform.position = new Vector3(100, 0, 0);
        transition = GameObject.FindObjectOfType<TransitionScript>();
        if(transition == null)
        {
            GameObject transition = Instantiate(transitionPrefab);
            transition.GetComponent<TransitionScript>().DisplayTransitionAndGotoNextScene(true, false);
        }
    }

    void Update()
    {
        
    }
}
