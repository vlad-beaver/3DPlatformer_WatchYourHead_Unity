using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Ending");
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        
    }
}
