using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static bool startTime;
    public static bool diceManage;
    public static bool diceManage2;
    private bool last;
    private int lastPlayer;
    private float time;
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
    public GameObject diceBoard;
    public Image[] border;
    public Image[] photos;
    public Sprite[] setImages;
    public Vector3 leftPosition, rightPosition, scaleLR, topPosition, downPosition, scaleTD;
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
        StartCoroutine("soundOnOffCheckDelay");
        StartCoroutine("activeAll");
        last = CheckOrientation.land;
        activeImages();
    }

    private void activeImages()
    {
        switch (panelControl.howManyPlayers)
        {
            case 2:
                if (panelControl.colorSelect == 0 || panelControl.colorSelect == 1)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[0];
                    photos[2].GetComponent<Image>().sprite = setImages[1];
                }
                else if (panelControl.colorSelect == 3)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[1];
                    photos[2].GetComponent<Image>().sprite = setImages[0];
                }
                break;
            case 3:
                if (panelControl.colorSelect == 0 || panelControl.colorSelect == 1)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[0];
                    photos[1].GetComponent<Image>().sprite = setImages[1];
                    photos[2].GetComponent<Image>().sprite = setImages[2];
                }
                else if (panelControl.colorSelect == 2)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[1];
                    photos[1].GetComponent<Image>().sprite = setImages[0];
                    photos[2].GetComponent<Image>().sprite = setImages[2];
                }
                else if (panelControl.colorSelect == 3)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[2];
                    photos[1].GetComponent<Image>().sprite = setImages[1];
                    photos[2].GetComponent<Image>().sprite = setImages[0];
                }
                break;
            case 4:
                if (panelControl.colorSelect == 0 || panelControl.colorSelect == 1)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[0];
                    photos[1].GetComponent<Image>().sprite = setImages[1];
                    photos[2].GetComponent<Image>().sprite = setImages[2];
                    photos[3].GetComponent<Image>().sprite = setImages[3];
                }
                else if (panelControl.colorSelect == 2)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[1];
                    photos[1].GetComponent<Image>().sprite = setImages[0];
                    photos[2].GetComponent<Image>().sprite = setImages[2];
                    photos[3].GetComponent<Image>().sprite = setImages[3];
                }
                else if (panelControl.colorSelect == 3)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[2];
                    photos[1].GetComponent<Image>().sprite = setImages[1];
                    photos[2].GetComponent<Image>().sprite = setImages[0];
                    photos[3].GetComponent<Image>().sprite = setImages[3];
                }
                else if (panelControl.colorSelect == 4)
                {
                    photos[0].GetComponent<Image>().sprite = setImages[3];
                    photos[1].GetComponent<Image>().sprite = setImages[1];
                    photos[2].GetComponent<Image>().sprite = setImages[2];
                    photos[3].GetComponent<Image>().sprite = setImages[0];
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (last != CheckOrientation.land)
        {
            StartCoroutine("activeAll");
        }
        last = CheckOrientation.land;
        boardPossition();
        borderLine();
    }

    private void borderLine()
    {
        border[0].enabled = false;
        border[1].enabled = false;
        border[2].enabled = false;
        border[3].enabled = false;
        border[GameManager.currentPlayer - 1].enabled = true;
        //      for Image fillAmount
        if (startTime == false)
        {
            startTime = true;
            time = 10.0f;
            border[GameManager.currentPlayer - 1].fillAmount = 1f;
        }
        if (startTime == true)
        {
            border[GameManager.currentPlayer - 1].fillAmount -= 1.0f / time * Time.deltaTime;
            if (border[GameManager.currentPlayer - 1].fillAmount == 0f)
            {
                diceManage = true;
                diceManage2 = true;
                GameManager.repeatedSix = 0;
            }
        }
        if (lastPlayer != GameManager.currentPlayer)
        {
            startTime = false;
            border[0].fillAmount = 1.0f;
            border[1].fillAmount = 1.0f;
            border[2].fillAmount = 1.0f;
            border[3].fillAmount = 1.0f;
            lastPlayer = GameManager.currentPlayer;
        }
    }

    private void boardPossition()
    {
        if (CheckOrientation.land == true)
        {
            if (GameManager.currentPlayer == 1 || GameManager.currentPlayer == 2)
            {
                diceBoard.transform.localScale = scaleLR;
                diceBoard.transform.position = leftPosition;
            }
            else if (GameManager.currentPlayer == 3 || GameManager.currentPlayer == 4)
            {
                diceBoard.transform.localScale = scaleLR;
                diceBoard.transform.position = rightPosition;
            }
        }
        else
        {
            if (GameManager.currentPlayer == 1 || GameManager.currentPlayer == 4)
            {
                diceBoard.transform.localScale = scaleTD;
                diceBoard.transform.position = downPosition;
            }
            else if (GameManager.currentPlayer == 2 || GameManager.currentPlayer == 3)
            {
                diceBoard.transform.localScale = scaleTD;
                diceBoard.transform.position = topPosition;
            }
        }
    }

    IEnumerator activeAll()
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        foreach (GameObject q in panels)
        {
            q.SetActive(false);
        }
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

    private void setPlayerImages()
    {
        if (panelControl.howManyPlayers == 2)
        {
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
