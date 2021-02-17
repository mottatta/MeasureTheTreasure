using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAShareLevelScript : MonoBehaviour
{
    [SerializeField] int totalTasks = 4;
    int nextLevelVal;
    public int numberOfGroupsToCompleteTask;
    [SerializeField] CombinerScript combinerScript;
    [SerializeField] DiceCameraScript diceCameraScript;
    [SerializeField] ExplainerScript explainer;
    GameObject jewels;
    [SerializeField] GameObject jewelsPrefab;
    [SerializeField] TaskMenuScript taskMenuScript;
    bool explainerShown = false;

    private void CreateDiceTask()
    {
        diceCameraScript.TurnOn();
    }

    public void CreateJewels()
    {
        jewels = Instantiate(jewelsPrefab);
        jewels.GetComponent<JewelsScript>().EnableChildsByCount(nextLevelVal * numberOfGroupsToCompleteTask);
        taskMenuScript.EnableMenu();
        taskMenuScript.CreateEquation(nextLevelVal, numberOfGroupsToCompleteTask);
    }

    public void DestroyJewels()
    {
        Destroy(jewels);
        jewels = null;
    }

    public void OnObjectsCombined()
    {
        if (AreRequiredGroupsMade())
        {
            //player completed the task, show the successful equation
            combinerScript.levelIsOver = true;
            taskMenuScript.SetResult(nextLevelVal * numberOfGroupsToCompleteTask);
            taskMenuScript.ShowResult();
        }
    }

    public void ContinueDialog()
    {
        DestroyJewels();
        taskMenuScript.DisableMenu();
        explainer.ContinueDialog();
    }

    public void OnSecondDiceStop()
    {
        diceCameraScript.TurnOff();
        combinerScript.levelIsOver = false;
        if (explainerShown) CreateJewels();
        else
        {
            explainerShown = true;
            explainer.ContinueDialog();
        }
    }

    public int GetNextLevelVal()
    {
        return nextLevelVal;
    }
    public void SetNextLevelVal(int val)
    {
        nextLevelVal = val;
    }

    public void SetNumberOfGroupsToCompleteTheTask(int val)
    {
        numberOfGroupsToCompleteTask = val;
    } 

    private bool AreRequiredGroupsMade()
    {
        //check if required groups are made and check both a x b and b x a cases
        //where a is numberOfGroupsToCompleteTask and b is nextLevelVal
        if (CheckIfThisGroupsAreMade(numberOfGroupsToCompleteTask, nextLevelVal)) return true;
        if (CheckIfThisGroupsAreMade(nextLevelVal, numberOfGroupsToCompleteTask)) return true;
        return false;
    }

    private bool CheckIfThisGroupsAreMade(int numOfGroups, int val)
    {
        int groupsMade = 0;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("combineObject");
        foreach (GameObject obj in objects)
        {
            if (obj.GetComponent<CombineObjectScript>().GetVal() == val) groupsMade++;
        }
        if (groupsMade >= numOfGroups) return true;
        return false;
    }
}
