using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{

    public GameObject SettingsMenuPanel;

    public AudioSource source;
    public SFXNMusicSO soundTest;


    // Start is called before the first frame update
    void Start()
    {
        SettingsMenuPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
	{
        SceneManager.LoadScene(1);
	}

    public void OpenSettingsMenu()
	{
        SettingsMenuPanel.gameObject.SetActive(true);
	}

    public void CloseSettingsMenu()
	{
        SettingsMenuPanel.gameObject.SetActive(false);
	}

    public void SoundTesting()
	{
        int r = Random.Range(0, 7);
        source.clip = soundTest.sfx[r];
        source.Play();
	}
}
