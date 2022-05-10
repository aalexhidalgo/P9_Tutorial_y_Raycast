using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialTips;
    public GameObject PlayerPos;

    private int index;
    
    private PlayerController playerController;
    private float jumpForceValue;

    private float tol = 0.000001f;

    private void Start()
    {
        index = 0;
        ShowTutorial(index);
        
        playerController = FindObjectOfType<PlayerController>();
        jumpForceValue = playerController.jumpForce;
        playerController.jumpForce = 0;
    }

    void Update()
    {
        if (index == 0)
        {
            if (Math.Abs(Input.GetAxis("Horizontal")) > tol)
            {
                StartCoroutine(ShowNext());
            }
        }else if (index == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space) && PlayerPos.transform.position.x > tutorialTips[1].transform.position.x)
            {
                playerController.jumpForce = jumpForceValue;
                StartCoroutine(ShowNext());
            }
        }
        
    }

    private void ShowTutorial(int idx)
    {
        for (int i = 0; i < tutorialTips.Length; i++)
        {
            if (i == idx)
            {
                tutorialTips[i].SetActive(true);
                StartCoroutine(FadeIn(i));
                StartCoroutine(Letters(i));
            }
            else
            {
                tutorialTips[i].SetActive(false);
            }
        }
    }

    private IEnumerator ShowNext()
    {
        index++;
        yield return StartCoroutine(FadeOut(index - 1));
        ShowTutorial(index);
    }

    private IEnumerator FadeIn(int idx)
    {
        GameObject child = tutorialTips[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI textAlpha = child.GetComponent<TextMeshProUGUI>();
        textAlpha.alpha = 0;
        
        for (float i = 0; i < 1; i += 0.1f)
        {
            textAlpha.alpha = i;
            yield return new WaitForSeconds(0.2f);
        }
        textAlpha.alpha = 1;
    }
    
    private IEnumerator FadeOut(int idx)
    {
        GameObject child = tutorialTips[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI textAlpha = child.GetComponent<TextMeshProUGUI>();
        textAlpha.alpha = 1;
        
        for (float i = 1; i > 0; i -= 0.1f)
        {
            textAlpha.alpha = i;
            yield return new WaitForSeconds(0.1f);
        }
        textAlpha.alpha = 0;
    }

    //Aparición del diálogo por letras
    private IEnumerator Letters(int idx)
    {
        GameObject child = tutorialTips[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI message = child.GetComponent<TextMeshProUGUI>();
        string originalmessage = message.text;

        message.text = "";

        foreach(var c in originalmessage) //var (comodín)
        {
            message.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
