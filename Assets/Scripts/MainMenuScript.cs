using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject exitGame, GameType, GameSelection;
    public GameObject tokenSelect1, tokenSelect2;
    public GameObject singleGame, dualGame;
    public GameObject twoP, threeP, fourP;
    public static int howManyPlayers;
    public static int gutiType;
    public static int boardType;
    public AudioSource gameStartSource;
    public GameObject blueFbg, redFbg, blueFselect, redfselect;


    // Start is called before the first frame update
    void Start()
    {
        gutiType = 0;
        howManyPlayers = 2;
        boardType = 0;
        if (PlayerPrefs.GetInt("musicState", 0) == 0)
        {
            gameStartSource.mute = false;
        }
        else
        {
            gameStartSource.mute = true;
        }
    }

    public void selectedboard0()
    {
        boardType = 0;
        blueFbg.SetActive(false);
        redFbg.SetActive(true);
        blueFselect.SetActive(true);
        redfselect.SetActive(false);
    }
    public void selectedboard1()
    {
        boardType = 1;
        blueFbg.SetActive(true);
        redFbg.SetActive(false);
        blueFselect.SetActive(false);
        redfselect.SetActive(true);
    }
    public void selectedTwoP()
    {
        SoundManager.buttonAudioSource.Play();
        howManyPlayers = 2;
        threeP.SetActive(false);
        fourP.SetActive(false);
        twoP.SetActive(true);
    }
    public void selectedThreeP()
    {
        SoundManager.buttonAudioSource.Play();
        howManyPlayers = 3;
        threeP.SetActive(true);
        fourP.SetActive(false);
        twoP.SetActive(false);
    }
    public void selectedFourP()
    {
        SoundManager.buttonAudioSource.Play();
        howManyPlayers = 4;
        threeP.SetActive(false);
        fourP.SetActive(true);
        twoP.SetActive(false);
    }
    public void selectedGuti1()
    {
        SoundManager.buttonAudioSource.Play();
        gutiType = 0;
        tokenSelect2.SetActive(false);
        tokenSelect1.SetActive(true);
    }
    public void selectedGuti2()
    {
        SoundManager.buttonAudioSource.Play();
        gutiType = 1;
        tokenSelect2.SetActive(true);
        tokenSelect1.SetActive(false);
    }
    public void selectedSingleGame()
    {
        SoundManager.buttonAudioSource.Play();
        singleGame.SetActive(true);
        dualGame.SetActive(false);
    }
    public void selectedDualGame()
    {
        SoundManager.buttonAudioSource.Play();
        singleGame.SetActive(false);
        dualGame.SetActive(true);
    }
    public void openGameType()
    {
        SoundManager.buttonAudioSource.Play();
        GameType.SetActive(true);
    }
    public void closeGameType()
    {
        SoundManager.buttonAudioSource.Play();
        GameType.SetActive(false);
    }
    public void openGameSelection()
    {
        SoundManager.buttonAudioSource.Play();
        GameSelection.SetActive(true);
        if(gutiType == 0)
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("guti2");
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
        }
        else
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("guti1");
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
        }
    }
    public void closeGameSelection()
    {
        SoundManager.buttonAudioSource.Play();
        GameSelection.SetActive(false);
    }
    public void exitButtonClick()
    {
        SoundManager.buttonAudioSource.Play();
        exitGame.SetActive(true);
    }
    public void quit()
    {
        SoundManager.buttonAudioSource.Play();
        Application.Quit();
    }
    public void quitCancel()
    {
        SoundManager.buttonAudioSource.Play();
        exitGame.SetActive(false);
    }
    public void playGame()
    {
        SoundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("GameScene");
    }
}
