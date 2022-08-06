using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScript : PuzzleComponent
{
    [SerializeField]
    private GameObject _endGameMenu;
    [SerializeField]
    private GameObject _nextText;
    [SerializeField]
    private AudioSource _firstEndingSound;
    [SerializeField]
    private AudioSource _secondEndingSound;

    private int _numOfCadrs = 2;

    private Image _comics;

    private int _keyPressCounter = 0;

    private void Update()
    {
        if (Input.anyKeyDown && _nextText.activeSelf)
        {
            _nextText.SetActive(false);
            _comics.enabled = false;
            if (SceneManager.GetActiveScene().name == "SecondEnding")
            {
                _numOfCadrs = 3;
            }
            else
            {
                _numOfCadrs = 2;
            }
            if (_keyPressCounter >= _numOfCadrs)
            {
                _endGameMenu.SetActive(true);
            }
            else
            {
                _comics = GameObject.Find("Comics" + (++_keyPressCounter)).GetComponent<Image>();
                SoundHandler();
                _comics.enabled = true;
                StartCoroutine(ShowNextText());
            }
        }
    }

    private void SoundHandler()
    {
        if (SceneManager.GetActiveScene().name == "SecondEnding" && _keyPressCounter == 2)
        {
            _firstEndingSound.Stop();
            _secondEndingSound.Play();
        }
    }

    private void Start()
    {
        _comics = GameObject.Find("Comics" + (++_keyPressCounter)).GetComponent<Image>();
        _comics.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(ShowNextText());
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

    IEnumerator ShowNextText()
    {
        yield return new WaitForSeconds(3);
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        _nextText.SetActive(true);
    }

}
