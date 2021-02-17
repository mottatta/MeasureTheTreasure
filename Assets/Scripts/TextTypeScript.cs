using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypeScript : MonoBehaviour
{
    Text textComponent;
    float delayBetweenLetters = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
        StartCoroutine(AnimateText("This is a text I really want animated!!! \n and lets check if the new line is working too :-)"));
    }

    IEnumerator AnimateText(string inputText)
    {
        int i = 0;
        while(i < inputText.Length)
        {
            textComponent.text += inputText[i];
            i++;
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        Debug.Log("Done my friend");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
