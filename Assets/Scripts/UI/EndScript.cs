using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _endGameMenu;
    [SerializeField]
    private GameObject _nextButton;

    private Image _comics;

    private int _keyPressCounter = 0;

    public void NextComics()
    {
        _nextButton.SetActive(false);
        _comics.enabled = false;
        if (_keyPressCounter >= 2)
        {
            _endGameMenu.SetActive(true);
        }
        else
        {
            _comics = GameObject.Find("Comics" + (++_keyPressCounter)).GetComponent<Image>();
            _comics.enabled = true;
            StartCoroutine(ShowNextButton());
        }
    }

    private void Start()
    {
        _comics = GameObject.Find("Comics" + (++_keyPressCounter)).GetComponent<Image>();
        _comics.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(ShowNextButton());
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartMenuWithExit");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    IEnumerator ShowNextButton()
    {
        yield return new WaitForSeconds(1);
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        _nextButton.SetActive(true);
    }

}
