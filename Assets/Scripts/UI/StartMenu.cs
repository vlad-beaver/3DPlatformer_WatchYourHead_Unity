using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _nextText;
    private Image _comics;

    [SerializeField]
    private AudioSource _slideSound;
    private int keyPressCounter=0;
    private int slidePressCounter = 1;

    // ComicsLocalization variables
    [SerializeField]
    private Image _comicsImage3;
    [SerializeField]
    private Image _comicsImage4;
    private readonly string _pathEng = "StartComics/English/";
    private readonly string _pathRussian = "StartComics/Russian/";
    private readonly string _pathBelarusian = "StartComics/Belarusian/";

    void Update()
    {
        if (Input.anyKeyDown && _mainMenu.activeSelf && !Input.GetKeyDown(KeyCode.Mouse0))
        {
            ComicsLocalization();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("QUIT");
                Application.Quit();
            }
            else {
                _comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
                _mainMenu.SetActive(false);
                _comics.enabled = true;
                StartCoroutine(ShowNextText());
            }
        }else if (Input.anyKeyDown && _nextText.activeSelf)
        {
            _nextText.SetActive(false);
            _comics.enabled = false;
            if (keyPressCounter >= 4)
            {
                PlayGame();
                return;
            }
            _slideSound = GameObject.Find("SlideMusic" + (++slidePressCounter)).GetComponent<AudioSource>();
            _comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
            _comics.enabled = true;
            StartCoroutine(ShowNextText());
        }
    }
    private void ComicsLocalization()
    {
        Sprite temp;
        switch (LocalizationManager.SelectedLanguage)
        {
            case 0:
                temp = Resources.Load<Sprite>(_pathEng + 3);
                _comicsImage3.GetComponent<Image>().sprite = temp;
                temp = Resources.Load<Sprite>(_pathEng + 4);
                _comicsImage4.GetComponent<Image>().sprite = temp;
                break;
            case 1:
                temp = Resources.Load<Sprite>(_pathRussian + 3);
                _comicsImage3.GetComponent<Image>().sprite = temp;
                temp = Resources.Load<Sprite>(_pathRussian + 4);
                _comicsImage4.GetComponent<Image>().sprite = temp;
                break;
            case 2:
                temp = Resources.Load<Sprite>(_pathBelarusian + 3);
                _comicsImage3.GetComponent<Image>().sprite = temp;
                temp = Resources.Load<Sprite>(_pathBelarusian + 4);
                _comicsImage4.GetComponent<Image>().sprite = temp;
                break;
        }
    }

    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainLevel");
    }

    IEnumerator ShowNextText()
    {
        _slideSound.Play();
        yield return new WaitForSeconds(2);
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        _nextText.SetActive(true);
    }
}
