using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    bool isInitialisation = true;
    bool gotoNextScene;
    Animator animator;
    float nextSceneDelay = 0.6f;
    string anime;

    bool isMapLevel;
    bool gotoMapLevel;
    bool gotoMainMenu;

    void Start()
    {
        if (isInitialisation)
        {
            animator = GetComponent<Animator>();
            GetComponent<SpriteRenderer>().enabled = true;
            isInitialisation = false;
            gameObject.SetActive(true);
            anime = "out";
            animator.Play("TransitionOut");
        }
    }

    public void OnTransitionOutEnd()
    {
        if (anime == "out") gameObject.SetActive(false);
    }

    public void DisplayTransitionAndGotoNextScene(bool _isMapLevel = false, bool _gotoMapLevel = false, bool _gotoMainMenu = false)
    {
        gotoMapLevel = _gotoMapLevel;
        isMapLevel = _isMapLevel;
        gotoMainMenu = _gotoMainMenu;
        if (isMapLevel == false) SharedState.lastNonMapScene = SceneManager.GetActiveScene().buildIndex;
        anime = "in";
        gameObject.SetActive(true);
        gotoNextScene = true;
        if (animator == null) animator = GetComponent<Animator>();
        animator.Play("TransitionIn");
    }

    public void GotoNextSceneWithDelay()
    {
        StartCoroutine(DelayedNextScene());
    }

    private IEnumerator DelayedNextScene()
    {
        yield return new WaitForSeconds(nextSceneDelay);
        GotoNextScene();
    }

    public void GotoNextScene()
    {
        Map map = GameObject.FindObjectOfType<Map>();
        if(map != null) map.Hide();
        if (gotoNextScene == true)
        {
            if (gotoMainMenu)
            {
                SceneManager.LoadScene(1);
            }
            else if (gotoMapLevel)
            {
                SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);//last scene is map scene
            }
            else if(isMapLevel == true)
            {
                SceneManager.LoadScene(SharedState.lastNonMapScene + 1);
            }
            else if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            //if no next scenes go to mainMenu
            {
                //TODO move GameComplete() in LastScene Start()
                //SharedState.GameComplete();
                SceneManager.LoadScene(1);
            }
        }
    }
}
