using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{

    public GameObject[] nodes;
    public GameObject[] levelNodes;
    public GameObject playButton;
    public SpriteRenderer renderer;
    public Sprite shipSprite;
    public Sprite pirateSprite;
    public GameObject[] shipSkinNodes;
    public GameObject[] pirateSkinNodes;
    public GameObject[] treasureCollectNodes;
    public GameObject[] treasures;
    int currentTreasure = 0;

    int currentLevel = -1;
    int targetNodeIndex = 0;
    int currentNodeIndex = 0;
    public float moveSpeed = 1.0f;
    bool isFinalTargetReached;

    void Start()
    {

    }

    void Update()
    {
        GameObject node = nodes[currentNodeIndex];
        if(node != null)
        {
            if (Vector3.Distance(transform.position, node.transform.position) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, node.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                CheckIfToChangeSkin(node);
                if(node != nodes[targetNodeIndex]) TargetNextNode();
                else if (!isFinalTargetReached)
                {
                    isFinalTargetReached = true;
                    playButton.GetComponent<Animator>().Play("PlayButton_Appear");
                }
            }
        }
    }

    void CheckIfToChangeSkin(GameObject nodePlayerIsOn)
    {
        foreach(GameObject node in shipSkinNodes)
        {
            if (node == nodePlayerIsOn) renderer.sprite = shipSprite;
        }
        foreach (GameObject node in pirateSkinNodes)
        {
            if (node == nodePlayerIsOn) renderer.sprite = pirateSprite;
        }
        foreach(GameObject node in treasureCollectNodes)
        {
            if (node == nodePlayerIsOn) RemoveATreasure();
        }
    }

    void RemoveATreasure()
    {
        treasures[currentTreasure].SetActive(false);
        currentTreasure++;
    }

    void TargetNextNode()
    {
        nodes[currentNodeIndex].GetComponent<Node>().Show();
        currentNodeIndex++;
    }

    public void SetNewLevelTargetNode()
    {
        currentLevel++; 
        targetNodeIndex = GetIndexOfNode(levelNodes[currentLevel]);
        isFinalTargetReached = false;
        playButton.GetComponent<Animator>().Play("PlayButton_Invisible");
    }

    int GetIndexOfNode(GameObject node)
    {
        for(int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] == node) return i;
        }
        return -1;
    }

    public void ChangeToShipSprite()
    {
        renderer.sprite = shipSprite;
    }

    public void ChangeToPirateSprite()
    {
        renderer.sprite = pirateSprite;
    }
}
