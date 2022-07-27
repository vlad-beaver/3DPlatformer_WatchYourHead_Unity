using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class InGameHintScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _popUpPhrase;
    [SerializeField]
    private string _hintText;
    [SerializeField]
    private TextMeshProUGUI _popUpText;
    [SerializeField]
    private BoxCollider _boxTrigger;

    private int _counterOfTries=0;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(_counterOfTries);
        _counterOfTries += 1;
        if(_counterOfTries==3)
        {
            _counterOfTries = 0;
            StartCoroutine(Text());
        }
        
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        _boxTrigger.enabled = true;
    }
    void HidePopUp()
    {
        _popUpPhrase.SetActive(false);
        _popUpText.text = "";
    }
    void ShowPopUp()
    {
        _popUpPhrase.SetActive(true);
        _popUpText.text = _hintText;
       
    }

    IEnumerator Text()
    {
        ShowPopUp();
        yield return new WaitForSeconds(5);
        HidePopUp();
    }
}

