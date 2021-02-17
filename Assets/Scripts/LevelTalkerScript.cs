using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTalkerScript : MonoBehaviour
{
    [SerializeField] Text textField;
    [SerializeField] GameObject textBubble;
    public bool goOut;
    float secondsBetweenShowAndHide = 3f;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        DisableTalker();
        animator = GetComponent<Animator>();
        HideTextBubble();
    }

    public void PresentText(int val)
    {
        goOut = false;
        gameObject.SetActive(true);
        ComposeBubbleText(val);
        animator.Play("LevelTalkerMoveInAnimation");
    }

    private void ComposeBubbleText(int val)
    {
        string key = "treasure_" + val.ToString() + "_" + Random.Range(1, 4);
        textField.text = SharedState.LanguageDefs[key];
    }

    public void ShowTextBubbsle()
    {
        textBubble.SetActive(true);
        StartCoroutine(WaitAndHide());
    }

    private IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(secondsBetweenShowAndHide);
        goOut = true;
        HideTextBubble();
        animator.Play("LevelTalkerMoveOutAnimation");
    }

    public void HideTextBubble()
    {
        textBubble.SetActive(false);
    }

    public void DisableTalker()
    {
        gameObject.SetActive(false);
    }
}
