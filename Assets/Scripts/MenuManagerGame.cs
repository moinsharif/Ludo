using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerGame : MonoBehaviour
{
    public GameObject quitGame;
    public GameObject soundOn, soundOff, musicOn, musicOff;
    public AudioSource gameStartSource;

    private void Start()
    {
        if(PlayerPrefs.GetInt("soundState", 0) == 0)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }

        if (PlayerPrefs.GetInt("musicState", 0) == 0)
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            gameStartSource.mute = false;
        }
        else
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
            gameStartSource.mute = true;
        }
    }

    public void quitGameonPlaying()
    {
        SoundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("Home");
    }
    public void replayGame()
    {
        SoundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void quitCancel()
    {
        SoundManager.buttonAudioSource.Play();
        quitGame.SetActive(false);
    }

    public void quitButtonClick()
    {
        SoundManager.buttonAudioSource.Play();
        quitGame.SetActive(true);
    }

    public void soundOnClicked()
    {
        soundOn.SetActive(false);
        soundOff.SetActive(true);
        PlayerPrefs.SetInt("soundState", 1);
        SoundManager.soundOff();
    }

    public void soundOffClicked()
    {
        soundOn.SetActive(true);
        soundOff.SetActive(false);
        PlayerPrefs.SetInt("soundState", 0);
        SoundManager.soundOn();
    }

    public void musicOnClicked()
    {
        musicOn.SetActive(false);
        musicOff.SetActive(true);
        PlayerPrefs.SetInt("musicState", 1);
        gameStartSource.mute = true;
    }

    public void musicOffClicked()
    {
        musicOn.SetActive(true);
        musicOff.SetActive(false);
        PlayerPrefs.SetInt("musicState", 0);
        gameStartSource.mute = false;
    }
}
