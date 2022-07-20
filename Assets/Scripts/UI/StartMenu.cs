using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    private Image comics;

    private int keyPressCounter=0;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (keyPressCounter == 0)
            {
                mainMenu.SetActive(false);
            }
            if (keyPressCounter >= 4)
            {
                PlayGame();
                return;
            }
            if (comics != null)
            {
                comics.enabled = false;
            }  
            comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
            comics.enabled = true;

        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

}
