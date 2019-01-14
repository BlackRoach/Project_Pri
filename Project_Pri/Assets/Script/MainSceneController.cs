using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour {

    public GameObject schedulePanel;
    public Image scheduleButton; // 스케줄 버튼 이미지
    private bool IsOpenSchedule = false;

    public Sprite scheduleClose;
    public Sprite scheduleOpen;

    [SerializeField] private GameObject option_Panel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject supportPanel;
    [SerializeField] private Slider BGMVolume;
    [SerializeField] private Slider SFVolume;

    private void Start()
    {
        SetSliderValues();
    }
    public  void OpenSchedule() // 토글 버튼
    {
        if (!IsOpenSchedule)
        {
            schedulePanel.SetActive(true);
            scheduleButton.sprite = scheduleOpen;
        }
        else
        {
            schedulePanel.SetActive(false);
            scheduleButton.sprite = scheduleClose;
        }

        IsOpenSchedule = !IsOpenSchedule;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void Option_Panel_On()
    {
        option_Panel.SetActive(true);
    }
    public void Option_Panel_Off()
    {
        option_Panel.SetActive(false);
    }

    public void SetBGMVolume(float value)
    {
        AudioManager AM = FindObjectOfType<AudioManager>();
        AM.SetBGMVolume(value);
    }

    public void SetSoundEffectVolume(float value)
    {
        AudioManager AM = FindObjectOfType<AudioManager>();
        AM.SetSoundEffectVolume(value);
    }

    public void EnterSupportPanel()
    {
        settingPanel.SetActive(false);
        supportPanel.SetActive(true);
    }

    public void ExitSupportPanel()
    {
        supportPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    // BGM과 효과음의 볼륨값을 BGM 슬라이더와 효과음 슬라이더 값에 입력합니다.
    private void SetSliderValues()
    {
        AudioManager AM = FindObjectOfType<AudioManager>();
        BGMVolume.value = AM.BGM.volume;
        SFVolume.value = AM.SoundEffect.volume;
    }
    public void Load_To_Title_Scene()
    {
        SceneManager.LoadScene("Title 1");
    }
}
