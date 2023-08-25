using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] panels;
    public GameObject[] timeSection;
    public Sprite toggleOn;
    public Sprite toggleOff;
    public Sprite micOn;
    public Sprite micOff;
    public GameObject sound;
    public GameObject music;
    public GameObject notification;
    public GameObject mic;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("musicState") == 0)
        {
            audioData.Play(0);
        }
        setPlayerImages();
        toggleSprite("soundState");
        toggleSprite("notification");
        toggleSprite("musicState");
        toggleSprite("mic");
        soundOnOffCheckDelay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator soundOnOffCheckDelay()
    {
        yield return new WaitForSeconds(1);
        if (PlayerPrefs.GetInt("soundState") == 1)
        {
            SoundManager.soundOff();
        }
        else
        {
            SoundManager.soundOn();
        }
    }

    public void popupOpen(int sl)
    {
        SoundManager.buttonAudioSource.Play();
        panels[sl].SetActive(true);
    }

    public void popupClose(int sl)
    {
        SoundManager.buttonAudioSource.Play();
        panels[sl].SetActive(false);
    }

    public void togglePref(string key)
    {
        SoundManager.buttonAudioSource.Play();
        if (PlayerPrefs.GetInt(key) == 0)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
        toggleSprite(key);
        soundOnOffCheck();
    }

    public void timeChange(int sl)
    {
        SoundManager.buttonAudioSource.Play();
        Color tempColor;
        Image image;
        Text childTxt;
        for (int i = 0; i < 3; i++)
        {
            image = timeSection[i].GetComponent<Image>();
            tempColor = image.color;
            tempColor.a = 0f;
            image.color = tempColor;
            childTxt = timeSection[i].transform.GetChild(0).gameObject.GetComponent<Text>();
            childTxt.color = Color.white;

        }
        image = timeSection[sl].GetComponent<Image>();
        tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        childTxt = timeSection[sl].transform.GetChild(0).gameObject.GetComponent<Text>();
        childTxt.color = Color.black;
    }

    private void soundOnOffCheck()
    {
        if (PlayerPrefs.GetInt("soundState") == 1)
        {
            SoundManager.soundOff();
        }
        else
        {
            SoundManager.soundOn();
        }
    }

    private void toggleSprite(string key)
    {
        Sprite setImage, setMic;
        if (PlayerPrefs.GetInt(key) == 1)
        {
            setImage = toggleOff;
            setMic = micOff;
        }
        else
        {
            setImage = toggleOn;
            setMic = micOn;
        }

        switch (key)
        {
            case "soundState":
                sound.GetComponent<Image>().sprite = setImage;
                break;
            case "musicState":
                music.GetComponent<Image>().sprite = setImage;
                if (PlayerPrefs.GetInt("musicState") == 0)
                {
                    audioData.Play(0);
                }
                else
                {
                    audioData.Pause();
                }
                break;
            case "notification":
                notification.GetComponent<Image>().sprite = setImage;
                break;
            case "mic":
                mic.GetComponent<Image>().sprite = setMic;
                break;
        }
    }

    private void setPlayerImages() {
        if (panelControl.howManyPlayers == 2) { 
            players[0].SetActive(false);
            players[1].SetActive(true);
            players[2].SetActive(true);
            players[3].SetActive(false);
        }
        else if (panelControl.howManyPlayers == 3)
        {
            players[0].SetActive(true);
            players[1].SetActive(true);
            players[2].SetActive(true);
            players[3].SetActive(false);
        }
        else if (panelControl.howManyPlayers == 4)
        {
            players[0].SetActive(true);
            players[1].SetActive(true);
            players[2].SetActive(true);
            players[3].SetActive(true);
        }
    }
}
