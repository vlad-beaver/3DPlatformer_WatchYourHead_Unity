using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Ending");
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        
    }
}
