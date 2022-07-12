using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public GameObject endGameMenu;
    public GameObject inGameUI;
    void OnTriggerEnter(Collider other)
    {
        //PuzzlePlayer.Instance.Kill();
        endGameMenu.SetActive(true);
        inGameUI.SetActive(false);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Object is within trigger");
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object Exited the trigger");
    }
    

}
