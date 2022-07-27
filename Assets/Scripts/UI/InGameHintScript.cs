using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class InGameHintScript : MonoBehaviour
{
    [SerializeField]
    private GameObject popUpPhrase;
    [SerializeField]
    private string hintText;
    [SerializeField]
    private TextMeshProUGUI popUpText;
    [SerializeField]
    private BoxCollider boxTrigger;

    private int _counterOfTries=0;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(_counterOfTries);
        _counterOfTries += 1;
        if(_counterOfTries==3)
        {
            StartCoroutine(Text());
        }
        
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        boxTrigger.enabled = true;
    }
    void HidePopUp()
    {
        popUpPhrase.SetActive(false);
        popUpText.text = "";
    }
    void ShowPopUp()
    {
        popUpPhrase.SetActive(true);
        popUpText.text = hintText;
       
    }

    IEnumerator Text()
    {
        ShowPopUp();
        yield return new WaitForSeconds(5);
        HidePopUp();
    }
}

