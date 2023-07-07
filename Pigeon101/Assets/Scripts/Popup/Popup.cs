using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class Popup : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        canvasGroup = gameObject.transform.parent.GetComponent<CanvasGroup>();
        
    }

    public IEnumerator Show(string text)
    {   
        TextMeshProUGUI popupText = GetComponentInChildren<TextMeshProUGUI>();
        popupText.text = text;
        
        animator.Play("Popup Open");
        // set the Canus Group aplha to 1
        while(canvasGroup.alpha < 1){
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
        
        yield return new WaitForSeconds(5f);
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {   
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.Play("Popup Close");
        }
        
        while(canvasGroup.alpha > 0){
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        
    }



}
