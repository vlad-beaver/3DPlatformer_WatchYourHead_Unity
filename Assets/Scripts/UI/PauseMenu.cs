using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    private static bool _gameIsPaused = false;
    [SerializeField]
    private GameObject _pauseMenuUI;
    [SerializeField]
    private GameObject _authorsMenuUI;
    [SerializeField]
    private GameObject _inGameUI;

    public AudioMixerGroup mixer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Awake()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        _inGameUI.SetActive(true);
        _pauseMenuUI.SetActive(false);
        _authorsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
    void Pause()
    {
        _inGameUI.SetActive(false);
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void BackToMainMenu()
    {
        _gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // For setting screen in UI

    //private void ToggleMusic()
    //{
    //    if (GameIsPaused)
    //        mixer.audioMixer.SetFloat("MasterVolume", 0);
    //    else
    //        mixer.audioMixer.SetFloat("MasterVolume", -80);
    //}

    //private void ChangeVolume(float volume)
    //{
    //    mixer.audioMixer.GetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
    //}
}
