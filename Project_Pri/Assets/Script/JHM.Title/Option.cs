using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {

    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject supportPanel;
    [SerializeField] private Slider BGMVolume;
    [SerializeField] private Slider SFVolume;

    private void Start()
    {
        SetSliderValues();
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

    public void ExitOption()
    {
        optionPanel.SetActive(false);
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
}
