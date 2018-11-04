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
    public void Load_To_Title_Scene()
    {
        SceneManager.LoadScene("Title 1");
    }
}
