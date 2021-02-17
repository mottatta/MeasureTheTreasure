using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] TransitionScript transitionScript;
    public bool isMapButton = false;
    Animator animator;
    public GameObject body;
    public AudioClip sfxClick;

    void Start()
    {
        if (isMapButton)
        {
            animator = GetComponent<Animator>();
            animator.Play("PlayButton_Invisible");
        }
        //else GameObject.FindObjectOfType<Map>().Show();
    }

    void OnMouseDown()
    {
        SoundManager.GetInstance().PlaySFX(sfxClick);
        if (isMapButton)
        {
            if (body.activeInHierarchy)
            {
                if (animator == null) animator = GetComponent<Animator>();
                animator.Play("PlayButton_Invisible");
                if (transitionScript == null) transitionScript = GameObject.FindObjectOfType<TransitionScript>();
                transitionScript.gameObject.SetActive(true);
                transitionScript.DisplayTransitionAndGotoNextScene(true, false);
            }
        }
        else
        {
            transitionScript.DisplayTransitionAndGotoNextScene();
        }
    }
}
