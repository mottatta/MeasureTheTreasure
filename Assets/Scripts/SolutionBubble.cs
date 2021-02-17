using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolutionBubble : MonoBehaviour
{

    public int val;
    public Text valTxt;
    public Vector3 targetPos;
    public float moveSpeed;
    public bool isCorrectSolution = false;
    public Animator animator;
    bool isInPlace = false;
    public ME_LevelManager levelManager;
    bool isShown = false;
    public SpriteRenderer renderer;
    public Canvas canvas;

    void Start()
    {
        valTxt.text = val.ToString();
        Hide();
    }

    void Hide()
    {
        renderer.enabled = false;
        canvas.enabled = false;
    }

    public void Show()
    {
        isShown = true;
        renderer.enabled = true;
        canvas.enabled = true;
    }

    void Update()
    {
        if(!isShown) return;
        if (!isInPlace)
        {
            if (!V3Equal(gameObject.transform.position, targetPos))
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, moveSpeed * Time.deltaTime);
            }
            else
            {
                isInPlace = true;
                gameObject.transform.position = targetPos;
            }
        }
    }

    public bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.10;
    }

    private void OnMouseDown()
    {
        if (!isInPlace) return;
        if (isCorrectSolution)
        {
            levelManager.LevelComplete(val);
        }
        else
        {
            animator.Play("SolutionBubbleIncorrect");
            levelManager.PlaySFX(levelManager.sfxWrong);
        }
    }
}
