using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerCombinerScript : MonoBehaviour
{
    [SerializeField] int nextLevelVal;
    [SerializeField] GameObject gameCompleteAnimation;
    [SerializeField] CombinerScript combinerScript;
    [SerializeField] GameObject coins;

    public void OnObjectsCombined()
    {
        if (AreAllObjectsOnNextLevelVal())
        {
            combinerScript.levelIsOver = true;
            Invoke("OnLevelComplete", 1.20f);
        }
    }

    public int GetNextLevelVal()
    {
        return nextLevelVal;
    }

    private void OnLevelComplete()
    {
        gameCompleteAnimation.SetActive(true);
        coins.SetActive(false);
    }

    private bool AreAllObjectsOnNextLevelVal()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("combineObject");
        foreach(GameObject obj in objects)
        {
            if (obj.GetComponent<CombineObjectScript>().GetVal() != nextLevelVal) return false;
        }
        return true;
    }
}
