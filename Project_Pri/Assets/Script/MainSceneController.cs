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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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


}
