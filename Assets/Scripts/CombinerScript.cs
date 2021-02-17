using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinerScript : MonoBehaviour
{
    List<GameObject> objects;
    [SerializeField] ParticleSystem particles;
    [SerializeField] LevelManagerCombinerScript levelManagerScript;
    [SerializeField] GetAShareLevelScript getAShareScript;

    public bool levelIsOver = false;
    public AudioClip success;
    public AudioClip fail;
    void Awake()
    {
        objects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelIsOver == true) return;
        if(objects.Count > 0)
        {
            int combinersCount = objects[0].GetComponent<CombineObjectScript>().GetCombinersCount();
            if(objects.Count == combinersCount && AreAllObjectsOnSamePosition())
            {
                if(SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(success);
                ParticleSystem p = Instantiate(particles);
                p.transform.position = objects[0].transform.position;
                objects[0].GetComponent<CombineObjectScript>().AddNewObject();
                DestroyAllObjects();
                Invoke("NotifyLevelManager", 0.20f);//use a small delay to allow objects that are being destroyed to be removed
            }
        }
    }

    private void NotifyLevelManager()
    {
        if (levelManagerScript != null) levelManagerScript.OnObjectsCombined();
        else if (getAShareScript != null) getAShareScript.OnObjectsCombined(); 
    }

    private bool AreAllObjectsOnSamePosition()
    {
        Vector3 pos = objects[0].transform.position;
        for(int i = 1; i < objects.Count; i++)
        {
            if (objects[i].transform.position != pos) return false;
        }
        return true;
    }

    private void DestroyAllObjects()
    {
        foreach(GameObject obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
    }

    public void OnObjectSelected(GameObject selectedObject)
    {
        if (objects.Count <= 0)
        {
            objects.Add(selectedObject);
        }
        else
        {
            objects.Add(selectedObject);
            if (!AreAllObjectsOfSameVal())
            {
                UnselectAllObjects();
                return;
            }
            CombineObjectScript co_scrinpt = objects[0].GetComponent<CombineObjectScript>();
            int combinersCount = co_scrinpt.GetCombinersCount();
            if(objects.Count >= combinersCount)
            {
                CombineObjects();
            }
        }
    }

    private void UnselectAllObjects()
    {
        if(SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(fail);
        while(objects.Count > 0)
        {
            GameObject obj = objects[objects.Count - 1];
            obj.GetComponent<CombineObjectScript>().Unselect();
        }
        //Debug.Log(objects);
    }

    private void CombineObjects()
    {
        for(int i = 1; i < objects.Count; i++)
        {
            objects[i].GetComponent<CombineObjectScript>().SetTarget(objects[0]);
        }
    }

    private bool AreAllObjectsOfSameVal()
    {
        CombineObjectScript co_scrinpt = objects[0].GetComponent<CombineObjectScript>();
        int val = co_scrinpt.GetVal();
        foreach (GameObject obj in objects)
        {
            co_scrinpt = obj.GetComponent<CombineObjectScript>();
            if (co_scrinpt.GetVal() != val) return false;
        }
        return true;
    }

    public void OnObjectUnselected(GameObject unselectedObject)
    {
        if(objects.IndexOf(unselectedObject) != -1)
        {
            objects.Remove(unselectedObject);
        }
    }

    public bool IsSelectingAllowed()
    {
        if (levelIsOver == true) return false;
        if (objects.Count <= 0) return true;
        if (objects[0].GetComponent<CombineObjectScript>().GetCombinersCount() > objects.Count) return true;
        return false;
    }

}
