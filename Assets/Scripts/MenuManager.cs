using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panel")]
    //0 == Info pannel; 1 == setting pannel;
    [SerializeField] private GameObject[] panels;

    [Header("Button")]
    // 0 == pause Setting; 1 == Close Setting; 2 == Info Open; 3 == Info Open; 4 = Play; 5 == Toogle Sound
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private AudioSource BGM;

    private void Awake()
    {
        BGM = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        SetSoundValue();
        GetButtons();
    }

    private void GetButtons()
    {
        for (int i = 0;i < buttons.Length; i++)
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

                //Info Open
                case 2:
                    buttons[2].GetComponent<Button>().onClick.AddListener(OpenInfoPanel);
                    break;

                //Info Close
                case 3:
                    buttons[3].GetComponent<Button>().onClick.AddListener(CloseInfoPanel);
                    break;

                //Play
                case 4:
                    buttons[4].GetComponent<Button>().onClick.AddListener(PlayToGamePlay);
                    break;

                //Toogle Sound
                case 5:
                    buttons[5].GetComponent<Toggle>().onValueChanged.AddListener(CheckBGMValue);
                    break;

                default:
                    Debug.Log("Button Berhasil");
                    break;
            }
        }
    }

    private void OpenSetting()
    {
        panels[1].SetActive(true);
    }

    private void CloseSetting()
    {
        panels[1].SetActive(false);
    }

    private void OpenInfoPanel()
    {
        panels[0].SetActive(true);
    }

    private void CloseInfoPanel()
    {
        panels[0].SetActive(false);
    }

    private void PlayToGamePlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void CheckBGMValue(bool value)
    {
        if (value == false)
        {
            buttons[5].GetComponent<Toggle>().isOn = false;
            SoundData.SetAudio("BGM", 0);
            BGM.mute = false;
        }
        else if (value == true)
        {
            buttons[5].GetComponent<Toggle>().isOn = true;
            SoundData.SetAudio("BGM", 1);
            BGM.mute = true;
        }
    }

    private void SetSoundValue()
    {
        if (SoundData.GetAudio("BGM") == 0)
        {
            buttons[5].GetComponent<Toggle>().isOn = false;
            BGM.mute = false;
        }
        else if (SoundData.GetAudio("BGM") == 1)
        {
            buttons[5].GetComponent<Toggle>().isOn = true;
            BGM.mute = true;
        }
    }
}
