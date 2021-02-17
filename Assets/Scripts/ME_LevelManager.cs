using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ME_LevelManager : MonoBehaviour
{
    public int equationLevelNumber = 1;
    public StaticBubbleSpot[] staticBubbles;
    public DraggableBubble[] draggableBubbles;
    public GameObject[] gameplayObjects;
    public ME_DialogScript levelTalker;
    public Text equationText;
    public SolutionBubble[] solutionBubbles;
    public Animator rays;
    public GameObject[] sails;

    public GameObject[] draggableBubblesPos;
    public GameObject[] solutionsPos;
    public GameObject sparksPrefab;
    public GameObject targetPos;

    public AudioClip sfxWrong;
    public AudioClip sfxSpark;
    public AudioClip sfxSuccess;
    public AudioClip sfxPlaceBubble;
    public AudioClip sfxSelect;
    public AudioClip sfxShowSolutions;
    public SoundManager soundManager;
    public BuoyancyEffector2D buoyancy;

    private bool equationIsReady = false;

    void Start()
    {
        SharedState.SubmitProgress();
        SoundManager.GetInstance().PlayMusic(SoundManager.GetInstance().levelMusic);
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        rays.Play("RaysInvisible");
        DisableGameplayObjects();
        equationText.text = SharedState.GetJsonText("equation_level_" + equationLevelNumber.ToString() + "_equation_text");
        RandomizeDraggableBubbles();
    }

    public void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        System.Array.Resize(ref arr, arr.Length - 1);
    }

    void RandomizeDraggableBubbles()
    {
        for (int i = 0; i < draggableBubbles.Length; i++)
        {
            int randomIndex = Random.Range(0, draggableBubblesPos.Length - 1);
            GameObject pos = draggableBubblesPos[randomIndex];
            draggableBubbles[i].transform.position = pos.transform.position;
            draggableBubbles[i].originalPosition = draggableBubbles[i].transform.position;
            RemoveAt(ref draggableBubblesPos, randomIndex);
        }

    }

    DraggableBubble GetUnknownBubble()
    {
        DraggableBubble[] bubbles = GameObject.FindObjectsOfType<DraggableBubble>();
        foreach(DraggableBubble bubble in bubbles)
        {
            if (bubble.val == "?") return bubble;
        }
        return null;
    }

    void RandomizeSolutions()
    {
        DraggableBubble unknown = GetUnknownBubble();
        targetPos.transform.position = unknown.transform.position;
        for (int i = 0; i < solutionBubbles.Length; i++)
        {
            Debug.Log("randomize " + i + " " + solutionsPos.Length);
            int randomIndex = Random.Range(0, solutionsPos.Length);
            GameObject pos = solutionsPos[randomIndex];
            solutionBubbles[i].transform.position = unknown.transform.position;
            solutionBubbles[i].targetPos = pos.transform.position;
            solutionBubbles[i].Show();
            RemoveAt(ref solutionsPos, randomIndex);
        }
    }

    void Update()
    {
        if (!equationIsReady)
        {
            if (CheckIfEquationIsReady())
            {
                OnEquationReady();
            }
        }
    }

    void DisableGameplayObjects()
    {
        equationText.enabled = false;
        foreach (GameObject obj in gameplayObjects)
        {
            obj.SetActive(false);
        }
    }

    public void EnableGameplayObjects()
    {
        equationText.enabled = true;
        foreach (GameObject obj in gameplayObjects)
        {
            obj.SetActive(true);
        }
    }

    public void LevelComplete(int solution)
    {
        foreach (DraggableBubble bubble in draggableBubbles)
        {
            if (bubble.val == "?") bubble.SetVal(solution.ToString());
        }
        Invoke("MakeWaterFlow", 0.50f);
        ShowSails();
        rays.Play("RaysRotateConstantly");
        HideEquationSolutions();
        levelTalker.GoToNextSpeach();
        PlaySFX(sfxSuccess);
    }

    void ShowSails()
    {
        foreach (GameObject sail in sails)
        {
            sail.SetActive(true);
        }
    }

    void HideEquationSolutions()
    {
        foreach (SolutionBubble bubble in solutionBubbles)
        {
            bubble.gameObject.SetActive(false);
        }
    }

    public void ShowEquationSolutions()
    {
        PlaySFX(sfxShowSolutions);
        RandomizeSolutions();
    }

    void OnEquationReady()
    {
        foreach (DraggableBubble bubble in draggableBubbles)
        {
            bubble.isEquationReady = true;
            if (bubble.val == "?") bubble.pointerReminder.SetActive(true);
        }
        equationIsReady = true;
        DisableBubbles();
        levelTalker.GoToNextSpeach();
        CreateSparks();
    }

    void CreateSparks()
    {
        PlaySFX(sfxSpark);
        foreach (DraggableBubble bubble in draggableBubbles)
        {
            GameObject sparks = Instantiate(sparksPrefab);
            sparksPrefab.transform.position = bubble.transform.position;
        }
    }

    void DisableBubbles()
    {
        foreach (DraggableBubble bubble in draggableBubbles)
        {
            bubble.isDraggable = false;
        }
    }

    bool CheckIfEquationIsReady()
    {
        foreach (DraggableBubble bubble in draggableBubbles)
        {
            if (bubble.colliderObject)
            {
                StaticBubbleSpot spot = bubble.colliderObject.GetComponent<StaticBubbleSpot>();
                if ((spot.index == bubble.index || spot.index == bubble.alternativeIndex) && !bubble.isDragged && !spot.isFree)
                {

                }
                else return false;
            }
            else return false;
        }
        return true;
    }

    private void MakeWaterFlow()
    {
        buoyancy.flowVariation = 0.70f;
    }

    public void PlaySFX(AudioClip clip)
    {
        soundManager.PlaySFX(clip);
    }
}
