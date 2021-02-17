using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskMenuScript : MonoBehaviour
{
    [SerializeField] GameObject digit_2;
    [SerializeField] GameObject digit_4;
    [SerializeField] GameObject digit_8;
    [SerializeField] Text secondMultiplierText;
    [SerializeField] Text resultText;
    [SerializeField] Text labelText;
    Animator animator;

    public AudioClip clipSelect;
    public AudioClip clipSuccess;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void CreateEquation(int firstMultiplier, int secondMultiplier)
   {
        DisableAllFirstMultipliers();
        secondMultiplierText.text = secondMultiplier.ToString();
        switch (firstMultiplier)
        {
            case 2:
                digit_2.SetActive(true);
                break;
            case 4:
                digit_4.SetActive(true);
                break;
            case 8:
                digit_8.SetActive(true);
                break;
        }
   }

    public void DisableAllFirstMultipliers()
    {
        digit_2.SetActive(false);
        digit_4.SetActive(false);
        digit_8.SetActive(false);
    }

    public void ShowResult()
    {
        animator.Play("TaskMenu_ShowResult");
    }

    public void SetResult(int val)
    {
        resultText.text = val.ToString();
    }

    public void EnableMenu()
    {
        labelText.text = SharedState.LanguageDefs["taskMenuLabel"];
        gameObject.SetActive(true);
        animator.Play("TaskMenu_Stand");
    }

    public void DisableMenu()
    {
        gameObject.SetActive(false);
    }

    public void PlaySFXSelect()
    {
        SoundManager.GetInstance().PlaySFX(clipSelect);
    }

    public void PlaySFXSuccess()
    {
        SoundManager.GetInstance().PlaySFX(clipSuccess);
    }
}
