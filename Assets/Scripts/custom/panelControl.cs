using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class panelControl : MonoBehaviour
{
    private bool last;
    public GameObject[] panels;
    public Sprite[] gutiImage;
    public GameObject[] guti;
    public GameObject gutiHolder;
    public GameObject[] timeSection;
    public Sprite toggleOn;
    public Sprite toggleOff;
    public GameObject sound;
    public GameObject music;
    public GameObject notification;
    public TMP_Text priceText;
    public Text[] coins;
    public static int priceInt;
    public static int howManyPlayers;
    public static int colorSelect;
    AudioSource audioData;

    /*
     soundState , musicState , notification
    0 = ON and 1 = OFF
     */

    // Start is called before the first frame update
    void Start()
    {
        priceInt = Convert.ToInt32(priceText.text);
        audioData = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("musicState") == 0)
        {
            audioData.Play(0);
        }
        toggleSprite("soundState");
        toggleSprite("notification");
        toggleSprite("musicState");
        activePanelNoSound(0);
        playerPref("soundState", 0);
        playerPref("musicState", 0);
        playerPref("notification", 0);
        playerPref("language", 0);
        playerPref("mic", 0);
        StartCoroutine("soundOnOffCheckDelay");
        StartCoroutine("activeAll");
        last = CheckOrientation.land;
    }

    public void plusBtn(int amount)
    {
        priceInt = priceInt + amount;
        priceText.text = "" + priceInt;
        coinTextChange();
    }

    public void minusBtn(int amount)
    {
        if (priceInt > 100)
        {
            priceInt = priceInt - amount;
        }
        priceText.text = "" + priceInt;
        coinTextChange();
    }

    private void coinTextChange()
    {
        foreach (Text coinTxt in coins)
        {
            coinTxt.text = ""+priceInt;
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

    // Update is called once per frame
    void Update()
    {
        if (last != CheckOrientation.land)
        {
            StartCoroutine("activeAll");
        }
        last = CheckOrientation.land;
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
    }

    IEnumerator activeAll()
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        hideAll();
        activePanelNoSound(0);
    }

    private void hideAll()
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }
    }

    public void activePanel(int sl)
    {
        SoundManager.buttonAudioSource.Play();
        hideAll();
        panels[sl].SetActive(true);
    }

    public void activePanelNoSound(int sl)
    {
        hideAll();
        panels[sl].SetActive(true);
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

    //Panel 03 Choose Color Change image
    public void colorChoose(int position)
    {
        colorSelect = position + 1;
        SoundManager.buttonAudioSource.Play();
        for (int srl = 0; srl < guti.Length; srl++)
        {
            guti[srl].GetComponent<Image>().sprite = gutiImage[srl];
        }
        guti[position].GetComponent<Image>().sprite = gutiImage[position + 4];
    }

    //"board" Save into PlayerPrefs
    public void board(int player)
    {
        SoundManager.buttonAudioSource.Play();
        PlayerPrefs.SetInt("board", player);
        howManyPlayers = player;
        if (player == 5)
        {
            howManyPlayers = 4;
        }
        switch (howManyPlayers)
        {
            case 2:
                guti[0].SetActive(true);
                guti[1].SetActive(false);
                guti[2].SetActive(true);
                guti[3].SetActive(false);
                gutiHolder.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.15f, 0.56f);
                gutiHolder.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.85f, 0.76f);
                break;
            case 3:
                guti[0].SetActive(true);
                guti[1].SetActive(true);
                guti[2].SetActive(true);
                guti[3].SetActive(false);
                gutiHolder.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.06f, 0.56f);
                gutiHolder.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.76f, 0.76f);
                break;
            case 4:
                guti[0].SetActive(true);
                guti[1].SetActive(true);
                guti[2].SetActive(true);
                guti[3].SetActive(true);
                gutiHolder.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.15f, 0.56f);
                gutiHolder.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.85f, 0.76f);
                break;
        }
    }

    public void loadBoard()
    {
        SoundManager.buttonAudioSource.Play();
        int board = PlayerPrefs.GetInt("board");
        activePanel(board + 7);
    }

    public void startGame()
    {
        SoundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("GameScene");
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
        Sprite setImage;
        if (PlayerPrefs.GetInt(key) == 1)
        {
            setImage = toggleOff;
        }
        else
        {
            setImage = toggleOn;
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
        }
    }

    private void playerPref(string key, int val)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, val);
        }
    }

}