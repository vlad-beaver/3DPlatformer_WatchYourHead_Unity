using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLanguage : MonoBehaviour
{
    public void OpenStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
