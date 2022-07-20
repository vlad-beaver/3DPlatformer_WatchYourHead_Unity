using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    //[SerializeField]
    //private GameObject mainMenu;
    //[SerializeField]
    //private GameObject comics;
    //[SerializeField]
    //private Image comicsImage;

    //private int keyPressCounter=0;
    void Update()
    {
        if (Input.anyKey)
        {
            PlayGame();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
    //public void WatchComics()
    //{
    //    mainMenu.SetActive(false);
    //    comics.SetActive(true);
    //    comicsImage.sprite=
    //}
}
