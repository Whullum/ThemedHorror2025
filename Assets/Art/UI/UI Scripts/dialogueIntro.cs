using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class dialogueIntro : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public string mainSceneName;
    private int index;
    public GameObject arrow;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrow.SetActive(false);
        textComponent.text=string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text==lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text=lines[index];
            }
        }
    }


    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text+=c;
            yield return new WaitForSeconds(textSpeed);

        }
        arrow.SetActive(true);
    }

    void NextLine()
    {
        arrow.SetActive(false);
        if (index<lines.Length-1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(mainSceneName);
        }
    }
}
