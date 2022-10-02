using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenusController : MonoBehaviour
{

    public bool isPaused; // Is the game paused
    public bool isGameOver; // Is the game over

    public GameObject pauseMenuPanel;
    public GameObject settingsMenuPanel;
    public GameObject gameOverMenu;

    public SFXNMusicSO soundTest;

    private AudioSource tester;



    // Start is called before the first frame update
    void Start()
    {
        pauseMenuPanel.gameObject.SetActive(false);
        settingsMenuPanel.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        tester = GetComponent<AudioSource>();
        tester.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
		{
            Time.timeScale = 0;
            pauseMenuPanel.gameObject.SetActive(true);
            isPaused = true;
		} 
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
		{
            pauseMenuPanel.gameObject.SetActive(false);
            settingsMenuPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
		}

        if(isGameOver == true)
		{
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
		}

    }

    public void ReturnToMainMenu()
	{
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
	}

    public void OpenSettingsMenu()
	{
        settingsMenuPanel.gameObject.SetActive(true);
	}

    public void CloseSettings()
	{
        settingsMenuPanel.gameObject.SetActive(false);
    }

    public void ClosePause()
	{
        pauseMenuPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SoundTestTime()
	{
        int r = Random.Range(0, 7);
        tester.clip = soundTest.sfx[r];
        tester.Play();
	}
}
