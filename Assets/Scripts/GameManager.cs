using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timmer")]
    [SerializeField] private float countDown;
    public float bonusTime;

    [SerializeField] private TMP_Text timer;

    [Header("Score")]
    [SerializeField] private TMP_Text score;
    [SerializeField] private int _score;
    [SerializeField] private Spawnner spawnnerManager;

    [Header("Panel")]
    //0 == lose pannel; 1 == setting pannel;
    [SerializeField] private GameObject[] panels;

    [Header("Button")]
    // 0 == pause Setting; 1 == Close Setting; 2 == BackTo Main Menu Setting; 3 == Back To Main Menu Lose Setting; 4 == Toogle Sound
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private AudioSource BGM;

    // Start is called before the first frame update
    void Awake()
    {
        timer = GameObject.Find("Timer_Text").GetComponent<TMP_Text>();
        score = GameObject.Find("Score_Text").GetComponent<TMP_Text>();
        spawnnerManager = gameObject.GetComponent<Spawnner>();
        BGM = gameObject.GetComponent<AudioSource>();
        _score = spawnnerManager.npcDone;
    }

    private void Start()
    {
        _score = spawnnerManager.npcDone;

        SetSoundValue();
        SetButton();
    }
    // Update is called once per frame
    void Update()
    {
        if(countDown > 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Gameplay"))
        {
            countDown -= Time.deltaTime ;
            Timmer(countDown);
            GetScore();
        }
    }

    public void Timmer(float dispaly)
    {
        if(dispaly <= 0)
        {
            dispaly = 0f;
            panels[0].SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("U Losser");
        }
        float minute = Mathf.FloorToInt(dispaly / 60);
        float second = Mathf.FloorToInt(dispaly % 60);

        timer.text = string.Format("{0:00}:{1:00}", minute, second);
    }
    private void GetScore()
    {
        score.text = "Score : " + _score;
        if (_score > 0)
            _score = spawnnerManager.npcDone + 25;
    }

    private void SetButton()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            switch (i)
            {
                //pause Setting
                case 0:
                    buttons[0].GetComponent<Button>().onClick.AddListener(OpenSetting);
                    break;

                //Close Setting
                case 1:
                    buttons[1].GetComponent<Button>().onClick.AddListener(CloseSetting);
                    break;

                //BackTo Main Menu Setting
                case 2:
                    buttons[2].GetComponent<Button>().onClick.AddListener(BackToMainMenu);
                    break;

                //BackTo Main Menu Lose
                case 3:
                    buttons[3].GetComponent<Button>().onClick.AddListener(BackToMainMenu);
                    break;

                //Toogle Sound
                case 4: 
                    buttons[4].GetComponent<Toggle>().onValueChanged.AddListener(CheckBGMValue);
                    break;

                default:
                    Debug.Log("Button Berhasil");
                    break;
            }
        }
    }

    private void OpenSetting()
    {
        Time.timeScale = 0;
        panels[1].SetActive(true);
    }

    private void CloseSetting()
    {
        Time.timeScale = 1;
        panels[1].SetActive(false);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void CheckBGMValue(bool value)
    {
        if(value == false)
        {
            buttons[4].GetComponent<Toggle>().isOn = false;
            SoundData.SetAudio("BGM", 0);
            BGM.mute = false;
        }
        else if (value == true)
        {
            buttons[4].GetComponent<Toggle>().isOn = true;
            SoundData.SetAudio("BGM", 1);
            BGM.mute = true;
        }
    }

    private void SetSoundValue()
    {
        if (SoundData.GetAudio("BGM") == 0)
        {
            buttons[4].GetComponent<Toggle>().isOn = false;
            BGM.mute = false;
        }
        else if (SoundData.GetAudio("BGM") == 1)
        {
            buttons[4].GetComponent<Toggle>().isOn = true;
            BGM.mute = true;
        }
    }
}
