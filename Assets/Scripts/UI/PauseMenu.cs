using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    private static bool GameIsPaused = false;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject inGameUI;

    public AudioMixerGroup mixer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        inGameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
    void Pause()
    {
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMainMenu()
    {
        GameIsPaused = false;
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
