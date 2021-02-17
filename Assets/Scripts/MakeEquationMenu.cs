using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeEquationMenu : MonoBehaviour
{
    public Transform[] coinPositions;
    public EmptyPlace empty1, empty2;
    public GameObject coinPrefab;
    public GameObject rays;
    public Animator raysAnimator;
    public Text txt;
    public Animator animator;
    public LevelManagerDivide levelManager;
    public AudioClip tadaa;
    int rightAnswers;
    int currentTask = 0;
    List<Vector2> tasks = null;

    void Start()
    {
        
    }

    void SetTasks()
    {
        tasks = null;
        tasks = new List<Vector2>();
        tasks.Add(new Vector2(24, 6));
        tasks.Add(new Vector2(45, 5));
        tasks.Add(new Vector2(64, 4));
    }

    public void CreateTask()
    {
        rays.SetActive(false);
        if (tasks == null) SetTasks();
        SetVals((int) tasks[currentTask].x, (int) tasks[currentTask].y);
        txt.text = SharedState.LanguageDefs["make_equation_" + (currentTask + 1).ToString()];
        currentTask++;
    }

    public void SetVals(int val1, int val2)
    {
        rightAnswers = 0;
        empty1.SetVal(val1);
        empty2.SetVal(val2);
        if (Random.Range(0f, 1f) < 0.50)
        {
            CreateCoin(val1, coinPositions[1]);
            CreateCoin(val2, coinPositions[0]);
        }
        else
        {
            CreateCoin(val1, coinPositions[0]);
            CreateCoin(val2, coinPositions[1]);
        }
    }

    public void OnSuccess()
    {
        rightAnswers++;
        if(rightAnswers == 2)
        {
            if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(tadaa);
            rays.SetActive(true);
            raysAnimator.Play("RaysRotateConstantly");
            Invoke("DisableMenu", 2f);
        }
    }

    public void EnableMenu()
    {
        if(!gameObject.activeInHierarchy) gameObject.SetActive(true);
        CreateTask();
        rays.SetActive(false);
        animator.Play("EquationMenuShow");
    }

    public void DisableMenu()
    {
        levelManager.ActivateCurrentCoin();
        gameObject.SetActive(false);
    }

    void CreateCoin(int val, Transform pos)
    {
        GameObject coin = Instantiate(coinPrefab);
        coin.transform.position = pos.position;
        coin.GetComponent<PICM_DraggableCoin>().SetVal(val);
    }

    void Update()
    {
        
    }
}
